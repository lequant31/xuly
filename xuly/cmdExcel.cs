using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xuly
{
    public class cmdExcel
    {

        private string connString;
        public string ErrorText = "";

        public string getVersion()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication;
            excelApplication = new Microsoft.Office.Interop.Excel.ApplicationClass();
            return excelApplication.Version;
        }

        public string CheckVersionExcel()
        {
            RegistryKey baseKey = Registry.ClassesRoot;
            RegistryKey subKey = baseKey.OpenSubKey(@"Excel.Application\CurVer");
            return (string)subKey.GetValue("");
        }

        public cmdExcel(string excelFile)
        {
            if (CheckVersionExcel() == "Excel.Application.12")
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + excelFile + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            else
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                    "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
        }

        //Cac sheet
        public string[] GetExcelSheetNames()
        {
            OleDbConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = new OleDbConnection(connString);

                // Mở kết nối đến CSDL
                objConn.Open();
                // Lấy về dữ liệu 
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                string[] excelSheets = new string[dt.Rows.Count];
                int i = 0;
                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                return excelSheets;
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                return null;
            }
            finally
            {
                //Xóa đối tượng.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        //Lay du lieu tu excel
        public DataSet ExcelSelect(string SheetName)
        {
            OleDbConnection oleConn = new OleDbConnection(connString);
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter("select * from [" + SheetName + "]", oleConn);
            DataSet dSet = new DataSet();

            try
            {
                oleConn.Open();
                oleAdapter.Fill(dSet);
                oleConn.Close();

                return dSet;
            }
            catch (Exception ex)
            {
                oleConn.Close();
                ErrorText = ex.Message;
                return null;
            }
        }
    }
}
