﻿

//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace MasVidaWebMVC
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class MasVidaDataContext : DbContext
{
    public MasVidaDataContext()
        : base("name=MasVidaDataContext")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public DbSet<FamilyGroup> FamiliesGroups { get; set; }

    public DbSet<Parameter> Parameters { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<TransactionType> TransactionsTypes { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserType> UsersTypes { get; set; }

    public DbSet<Audit> Audits { get; set; }

    public DbSet<sysdiagram> sysdiagrams { get; set; }

    public DbSet<Resource> Resources { get; set; }

    public DbSet<UserTypes_Resources> UserTypes_Resources { get; set; }

}

}

