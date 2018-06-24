using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    [Table("prints")]
    public class Prints:BasicEntity
    {


        [Column("company_id")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("type_id")]
        public int? TypeId { get; set; }
        public Types Type { get; set; }

        [Column("height")]
        public float Height { get; set; }

        [Column("width")]
        public float Width { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("pnumber")]
        public int? Pnumber { get; set; }


        [NotMapped]
        public float Area { get; set; }


    }

}
