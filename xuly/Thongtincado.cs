using DevExpress.XtraEditors;
using Novacode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xuly
{
    public partial class Thongtincado : Form
    {
        cobacEntities cado = new cobacEntities();
        doibong _doibong;
        giaidau _giaidau;
        private thongtinchung _thongtinchung;
        cachthuccado _cachthucado;
        public int? ID_Taikhoan;
        public string _masotaikhoan;
        private bool bool_sua = false;
        private bool bool_add = false;
        private int id_sua;
        public Thongtincado()
        {
            InitializeComponent();
        }
        private void FunSave()
        {
            #region check
            if (bool_add == false)
            {
                XtraMessageBox.Show("Chưa chọn chức năng thêm mới");
                return;
            }
            #endregion
            //kiem tra nhap
            try
            {
                ////////////////// thêm//////////////////////////
                if (bool_sua == false)
                {
                    _thongtinchung = new thongtinchung();
                    if (com_hiepcado.Text == "1h")
                    {
                        _thongtinchung.hiepcado = "1h";

                    }
                    if (com_hiepcado.Text == "Full time")
                    {
                        _thongtinchung.hiepcado = "";
                    }
                    if (com_hiepcado.Text == "15 mins")
                    {
                        _thongtinchung.hiepcado = "15 mins";
                    }

                    //////////////////// xử lý chuỗi
                    string chuoi = " - |-|- | -";
                    Regex myRegex = new Regex(chuoi);
                    string str_giaidau = Look_GiaiDau.Text.ToUpper() + "";
                    string[] cut_giaidau = myRegex.Split(str_giaidau);
                    string tengiaidau = cut_giaidau[0];
                    if (cut_giaidau.Count() == 2)
                    {

                        string tengiaiMoRong = cut_giaidau[1];
                        if (tengiaiMoRong == "SPECIALS" || tengiaiMoRong == "SPECIFIC 15 MINS")
                        {
                            _thongtinchung.giaidaumorong = tengiaiMoRong;
                        }
                        else _thongtinchung.giaidaumorong = "";
                    }
                    else _thongtinchung.giaidaumorong = "";

                    /////////////////////////////////////////////// SPECIALS|,-SPECIALS| - SPECIFIC 15 MINS|-SPECIFIC 15 MINS
                    //kiem tra giai dau ton tai
                    var l = cado.giaidau.SingleOrDefault(p => p.tengiaidau == str_giaidau);
                    if (l == null)
                    {
                        _giaidau = new giaidau();
                        string giai = (string)Look_GiaiDau.Text;
                        _giaidau.tengiaidau = giai.ToUpper();
                        cado.giaidau.Add(_giaidau);

                    }

                    var k = cado.doibong.SingleOrDefault(p => p.tendoibong == (string)Look_TenDoi1.EditValue);
                    if (k == null)
                    {
                        _doibong = new doibong();
                        _doibong.tendoibong = (string)Look_TenDoi1.EditValue;
                        cado.doibong.Add(_doibong);
                    }

                    var m = cado.doibong.SingleOrDefault(p => p.tendoibong == (string)Look_TenDoi2.EditValue);
                    if (m == null)
                    {
                        _doibong = new doibong();
                        _doibong.tendoibong = (string)Look_TenDoi2.EditValue;
                        cado.doibong.Add(_doibong);
                    }
                    if (Look_CachThucCaDo.EditValue != null)
                    {
                        var n = cado.cachthuccado.SingleOrDefault(p => p.tencachthuccado == (string)Look_CachThucCaDo.EditValue);
                        if (n == null)
                        {
                            _cachthucado = new cachthuccado();
                            _cachthucado.tencachthuccado = (string)Look_CachThucCaDo.EditValue;
                            cado.cachthuccado.Add(_cachthucado);
                        }
                    }
                    _thongtinchung.id_quanlyTK_idTK = (int)ID_Taikhoan;
                    _thongtinchung.ngay = (DateTime)date_ThoiGianCaDo.EditValue;
                    _thongtinchung.gio = (TimeSpan)date_gio.EditValue;
                    _thongtinchung.thoigiancado = (DateTime)date_ThoiGianCaDo.EditValue + (TimeSpan)date_gio.EditValue;
                    _thongtinchung.cuacado = txt_HinhThucCaDo.Text;
                    //_thongtinchung.tylechap = Spin_TyLeChap.Text;
                    _thongtinchung.hinhthuccado = Com_DangCaDo.Text;
                    if (Look_CachThucCaDo.Text == "")
                    {

                        _thongtinchung.cachthuccado = "";
                    }
                    else
                    {
                        _thongtinchung.cachthuccado = Look_CachThucCaDo.Text;
                    }
                    _thongtinchung.tengiaidau = tengiaidau;
                    _thongtinchung.doibong1 = Look_TenDoi1.Text;
                    _thongtinchung.doibong2 = Look_TenDoi2.Text;
                    _thongtinchung.tylecuoc = Spin_TyLeCuoc.Text;
                    _thongtinchung.tiencuoc = Spin_TienCuoc.Value;
                    _thongtinchung.tienthang_thua = Spin_KetQua.Value;
                    if (Com_trangThai.Text == "Thắng")
                    {
                        _thongtinchung.ketqua_trongkeo = Spin_TienCuoc.Value + Spin_KetQua.Value;
                    }
                    else
                    {
                        _thongtinchung.ketqua_trongkeo = Spin_TienCuoc.Value;
                    }
                    _thongtinchung.com = Spin_SoCom.Value;
                    _thongtinchung.trangthai = Com_trangThai.Text;

                    cado.thongtinchung.Add(_thongtinchung);
                }
                else
                {
                    ///////////////////////sửa/////////////////////////////////////////////////////////////////
                    var _thongtinchung = cado.thongtinchung.Where(a => a.idthongtin == id_sua).SingleOrDefault();
                    if (com_hiepcado.Text == "1h")
                    {
                        _thongtinchung.hiepcado = "1h";

                    }
                    if (com_hiepcado.Text == "Full time")
                    {
                        _thongtinchung.hiepcado = "";
                    }
                    if (com_hiepcado.Text == "15 mins")
                    {
                        _thongtinchung.hiepcado = "15 mins";
                    }

                    //////////////////// xử lý chuỗi
                    string chuoi = " - |-|- | -";
                    Regex myRegex = new Regex(chuoi);
                    string str_giaidau = Look_GiaiDau.Text.ToUpper() + "";
                    string[] cut_giaidau = myRegex.Split(str_giaidau);
                    string tengiaidau = cut_giaidau[0];
                    if (cut_giaidau.Count() == 2)
                    {

                        string tengiaiMoRong = cut_giaidau[1];
                        if (tengiaiMoRong == "SPECIALS" || tengiaiMoRong == "SPECIFIC 15 MINS")
                        {
                            _thongtinchung.giaidaumorong = tengiaiMoRong;
                        }
                        else _thongtinchung.giaidaumorong = "";
                    }
                    else _thongtinchung.giaidaumorong = "";

                    /////////////////////////////////////////////// SPECIALS|,-SPECIALS| - SPECIFIC 15 MINS|-SPECIFIC 15 MINS
                    //kiem tra giai dau ton tai
                    var l = cado.giaidau.SingleOrDefault(p => p.tengiaidau == str_giaidau);
                    if (l == null)
                    {
                        _giaidau = new giaidau();
                        string giai = (string)Look_GiaiDau.Text;
                        _giaidau.tengiaidau = giai.ToUpper();
                        cado.giaidau.Add(_giaidau);

                    }

                    var k = cado.doibong.SingleOrDefault(p => p.tendoibong == (string)Look_TenDoi1.EditValue);
                    if (k == null)
                    {
                        _doibong = new doibong();
                        _doibong.tendoibong = (string)Look_TenDoi1.EditValue;
                        cado.doibong.Add(_doibong);
                    }

                    var m = cado.doibong.SingleOrDefault(p => p.tendoibong == (string)Look_TenDoi2.EditValue);
                    if (m == null)
                    {
                        _doibong = new doibong();
                        _doibong.tendoibong = (string)Look_TenDoi2.EditValue;
                        cado.doibong.Add(_doibong);
                    }
                    if (Look_CachThucCaDo.EditValue != null)
                    {
                        var n = cado.cachthuccado.SingleOrDefault(p => p.tencachthuccado == (string)Look_CachThucCaDo.EditValue);
                        if (n == null)
                        {
                            _cachthucado = new cachthuccado();
                            _cachthucado.tencachthuccado = (string)Look_CachThucCaDo.EditValue;
                            cado.cachthuccado.Add(_cachthucado);
                        }
                    }
                    _thongtinchung.id_quanlyTK_idTK = (int)ID_Taikhoan;
                    _thongtinchung.ngay = (DateTime)date_ThoiGianCaDo.EditValue;
                    _thongtinchung.gio = (TimeSpan)date_gio.EditValue;
                    _thongtinchung.thoigiancado = (DateTime)date_ThoiGianCaDo.EditValue + (TimeSpan)date_gio.EditValue;
                    _thongtinchung.cuacado = txt_HinhThucCaDo.Text;
                    //_thongtinchung.tylechap = Spin_TyLeChap.Text;
                    _thongtinchung.hinhthuccado = Com_DangCaDo.Text;
                    if (Look_CachThucCaDo.Text == "")
                    {

                        _thongtinchung.cachthuccado = "";
                    }
                    else
                    {
                        _thongtinchung.cachthuccado = Look_CachThucCaDo.Text;
                    }
                    _thongtinchung.tengiaidau = tengiaidau;
                    _thongtinchung.doibong1 = Look_TenDoi1.Text;
                    _thongtinchung.doibong2 = Look_TenDoi2.Text;
                    _thongtinchung.tylecuoc = Spin_TyLeCuoc.Text;
                    _thongtinchung.tiencuoc = Spin_TienCuoc.Value;
                    _thongtinchung.tienthang_thua = Spin_KetQua.Value;
                    if (Com_trangThai.Text == "Thắng")
                    {
                        _thongtinchung.ketqua_trongkeo = Spin_TienCuoc.Value + Spin_KetQua.Value;
                    }
                    else
                    {
                        _thongtinchung.ketqua_trongkeo = Spin_TienCuoc.Value;
                    }
                    _thongtinchung.com = Spin_SoCom.Value;
                    _thongtinchung.trangthai = Com_trangThai.Text;
                    bool_sua = false;
                }
                cado.SaveChanges();
                XtraMessageBox.Show("Lưu thành công");
            }
            catch (Exception)
            {

                XtraMessageBox.Show("Lưu thông tin không thành công");
                throw;
            }

        }
        private void GetNull()
        {
            txt_HinhThucCaDo.EditValue = null;
            //Spin_TyLeChap.Text = null;
            Com_DangCaDo.EditValue = null;
            Look_CachThucCaDo.EditValue = null;
            Look_GiaiDau.EditValue = null;
            Look_TenDoi1.EditValue = null;
            Look_TenDoi2.EditValue = null;
            Spin_TyLeCuoc.Text = null;
            Spin_TienCuoc.Value = 0;
            Spin_KetQua.Value = 0;
            Spin_SoCom.Value = 0;
            Com_trangThai.EditValue = null;
        }
        private void ReLoad()
        {
            var ten = cado.proc_sotaikhoan(ID_Taikhoan).SingleOrDefault();
            txt_sotk.Text = ten;
            //var l = cado.proc_tendoibong().ToList();
            //com_hiepcado.SelectedIndex = 2;

            //Look_TenDoi1.Properties.Items.Clear();
            //Look_TenDoi2.Properties.Items.Clear();
            //txt_HinhThucCaDo.Properties.Items.Clear();
            //txt_HinhThucCaDo.Properties.Items.Add("Over");
            //txt_HinhThucCaDo.Properties.Items.Add("Under");
            //foreach (var i in l)
            //{
            //    Look_TenDoi1.Properties.Items.Add(i);
            //    Look_TenDoi2.Properties.Items.Add(i);
            //    txt_HinhThucCaDo.Properties.Items.Add(i);
            //}
            //var tengiaidau = cado.proc_tengiaidau().ToList();
            //Look_GiaiDau.Properties.Items.Clear();
            //foreach (var j in tengiaidau)
            //{
            //    Look_GiaiDau.Properties.Items.Add(j);
            //}
            //Look_CachThucCaDo.Properties.Items.Clear();
            //var cachthuc = cado.proc_cachthuccado().ToList();
            //foreach (var k in cachthuc)
            //{
            //    Look_CachThucCaDo.Properties.Items.Add(k);
            //}

            //var thongtin = (from f in cado.thongtinchung
            //                join e in cado.quanlyTK
            //                on f.id_quanlyTK_idTK equals e.idTK
            //                where f.id_quanlyTK_idTK == ID_Taikhoan
            //                select new
            //                {
            //                    e.idTK,

            //                    e.SoTK,
            //                    f.idthongtin,
            //                    f.visible,
            //                    //f.thoigiancado,
            //                    f.ngay,
            //                    f.gio,
            //                    tentrandau = f.doibong1 + " --vs-- " + f.doibong2,
            //                    gopthongtin = f.hiepcado + "  " + f.cuacado+ "\n" + f.hinhthuccado + "\n" + f.doibong1 + "(" + f.cachthuccado + ")" + "--vs--" + f.doibong2 + "(" + f.cachthuccado + ")" + "\n" + f.tengiaidau,
            //                    f.tylecuoc,
            //                    f.tiencuoc,
            //                    f.tienthang_thua,
            //                    f.com,
            //                    f.trangthai
            //                }
            //                        ).ToList();
            var thongtin = cado.thongtin(ID_Taikhoan).ToList();
            grd_bongda.DataSource = thongtin;
        }
        private void FunPrint()
        {
            try
            {

                //////////////////////////////////////////////////////////////////////////////


                var taikhoan = new quanlyTK();
                string fileName = @"C:\cadobongda\" + _masotaikhoan + ".doc";
                using (var doc = DocX.Create(fileName))
                {
                    var layngay = cado.thongtinchung.Select(p => new { p.ngay }).Distinct().ToList();
                    foreach (var laysotran in layngay)
                    {

                        var laydoi = (from f in cado.thongtinchung
                                      where f.id_quanlyTK_idTK == ID_Taikhoan && f.ngay == laysotran.ngay
                                      select new
                                      {
                                          doibong = f.doibong1 + f.doibong2,

                                      }
                               ).ToList();

                        var laysokeo = (from t in laydoi
                                        group t by t.doibong into th
                                        select new
                                        {
                                            sokeo = th.Key,
                                            soluot = th.Count()
                                        }
                                    ).Distinct().ToString();

                        doc.InsertParagraph("Kế quả cho ngày:" + laysotran.ngay.ToString("dd/MM/yyyy") + "số trận" + laydoi.Count());


                        //int a = 1;
                    }


                    doc.Save();
                }
                Process.Start("WINWORD.EXE", fileName);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void Thongtincado_Load(object sender, EventArgs e)
        {
            ReLoad();
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                bool_add = true;
                var i = gridView1.GetFocusedRowCellValue("idthongtin");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn kèo cần sửa");
                }
                else
                {
                    var sua = cado.thongtinchung.Where(a => a.idthongtin == (int)i).SingleOrDefault();
                    date_ThoiGianCaDo.EditValue = sua.ngay;
                    date_gio.EditValue = sua.gio;
                    if (sua.hiepcado == "")
                    {
                        com_hiepcado.EditValue = "Full time";
                    }
                    else
                    {
                        com_hiepcado.EditValue = sua.hiepcado;
                    }
                    txt_HinhThucCaDo.EditValue = sua.cuacado;
                    Com_DangCaDo.EditValue = sua.hinhthuccado;
                    Spin_KetQua.EditValue = sua.tienthang_thua;
                    Spin_TienCuoc.EditValue = sua.tiencuoc;
                    Spin_SoCom.EditValue = sua.com;
                    //Spin_TyLeChap.EditValue = sua.tylechap;
                    Spin_TyLeCuoc.EditValue = sua.tylecuoc;
                    Com_trangThai.EditValue = sua.trangthai;
                    Look_TenDoi1.EditValue = sua.doibong1;
                    Look_TenDoi2.EditValue = sua.doibong2;
                    Look_CachThucCaDo.EditValue = sua.cachthuccado;
                    if (sua.giaidaumorong != null)
                    {
                        Look_GiaiDau.EditValue = sua.tengiaidau + " - " + sua.giaidaumorong;
                    }
                    else
                    {
                        Look_GiaiDau.EditValue = sua.tengiaidau;
                    }

                    bool_sua = true;
                    id_sua = (int)i;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btn_save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FunSave();
            Spin_TyLeCuoc.Text = null;
            Spin_TienCuoc.Value = 0;
            Spin_KetQua.Value = 0;
            Spin_SoCom.Value = 0;
            Com_trangThai.EditValue = null;
        }

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var i = gridView1.GetFocusedRowCellValue("idthongtin");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn kèo cần xóa");
                }
                else
                {
                    cado.show_hide((int)i,"Hide");
                    //var xoathongtin = cado.thongtinchung.Where(a => a.idthongtin == (int)i).SingleOrDefault();
                    //cado.thongtinchung.Remove(xoathongtin);
                    cado.SaveChanges();
                    XtraMessageBox.Show("Xóa thành công");
                    //ReLoad();
                }

            }
            catch (Exception)
            {

                XtraMessageBox.Show("Xóa không thành công");
            }
        }

        private void btn_print_khach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var db = new cobacEntities();
                // Modify to suit your machine:
                string fileName = @"C:\Cadobongda\"+_masotaikhoan+"(khach).doc";

                // Create a document in memory:
                var doc = DocX.Create(fileName);

                var lngay = db.view_thongtinchung.Where(p => p.id_quanlyTK_idTK == ID_Taikhoan).Select(p => new { p.ngay }).Distinct().ToList();
                foreach (var ingay in lngay)
                {

                    //var SoTranTrongNgay = (from q in db.thongtinchung
                    //                       where q.ngay == (DateTime)ingay.ngay
                    //                       select new
                    //                       {
                    //                           DoiBong = q.doibong1 + q.doibong2
                    //                       }
                    //                       ).ToList();

                    var ngay3 = String.Format("{0:dd/MM/yyyy}", ingay.ngay);
                    //doc.InsertParagraph("Kế quả cho ngày:" + ngay3+"\n\n");

                    //danh sách các trận đấu tương ứng với số kèo trong 1 ngày 
                    var lkeo = (from t in db.view_thongtinchung
                                    join tk in db.view_quanlytk on t.id_quanlyTK_idTK equals tk.idTK
                                    where t.ngay == (DateTime)ingay.ngay && t.id_quanlyTK_idTK == this .ID_Taikhoan
                                    select new
                                    {
                                        doi = t.doibong1 + t.doibong2
                                    }).ToList();

                    
                        var lkeokeo = (from t in lkeo
                                       group t by t.doi into th

                                       select new
                                       {
                                           doi = th.Key,
                                           so = th.Count()

                                       }).ToList();

                    //thông tin chi tiết từng trận đấu trong ngày
                        var l = (from t in db.view_thongtinchung
                                 join tk in db.view_quanlytk on t.id_quanlyTK_idTK equals tk.idTK
                                 where t.ngay == (DateTime)ingay.ngay && t.id_quanlyTK_idTK ==this.ID_Taikhoan
                                 select new
                                 {
                                     tk.GiaUSDTK,
                                     tk.TenTkDayDU,
                                     tk.TenTk,
                                     tk.NguoiCaDoCung,
                                     t.ngay,
                                     t.doibong1,
                                     t.doibong2,
                                     t.tengiaidau,
                                     t.trangthai,
                                     t.hinhthuccado,
                                     t.hiepcado,
                                     t.tylechap,
                                     t.tylecuoc,
                                     t.cuacado,
                                     t.tiencuoc,
                                     doimoi = t.doibong1 + t.doibong2,
                                     t.com,
                                     t.gio,
                                     t.ketqua_trongkeo,
                                     chonkeocado=t.cuacado=="under" ? "Xỉu" : t.cuacado == "over" ? "Tài" : t.cuacado,//tên đội chưa có
                                     t.cachthuccado,
                                     
                                     tienthangthua=t.tienthang_thua <0 ? t.tienthang_thua*-1 : t.tienthang_thua,
                                     tuongungtienVNDthangthua = t.tienthang_thua < 0 ? t.tienthang_thua * tk.GiaUSDTK*-1 : t.tienthang_thua * tk.GiaUSDTK,
                                     tiencuocvnd = t.tiencuoc * tk.GiaUSDTK

                                 }).OrderBy(p => p.doimoi).ToList();

                    //đếm số trận đấu trong ngày
                    var sotran =lkeokeo.Count;
                    //forrmat ngày
                        var ngay1 = String.Format("{0:dd/MM/yyyy}", ingay.ngay);

                    //hiện tiêu đề
                        doc.InsertParagraph("Trong ngày " + ngay1 + " tôi có cá độ: "+sotran+" trận đấu gồm: ").Bold();

                    //sử dụng để đếm số kèo trong cùng 1 trận trong ngày, bắt đầu từ 1
                        var keothu = 1;
                    var hettran = false;
                    //số thứ tự
                        var tt = 1;
                    //bắt đầu hiện thị chi tiết trận đấu trong mỗi ngày
                        foreach (var i in l)
                        {
                        if (keothu == 1)


                        doc.InsertParagraph().Append(tt.ToString() +".").Bold().Append(" Trận đấu giữa hai đội " + i.doibong1 + " và " + i.doibong2 + " thuộc giải bóng đá: " + i.tengiaidau);
                           
                            //doc.InsertParagraph(" Trận đấu giữa hai đội " + i.doibong1 + " và " + i.doibong2 + " thuộc giải bóng đá: " + i.tengiaidau);
                        //sử dụng để lưu số kèo trong 1 trận, mặc đinh ban đầu là 1 trận 1 kèo
                        var keomax = 1;

                        foreach (var j in lkeokeo)
                        {
                            if (i.doibong1 + i.doibong2 == j.doi)
                            {
                                if (keothu == 1)
                                    doc.InsertParagraph("     - Số kèo đấu đặt cá độ: " + j.so + " kèo.");
                                keomax = j.so;
                            }
                        }
                        if (keomax >1)
                        {
                            doc.InsertParagraph("     + Kèo thứ " + keothu);
                        }
                       
                        if (keomax > keothu)
                        {
                            keothu++;
                        }
                        else
                        {
                            hettran = true;
                            keothu = 1;
                        }
                        var ngay = string.Format("{0:dd/MM/yyyy}", i.ngay);
                        doc.InsertParagraph("     - Thời gian đặt kèo (thời gian nhập lệnh) cá độ: " + i.gio + " ngày " + ngay);
                        if (i.trangthai== "Từ chối")
                        {
                            doc.InsertParagraph("     - Tình trạng kèo cá độ: Từ chối.");
                        }
                        else
                            doc.InsertParagraph("     - Tình trạng kèo cá độ: Chấp nhận và hoàn thành.");                      
                        if (i.hinhthuccado =="Tài - Xỉu")
                        {
                            string str1 = i.cachthuccado;
                            // tài xỉu phạt góc
                            if (str1.EndsWith("Corner") || str1.EndsWith("Corners"))
                            {
                                // hiệp 1
                                if (i.hiepcado == "1h")
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Tài - Xỉu phạt góc hiệp 1");
                                    // tổng số quả phạt góc
                                    if (i.cachthuccado == "No. of Corners")
                                    {
                                        doc.InsertParagraph("     - Chọn kèo cá độ và tỷ lệ chấp: " + i.chonkeocado + ", tỷ lệ thắng thua: " + i.tylecuoc);
                                    }
                                    // quả phạt góc thứ n
                                    else
                                    {
                                        // tính quả phạt góc thứ N
                                        string solanphatgoc = i.cachthuccado.Remove(1);
                                        doc.InsertParagraph("     - Chọn kèo cá độ: Dự đoán đội được hưởng lần phạt góc và tỷ lệ chấp " + i.cuacado + ", tỷ lệ thắng thua: " + i.tylecuoc);
                                    }
                                    
                                }
                                // cả trận
                                else
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Tài - Xỉu phạt góc cả trận");
                                    if (i.cachthuccado == "No. of Corners")
                                    {
                                        doc.InsertParagraph("     - Chọn kèo cá độ và tỷ lệ chấp: " + i.chonkeocado  + ", tỷ lệ thắng thua: " + i.tylecuoc);
                                    }
                                   
                                }
                                
                            }
                            // tài xỉu tổng số bàn thắng
                            else
                            {
                                //hiệp 1
                                if (i.hiepcado == "1h")
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Tài - Xỉu hiệp 1");
                                }
                                // thắng thua cả trận
                                if (i.hiepcado == "15 mins")
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Tài - Xỉu 15 phút");
                                }
                                if (i.hiepcado == "")
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Tài - Xỉu cả trận");
                                }
                                doc.InsertParagraph("     - Chọn kèo cá độ và tỷ lệ chấp: " + i.chonkeocado + ", tỷ lệ thắng thua: " + i.tylecuoc);
                            }
                        }

                        /////////////////////////////////hết tài xỉu - chuyển sang kèo
                        // cá độ kèo
                        if ( i.hinhthuccado =="Kèo")
                        {
                            string str = i.cachthuccado;
                            if (str.EndsWith("Corner") || str.EndsWith("Corners"))
                            {
                                if (i.cachthuccado != "No. of Corners")
                                {
                                
                                    if (i.hiepcado == "1h")
                                    {
                                        doc.InsertParagraph("     - Cá độ dưới hình thức: Kèo phạt góc hiệp 1");
                                    }
                                    else
                                    {
                                        doc.InsertParagraph("     - Cá độ dưới hình thức: Kèo phạt góc cả trận");
                                    }
                                    string solanphatgoc = i.cachthuccado.Remove(1);
                                    doc.InsertParagraph("     - Chọn kèo cá độ: Dự đoán đội được hưởng số lần phạt góc và tỷ lệ chấp " + i.cuacado + ", tỷ lệ thắng thua: " + i.tylecuoc);
                                }
                                
                            }
                            // kèo thắng thua
                            else
                            {
                                if (i.hiepcado == "1h")
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Kèo chọn đội thắng hiệp 1");
                                }
                                else
                                {
                                    doc.InsertParagraph("     - Cá độ dưới hình thức: Kèo chọn đội thắng cả trận");
                                }
                                doc.InsertParagraph("     - Chọn kèo cá độ: Chọn đội thắng và tỷ lệ chấp " + i.cuacado + ", tỷ lệ thắng thua: " + i.tylecuoc);
                            }
                        }

                        if ( i.hinhthuccado == "Kết quả chính xác")
                        {
                           
                            // kèo thắng thua
                          
                                 doc.InsertParagraph("     - Cá độ dưới hình thức: Kèo dự đoán kết quả chính xác");
                                
                                doc.InsertParagraph("     - Chọn kèo cá độ: Dự đoán kết quả " + i.cuacado + ", tỷ lệ thắng thua: " + i.tylecuoc);
                            
                        }
                       
                       // doc.InsertParagraph("     - Cá độ dưới hình thức:" + i.hinhthuccado);
                       // doc.InsertParagraph("     - Chọn kèo cá độ: " + i.chonkeocado+", tỷ lệ chấp: " + i.tylechap + " " + "(" + i.cachthuccado + " " + i.tylechap + ")" + ", tỷ lệ thắng thua: " + i.tylecuoc);
                        var tiencuocVND = String.Format("{0:N}", i.tiencuocvnd);
                        doc.InsertParagraph("     - Số tiền tham gia cá độ: " + i.tiencuoc + "$ " + "(Tương ứng với số tiền: " + tiencuocVND + "đ).");
                        if(i.trangthai == "Từ chối")
                        {
                            
                        }
                        else
                        {
                            doc.InsertParagraph("     - Số COM: " + i.com);
                            var tienthangthuaVND = String.Format("{0:N}", i.tuongungtienVNDthangthua);

                            if (i.trangthai == "Hòa")
                            {
                                doc.InsertParagraph("     - Kết quả: " + i.trangthai);
                            }

                            else
                            {
                                string vietthuong = i.trangthai.ToLower();
                                doc.InsertParagraph("     - Kết quả: " + i.trangthai + ", số tiền " + vietthuong + " cá độ: " + i.tienthangthua + "$ (tương ứng với số tiền: " + tienthangthuaVND + "đ)");
                            }

                            var tiencuoctrongkeoVND = String.Format("{0:N}", i.ketqua_trongkeo * i.GiaUSDTK);
                            if (keomax != 1)
                                doc.InsertParagraph("Như vậy trong kèo cá độ này số tiền tôi(" + i.TenTk + ") đánh bạc là: (" + i.ketqua_trongkeo + "$) tương ứng với số tiền (" + tiencuoctrongkeoVND + "đ)\n");
                        }
                        
                        if(hettran==true)
                        {
                            var tien = l.Where(p => p.doibong1 == i.doibong1 && p.doibong2 == i.doibong2).Where(a=>a.trangthai !="Từ chối").Sum(p => p.ketqua_trongkeo);
                            
                            var tiencuoctrongtranVND = String.Format("{0:N}", tien * i.GiaUSDTK);
                            doc.InsertParagraph("Như vậy trong trận này tổng số tiền tôi("+i.TenTk+ ") đánh bạc là: (" + tien + "$) tương ứng với số tiền (" + tiencuoctrongtranVND + "đ)\n");
                            tt++;
                            hettran = false;
                        }
                        
                    }
                    //kêt quả trong ngày của 1 tài khoản
                    var ketquatrongngay = l.Where(a => a.trangthai != "Từ chối").Sum(p => p.ketqua_trongkeo);
                    var tiencuoctrongngayVND = String.Format("{0:N}", ketquatrongngay * l.First().GiaUSDTK);
                    doc.InsertParagraph("Vậy, trong ngày: "+ngay3+" tổng số tiền mà tôi(" +l.First().TenTk + ") đánh bạc là: (" + ketquatrongngay + ") tương ứng với số tiền (" + tiencuoctrongngayVND + "đ)\n");
                }
                // Save to the output directory:
                doc.Save();
           
                Process.Start("WINWORD.EXE", fileName);

                }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReLoad();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void bar_recovery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var i = gridView1.GetFocusedRowCellValue("idthongtin");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn kèo cần khôi phục");
                }
                else
                {
                    cado.show_hide((int)i, "Show");
                    //var xoathongtin = cado.thongtinchung.Where(a => a.idthongtin == (int)i).SingleOrDefault();
                    //cado.thongtinchung.Remove(xoathongtin);
                    cado.SaveChanges();
                    XtraMessageBox.Show("khôi phục thành công");
                    //ReLoad();
                }

            }
            catch (Exception)
            {

                XtraMessageBox.Show("Khôi phục không thành công");
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string priority = gridView1.GetRowCellDisplayText(e.RowHandle, gridView1.Columns["visible"]);
              
                if (priority == "Hide")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }              
                if (priority == "Show")
                {
                    e.Appearance.BackColor = Color.White;
                    
                }
            }
        }

        private void bar_add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool_add = true;
            var l = cado.proc_tendoibong().ToList();
            com_hiepcado.SelectedIndex = 2;

            Look_TenDoi1.Properties.Items.Clear();
            Look_TenDoi2.Properties.Items.Clear();
            txt_HinhThucCaDo.Properties.Items.Clear();
            txt_HinhThucCaDo.Properties.Items.Add("Over");
            txt_HinhThucCaDo.Properties.Items.Add("Under");
            foreach (var i in l)
            {
                Look_TenDoi1.Properties.Items.Add(i);
                Look_TenDoi2.Properties.Items.Add(i);
                txt_HinhThucCaDo.Properties.Items.Add(i);
            }
            var tengiaidau = cado.proc_tengiaidau().ToList();
            Look_GiaiDau.Properties.Items.Clear();
            foreach (var j in tengiaidau)
            {
                Look_GiaiDau.Properties.Items.Add(j);
            }
            Look_CachThucCaDo.Properties.Items.Clear();
            var cachthuc = cado.proc_cachthuccado().ToList();
            foreach (var k in cachthuc)
            {
                Look_CachThucCaDo.Properties.Items.Add(k);
            }
            XtraMessageBox.Show("Đã có thể nhập thông tin cần thêm mới");
        }

        private void bar_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                bool_add = true;
                var i = gridView1.GetFocusedRowCellValue("idthongtin");
                if (i == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn kèo cần sửa");
                }
                else
                {
                    var sua = cado.thongtinchung.Where(a => a.idthongtin == (int)i).SingleOrDefault();
                    date_ThoiGianCaDo.EditValue = sua.ngay;
                    date_gio.EditValue = sua.gio;
                    if (sua.hiepcado == "")
                    {
                        com_hiepcado.EditValue = "Full time";
                    }
                    else
                    {
                        com_hiepcado.EditValue = sua.hiepcado;
                    }
                    txt_HinhThucCaDo.EditValue = sua.cuacado;
                    Com_DangCaDo.EditValue = sua.hinhthuccado;
                    Spin_KetQua.EditValue = sua.tienthang_thua;
                    Spin_TienCuoc.EditValue = sua.tiencuoc;
                    Spin_SoCom.EditValue = sua.com;
                    //Spin_TyLeChap.EditValue = sua.tylechap;
                    Spin_TyLeCuoc.EditValue = sua.tylecuoc;
                    Com_trangThai.EditValue = sua.trangthai;
                    Look_TenDoi1.EditValue = sua.doibong1;
                    Look_TenDoi2.EditValue = sua.doibong2;
                    Look_CachThucCaDo.EditValue = sua.cachthuccado;
                    if (sua.giaidaumorong != null)
                    {
                        Look_GiaiDau.EditValue = sua.tengiaidau + " - " + sua.giaidaumorong;
                    }
                    else
                    {
                        Look_GiaiDau.EditValue = sua.tengiaidau;
                    }

                    bool_sua = true;
                    id_sua = (int)i;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grd_bongda_Click(object sender, EventArgs e)
        {

        }
    }
}
