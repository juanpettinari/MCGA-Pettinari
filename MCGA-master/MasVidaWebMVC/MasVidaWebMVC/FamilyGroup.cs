
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

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    
public partial class FamilyGroup
{

    public FamilyGroup()
    {

        this.Users = new HashSet<User>();

    }


    public int FamilyGroupID { get; set; }
        [Required]
    public string FamilyName { get; set; }



    public virtual ICollection<User> Users { get; set; }

}

}
