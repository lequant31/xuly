﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class cobacEntities : DbContext
    {
        public cobacEntities()
            : base("name=cobacEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cachthuccado> cachthuccado { get; set; }
        public virtual DbSet<doibong> doibong { get; set; }
        public virtual DbSet<giaidau> giaidau { get; set; }
        public virtual DbSet<quanlyTK> quanlyTK { get; set; }
        public virtual DbSet<thongtinchung> thongtinchung { get; set; }
        public virtual DbSet<view_quanlytk> view_quanlytk { get; set; }
        public virtual DbSet<view_thongtinchung> view_thongtinchung { get; set; }
    
        public virtual ObjectResult<string> proc_cachthuccado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_cachthuccado");
        }
    
        public virtual ObjectResult<string> proc_sotaikhoan(Nullable<int> sotk)
        {
            var sotkParameter = sotk.HasValue ?
                new ObjectParameter("sotk", sotk) :
                new ObjectParameter("sotk", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_sotaikhoan", sotkParameter);
        }
    
        public virtual ObjectResult<string> proc_tendoibong()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_tendoibong");
        }
    
        public virtual ObjectResult<string> proc_tengiaidau()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_tengiaidau");
        }
    
        public virtual int show_hide(Nullable<int> idthongtin, string visible)
        {
            var idthongtinParameter = idthongtin.HasValue ?
                new ObjectParameter("idthongtin", idthongtin) :
                new ObjectParameter("idthongtin", typeof(int));
    
            var visibleParameter = visible != null ?
                new ObjectParameter("visible", visible) :
                new ObjectParameter("visible", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("show_hide", idthongtinParameter, visibleParameter);
        }
    
        public virtual ObjectResult<thongtin_Result> thongtin(Nullable<int> idtk)
        {
            var idtkParameter = idtk.HasValue ?
                new ObjectParameter("idtk", idtk) :
                new ObjectParameter("idtk", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<thongtin_Result>("thongtin", idtkParameter);
        }
    
        public virtual ObjectResult<thongtincado_Result> thongtincado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<thongtincado_Result>("thongtincado");
        }
    
        public virtual ObjectResult<timkiem_Result> timkiem(string sotk)
        {
            var sotkParameter = sotk != null ?
                new ObjectParameter("sotk", sotk) :
                new ObjectParameter("sotk", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<timkiem_Result>("timkiem", sotkParameter);
        }
    
        public virtual ObjectResult<tinhtong_Result> tinhtong(Nullable<int> idtk, Nullable<decimal> tygia)
        {
            var idtkParameter = idtk.HasValue ?
                new ObjectParameter("idtk", idtk) :
                new ObjectParameter("idtk", typeof(int));
    
            var tygiaParameter = tygia.HasValue ?
                new ObjectParameter("tygia", tygia) :
                new ObjectParameter("tygia", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tinhtong_Result>("tinhtong", idtkParameter, tygiaParameter);
        }
    }
}
