using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    [Table("company")]
    public class Company:BasicEntity
    {


        [Column("title")]
        public string Title { get; set; }

        [Column("budget")]
        public string Budget { get; set; }

       
        [Column("notes")]
        public string Notes { get; set; }

        virtual public IEnumerable<Prints> Prints { get; set; }

    }

}
