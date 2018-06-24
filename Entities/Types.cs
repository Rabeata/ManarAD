using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    [Table("types")]
    public class Types:BasicEntity
    {


        [Column("title")]
        public string Title { get; set; }

        virtual public IEnumerable<Prints> Prints { get; set; }



    }

}
