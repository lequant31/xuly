//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xuly
{
    using System;
    using System.Collections.Generic;
    
    public partial class thongtinchung
    {
        public int idthongtin { get; set; }
        public int id_quanlyTK_idTK { get; set; }
        public System.DateTime thoigiancado { get; set; }
        public System.TimeSpan gio { get; set; }
        public System.DateTime ngay { get; set; }
        public string cuacado { get; set; }
        public string cachthuccado { get; set; }
        public string tylechap { get; set; }
        public string hinhthuccado { get; set; }
        public string doibong1 { get; set; }
        public string doibong2 { get; set; }
        public string tengiaidau { get; set; }
        public string giaidaumorong { get; set; }
        public string tylecuoc { get; set; }
        public Nullable<decimal> tiencuoc { get; set; }
        public Nullable<decimal> tienthang_thua { get; set; }
        public Nullable<decimal> ketqua_trongkeo { get; set; }
        public Nullable<decimal> com { get; set; }
        public string trangthai { get; set; }
        public string hiepcado { get; set; }
        public Nullable<int> weekofyear { get; set; }
        public string visible { get; set; }
    
        public virtual quanlyTK quanlyTK { get; set; }
    }
}
