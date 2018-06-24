using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class ApplicationUser:IdentityUser<Int64>
    {


        [Column("full_name")]
        public string fullName { get; set; }

        [Column("language")]
        public string Language { get; set; }

        [Column("roles_string")]
        public string RolesAsString { get; set; }


        [NotMapped]
        public string[] newRoles
        {
            get
            {
                if (!string.IsNullOrEmpty(this.RolesAsString))
                    return this.RolesAsString.Split(',');
                return new string[] { };
            }
            set
            {
                if (value != null && value.Count() > 0)
                {
                    RolesAsString = string.Join(",", value);
                }
            }
        }

        [NotMapped]
        public string Password { get; set; }

 


        public ApplicationUser()
        {
           
        }
    }
}
