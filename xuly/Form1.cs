using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xuly
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        cobacEntities _cobacEntity = new cobacEntities();
        quanlyTK _QuanLyTK = new quanlyTK();
        private bool bool_sua = false;
        private int id_sua;
        private void FunSave()
        {
            if (bool_sua == true)
            {
                var sua = _cobacEntity.quanlyTK.Where(a => a.idTK == id_sua).SingleOrDefault();
                sua.SoTK = txt_SoTK.Text;
                sua.TenTk = txt_Ten.Text;
                sua.TenTkDayDU = txt_HoVaTen.Text;
                sua.GiaUSDTK = Spin_GiaUSD.Value;
                sua.NguoiCaDoCung = txt_cadovoi.Text;
                sua.TenNguoiCaDoCung = txt_TenNguoiCaDoCung.Text;
                sua.FullNameDailyCap1 = txt_tendailycap1.Text;
                sua.TenDailyCap1 = txt_tenthuonggoidailycap1.Text;
                sua.GiaUSDDailyCap1 = spin_tygia2.Value;
                bool_sua = false;
            }
            else
            {
                if (txt_SoTK.Text != "" || txt_SoTK.EditValue != null)
                {
                    _QuanLyTK.SoTK = txt_SoTK.Text;
                    var kiemtra = _cobacEntity.quanlyTK.FirstOrDefault(a => a.SoTK.Equals(txt_SoTK.Text));
                    _QuanLyTK.TenTk = txt_Ten.Text;
                    _QuanLyTK.TenTkDayDU = txt_HoVaTen.Text;
                    _QuanLyTK.GiaUSDTK = Spin_GiaUSD.Value;
                    _QuanLyTK.NguoiCaDoCung = txt_cadovoi.Text;
                    _QuanLyTK.TenNguoiCaDoCung = txt_TenNguoiCaDoCung.Text;
                    _QuanLyTK.FullNameDailyCap1 = txt_tendailycap1.Text;
                    _QuanLyTK.TenDailyCap1 = txt_tenthuonggoidailycap1.Text;
                    _QuanLyTK.GiaUSDDailyCap1 = spin_tygia2.Value;
                    if (kiemtra == null)
                    {
                        _cobacEntity.quanlyTK.Add(_QuanLyTK);
                    }
                    else
                    {
                        XtraMessageBox.Show("Tài khoản đã tồn tại");
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Không để trống số tài khoản");
                }
            }

            _cobacEntity.SaveChanges();
            XtraMessageBox.Show("Lưu thông tin thành công");
        }
        private void FunNull()
        {

            txt_SoTK.EditValue = null;
            txt_HoVaTen.EditValue = null;
            txt_Ten.EditValue = null;
            Spin_GiaUSD.Value = 0;
            txt_cadovoi.EditValue = null;
            txt_TenNguoiCaDoCung.EditValue = null;
        }
        private void ReLoad()
        {
            try
            {
                var data = _cobacEntity.thongtincado()
                    //(from d in _cobacEntity.quanlyTK

                    //        select new
                    //        {
                    //            d.idTK,
                    //            d.SoTK,
                    //            d.TenTk,
                    //            d.TenTkDayDU,
                    //            d.GiaUSDTK,
                    //            d.NguoiCaDoCung,
                    //            d.TenNguoiCaDoCung,
                    //            tongketquatrongkeo = d.thongtinchung.Count > 0 ? d.thongtinchung.Where(p => p.ketqua_trongkeo > 0 && p.visible == "Show").Sum(p => p.ketqua_trongkeo) : 0,
                    //            tienthang = d.thongtinchung.Where(p => p.tienthang_thua > 0 && p.visible == "Show").Count() > 0 ? d.thongtinchung.Where(p => p.tienthang_thua > 0 && p.visible == "Show").Sum(p => p.tienthang_thua) : 0,
                    //            tienthua = d.thongtinchung.Where(p => p.tienthang_thua < 0 && p.visible == "Show").Count() > 0 ? d.thongtinchung.Where(p => p.tienthang_thua < 0 && p.visible == "Show").Sum(p => p.tienthang_thua) : 0,
                    //            tongtiencuoc = d.thongtinchung.Where(p => p.tiencuoc > 0 && p.trangthai != "Từ chối" && p.visible == "Show").Count() > 0 ? d.thongtinchung.Where(p => p.tiencuoc > 0 && p.trangthai != "Từ chối" && p.visible == "Show").Sum(p => p.tiencuoc) : 0,
                    //            socom = d.thongtinchung.Where(p => p.com >= 0 && p.visible == "Show").Count() > 0 ? d.thongtinchung.Where(p => p.com >= 0 && p.visible == "Show").Sum(p => p.com) : 0,
                    //            thangthua = d.thongtinchung.Count > 0 ? d.thongtinchung.Where(p => p.visible == "Show").Sum(p => p.tienthang_thua) : 0,
                    //        })
                            .ToList();
                Grd_TaiKhoan.DataSource = data;
            }
            catch
            {

                XtraMessageBox.Show("Kết nối mạng không ổn định");
            }

           
        }
        private void btn_import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ImportExcel importExcel = new ImportExcel();
            importExcel.ShowDialog();
        }

        private void btn_account_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FunNull();
        }

        private void Btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FunSave();
            FunNull();
            ReLoad();
        }

        private void Grv_TaiKhoan_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var i = Grv_TaiKhoan.GetFocusedRowCellValue("idTK");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn tài khoản cần sửa");
                }
                else
                {
                    var sua = _cobacEntity.quanlyTK.Where(a => a.idTK == (int)i).SingleOrDefault();
                    txt_SoTK.EditValue = sua.SoTK;
                    txt_HoVaTen.EditValue = sua.TenTkDayDU;
                    txt_Ten.EditValue = sua.TenTk;
                    if (sua.GiaUSDTK == null)
                    {
                        Spin_GiaUSD.Value =0;
                    }
                    else
                    {
                        Spin_GiaUSD.Value = (Decimal)sua.GiaUSDTK;
                    }
                    txt_tenthuonggoidailycap1.EditValue = sua.TenDailyCap1;
                    txt_tendailycap1.EditValue = sua.FullNameDailyCap1;
                    spin_tygia2.EditValue = sua.GiaUSDDailyCap1;
                    txt_cadovoi.EditValue = sua.NguoiCaDoCung;
                    txt_TenNguoiCaDoCung.EditValue = sua.TenNguoiCaDoCung;
                    bool_sua = true;
                    id_sua = (int)i;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReLoad();
            FunNull();
        }

        private void btn_thongtincado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var i = Grv_TaiKhoan.GetFocusedRowCellValue("idTK");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn tài khoản cần nhập và xem thông tin");
                }
                else
                {
                    var masotaikhoan = _cobacEntity.quanlyTK.Where(a => a.idTK == (int)i).SingleOrDefault();
                    ;
                    Thongtincado thongtincado = new Thongtincado();
                    thongtincado.ID_Taikhoan = (int)i;
                    thongtincado._masotaikhoan = masotaikhoan.SoTK;
                    thongtincado.ShowDialog();

                }
            }
            catch (Exception)
            {

                XtraMessageBox.Show("Chưa nhập đủ thông tin");
            }
        }

        private void btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var i = Grv_TaiKhoan.GetFocusedRowCellValue("idTK");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn tài khoản cần sửa");
                }
                else
                {
                    var sua = _cobacEntity.quanlyTK.Where(a => a.idTK == (int)i).SingleOrDefault();
                    txt_SoTK.EditValue = sua.SoTK;
                    txt_HoVaTen.EditValue = sua.TenTkDayDU;
                    txt_Ten.EditValue = sua.TenTk;
                    if (sua.GiaUSDTK == null)
                    {
                        Spin_GiaUSD.Value = 0;
                    }
                    else
                    {
                        Spin_GiaUSD.Value = (Decimal)sua.GiaUSDTK;
                    }

                    txt_cadovoi.EditValue = sua.NguoiCaDoCung;
                    txt_TenNguoiCaDoCung.EditValue = sua.TenNguoiCaDoCung;
                    txt_tenthuonggoidailycap1.EditValue = sua.TenDailyCap1;
                    txt_tendailycap1.EditValue = sua.FullNameDailyCap1;
                    spin_tygia2.EditValue = sua.GiaUSDDailyCap1;
                    bool_sua = true;
                    id_sua = (int)i;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_thongkechitiet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            thongkechitiet thongkechitiet = new thongkechitiet();
            try
            {
                var i = Grv_TaiKhoan.GetFocusedRowCellValue("idTK");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn tài khoản cần xem");
                }
                else
                {
                    var sua = _cobacEntity.quanlyTK.Where(a => a.idTK == (int)i).SingleOrDefault();
                    thongkechitiet.idtk = (int)i;
                    if (sua.GiaUSDTK == null)
                    {
                        Spin_GiaUSD.Value = 0;
                    }
                    else
                    {
                        thongkechitiet.tygia = (Decimal)sua.GiaUSDTK;
                    }
                    

                }
                

            }
            catch (Exception)
            {

                throw;
            }

            
            thongkechitiet.ShowDialog();
        }

        private void Grv_TaiKhoan_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
