using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xuly
{
    public partial class ImportExcel : Form
    {
        cmdExcel objExcel;
        public ImportExcel()
        {
            InitializeComponent();
        }
        decimal getNumber(int i, string name)
        {
            try
            {
                var str = grvKH.GetRowCellDisplayText(i, name) ?? "0";
                if (str.Trim() == "") str = "0";
                return Convert.ToDecimal(str);
            }
            catch
            {
                return 0;
            }
        }
        string getValue(string temb, int maxLen)
        {
            return temb == null || temb.Length == 0 ? "" : temb.Length > maxLen ? temb.Substring(0, maxLen) : temb;
        }

        string formatPhone(string mobile)
        {
            var str = ".,; ()-_  ";
            for (var i = 0; i < str.Length; i++)
            {
                mobile = mobile.Replace(str.Substring(i, 1), "");
            }
            return mobile;
        }
        private void itemExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (OpenFileDialog file = new OpenFileDialog())
                {
                    file.Filter = "(Excel file)|*.xls;*.xlsx";
                    file.ShowDialog();
                    if (file.FileName == "") return;
                    objExcel = new cmdExcel(file.FileName);
                    string[] sheets = objExcel.GetExcelSheetNames();
                    cmbSheet.Items.Clear();
                    foreach (string s in sheets)
                        cmbSheet.Items.Add(s.Trim('$'));
                    itemSheet.EditValue = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void itemSheet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                using (DataTable tblExcel = objExcel.ExcelSelect(itemSheet.EditValue.ToString() + "$").Tables[0])
                {
                    tblExcel.Columns.Add("Error", typeof(string));
                    grvKH.Columns.Clear();
                    DevExpress.XtraGrid.Columns.GridColumn col;
                    for (int i = 0; i < tblExcel.Columns.Count; i++)
                    {
                        if (tblExcel.Columns[i].Caption.IndexOf("F") == 0) continue;

                        col = new DevExpress.XtraGrid.Columns.GridColumn();
                        col.Caption = tblExcel.Columns[i].Caption;
                        col.FieldName = tblExcel.Columns[i].ColumnName;
                        col.OptionsColumn.AllowEdit = !(col.FieldName == "Error");


                        col.VisibleIndex = i;

                        grvKH.Columns.Add(col);
                    }

                    gcKH.DataSource = tblExcel;

                    //for (int i = 0; i < grvKH.RowCount; i++)
                    //{
                    //    var hoTen = (grvKH.GetRowCellValue(i, "c3") as string) ?? "";
                    //    if (hoTen.Trim() == "")
                    //        grvKH.SelectRow(i);
                    //    else
                    //        grvKH.UnselectRow(i);
                    //}

                    //grvKH.DeleteSelectedRows();
                }
            }
            else
            {
                gcKH.DataSource = null;
            }
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gcKH.DataSource == null)
            //{
            //    MessageBox.Show("Vui lòng chọn sheet");
            //    return;
            //}


            //var db = new CaDoDataContext();
            //var _tk = new quanlyTK();
            ////_tk.SoTK = grvKH.GetRowCellValue(0, "tài khoản").ToString().Trim();
            ////db.quanlyTKs.InsertOnSubmit(_tk);
            ////db.SubmitChanges();


            //try
            //{

            //    int _l = grvKH.RowCount / 5;
            //    for (int j = 0; j < _l; j++)
            //    {
            //        try
            //        {
            //            int i = 5 * j;

            //            grvKH.UnselectRow(i);
            //            grvKH.UnselectRow(i + 1);
            //            grvKH.UnselectRow(i + 2);
            //            grvKH.UnselectRow(i + 3);
            //            grvKH.UnselectRow(i + 4);

            //            var objKH = new thongtinchung();

            //            //objKH.id_quanlyTK_idTK = _tk.idTK;
            //            objKH.id_quanlyTK_idTK = Int32.Parse(txt_matk.EditValue.ToString());
            //            string[] _s1 = getValue(grvKH.GetRowCellValue(i, "tổ chức sự kiện").ToString().Trim(), 100).Split('[');
            //            //string[] _s11 = _s1[1].Split('[');
            //            objKH.cuacado = _s1[0].Trim();


            //            //if (_s1.Count() == 3)
            //            //objKH.tylechap = _s11[0].Trim().Replace(",", ".");
            //            //else if (_s1.Count() == 2)
            //            //    objKH.tylechap = _s1[1].Trim().Split('[')[0].Trim().Replace(",", ".");


            //            objKH.tylecuoc = getValue(grvKH.GetRowCellValue(i, "giá trị cược").ToString().Trim(), 100).Replace(",", ".");
            //            //string str1 = getValue(grvKH.GetRowCellValue(i, "tiền thưởng").ToString().Trim(), 100);
            //            ////str1 = str1.Replace(",", ".");
            //            //decimal d = decimal.Parse(str1, CultureInfo.InvariantCulture);
            //            objKH.tiencuoc = decimal.Parse(getValue(grvKH.GetRowCellValue(i, "tiền thưởng").ToString().Trim(), 100).Replace(",", "."), CultureInfo.InvariantCulture);
            //            objKH.tienthang_thua = decimal.Parse(getValue(grvKH.GetRowCellValue(i, "thắng thua").ToString().Trim(), 100).Replace(",", "."), CultureInfo.InvariantCulture);

            //            string _trangthai = getValue(grvKH.GetRowCellValue(i, "Trạng thái").ToString().Trim(), 100);
            //            if (_trangthai == "Won")
            //            {
            //                objKH.trangthai = "Thắng";
            //            }
            //            if (_trangthai == "Draw")
            //            {
            //                objKH.trangthai = "Hòa";
            //            }
            //            if (_trangthai == "Lose")
            //            {
            //                objKH.trangthai = "Thua";
            //            }
            //            if (_trangthai == "Rejected")
            //            {
            //                objKH.trangthai = "Từ chối";
            //            }
            //            if (_trangthai == "Cancel")
            //            {
            //                objKH.trangthai = getValue(grvKH.GetRowCellValue(i, "Trạng thái").ToString().Trim(), 100);
            //            }
            //            string _hinhthuccado = getValue(grvKH.GetRowCellValue(i + 1, "tổ chức sự kiện").ToString().Trim(), 100);
            //            if (_hinhthuccado == "Handicap")
            //            {
            //                objKH.hinhthuccado = "Kèo";
            //            }
            //            if (_hinhthuccado == "Over/Under")
            //            {
            //                objKH.hinhthuccado = "Tài - Xỉu";
            //            }
            //            if (_hinhthuccado == "Over/Under")
            //            {
            //                objKH.hinhthuccado = "Tài - Xỉu";
            //            }
            //            if (_hinhthuccado == "Correct Score")
            //            {
            //                objKH.hinhthuccado = "Kết quả chính xác";
            //            }
            //            objKH.com = decimal.Parse(getValue(grvKH.GetRowCellValue(i + 1, "thắng thua").ToString().Trim(), 100).Replace(",", "."), CultureInfo.InvariantCulture);

            //            string[] _s2 = getValue(grvKH.GetRowCellValue(i + 2, "tổ chức sự kiện").ToString().Trim(), 1000).Split('-');
            //            string[] _s3 = _s2[_s2.Count() - 1].Trim().Split('(');
            //            string _sv = "Corners";

            //            if (_sv.Contains(_s3[_s3.Count() - 1]) == false)
            //            {
            //                if (_s2[0].Trim() == "1h")
            //                {
            //                    objKH.doibong1 = _s2[1].Trim().Split('(')[0].Trim();
            //                    objKH.doibong2 = _s2[4].Trim().Split('(')[0].Trim();
            //                    objKH.hiepcado = "1h";
            //                }
            //                else
            //                {
            //                    objKH.doibong1 = _s2[0].Trim().Split('(')[0].Trim();
            //                    objKH.doibong2 = _s2[2].Trim().Split('(')[0].Trim();
            //                }
            //            }
            //            else
            //            {

            //                if (_s2[0].Trim() == "1h")
            //                {
            //                    objKH.doibong1 = _s2[1].Trim().Split(')')[0] + ")".Trim();
            //                    objKH.doibong2 = _s2[4].Trim().Split(')')[0] + ")".Trim();
            //                    objKH.hiepcado = "1h";
            //                }
            //                else
            //                {
            //                    objKH.doibong1 = _s2[0].Trim().Split(')')[0] + ")".Trim();
            //                    objKH.doibong2 = _s2[2].Trim().Split(')')[0] + ")".Trim();
            //                }
            //            }

            //            if (((int)_s3.Count() - 1) > 0)
            //            {
            //                objKH.cachthuccado = _s3[_s3.Count() - 1].Split(')')[0].Trim();
            //            }
            //            else
            //            {
            //                objKH.cachthuccado = "";
            //            }
            //            var _s4 = getValue(grvKH.GetRowCellValue(i + 2, "Thông tin").ToString().Trim(), 1000).Split(' ');
            //            string dat = _s4[0];
            //            objKH.ngay = DateTime.ParseExact(_s4[0].Trim() + "/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //            //string[] _s5 = _s4[1].Split(':');
            //            //int _gio;
            //            //if (_s4[1] == "CH")
            //            //    _gio = Convert.ToInt32(_s5[0]) + 12;
            //            //else
            //            //    _gio = Convert.ToInt32(_s5[0]);
            //            objKH.gio = TimeSpan.Parse(_s4[1]);
            //            var _s6 = getValue(grvKH.GetRowCellValue(i + 3, "tổ chức sự kiện").ToString().Trim(), 100).Trim();
            //            objKH.tengiaidau = getValue(grvKH.GetRowCellValue(i + 3, "tổ chức sự kiện").ToString().Trim(), 100).Trim();



            //            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            //            Calendar cal = dfi.Calendar;

            //            objKH.weekofyear = cal.GetWeekOfYear(objKH.ngay, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            //            objKH.thoigiancado = DateTime.ParseExact(_s4[0].Trim() + "/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture) + TimeSpan.Parse(_s4[1].Trim());

            //            if (_trangthai == "Won")
            //                objKH.ketqua_trongkeo = Convert.ToDecimal((objKH.tiencuoc + objKH.tienthang_thua));
            //            else if (_trangthai == "Lose" || _trangthai == "Draw")
            //                objKH.ketqua_trongkeo = Convert.ToDecimal(objKH.tiencuoc);
            //            else
            //                objKH.ketqua_trongkeo = 0;


            //            db.thongtinchungs.InsertOnSubmit(objKH);
            //            db.SubmitChanges();

            //            grvKH.SelectRow(i);
            //            grvKH.SelectRow(i + 1);
            //            grvKH.SelectRow(i + 2);
            //            grvKH.SelectRow(i + 3);
            //            grvKH.SelectRow(i + 4);


            //        }
            //        catch (Exception ex)
            //        {
            //            grvKH.SetRowCellValue(j, "Error", ex.Message);
            //            db = new CaDoDataContext();
            //        }
            //    }
            //    grvKH.DeleteSelectedRows();

            //    MessageBox.Show("Dữ liệu đã được lưu");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{

            //    db.Dispose();
            //}
        }
    }
}
