//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasVidaWebMVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class User
    {
        public User()
        {
            this.Transactions = new HashSet<Transaction>();
            this.Audits = new HashSet<Audit>();
        }

        [Key]
        public int UserID { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Apellido")]
        public string LastName { get; set; }

        public string DNI { get; set; }

        [DisplayName("Direccion")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Telefono")]
        public string Phone { get; set; }

        [DisplayName("Nacimiento")]
        public Nullable<System.DateTime> Birthday { get; set; }

        [DisplayName("Nombre de Usuario")]
        public string UserName { get; set; }

        [DisplayName("Contrasena")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public Nullable<int> ProductID { get; set; }

        [DisplayName("Activo")]
        public bool IsActive { get; set; }

        [DisplayName("Tipo de Usuario")]
        public int UserTypeID { get; set; }

        public Nullable<int> FamilyGroupID { get; set; }

        [DisplayName("Fecha de Creacion")]
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<int> LastTransactionID { get; set; }

        [DisplayName("Saldo")]
        public Nullable<double> AccountTotal { get; set; }

        public virtual FamilyGroup FamiliesGroup { get; set; }
        public virtual Product Product { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
    }
}
