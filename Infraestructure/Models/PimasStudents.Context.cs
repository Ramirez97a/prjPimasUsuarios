﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Pimas_PlataformaEntities1 : DbContext
    {
        public Pimas_PlataformaEntities1()
            : base("name=Pimas_PlataformaEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tematicas> Tematicas { get; set; }
        public virtual DbSet<TipoAssets> TipoAssets { get; set; }
        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<AssetsGroup> AssetsGroup { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Downloads> Downloads { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<GroupT> GroupT { get; set; }
        public virtual DbSet<Invitation> Invitation { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Requesters> Requesters { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersGroupT> UsersGroupT { get; set; }
        public virtual DbSet<Wall> Wall { get; set; }
    }
}
