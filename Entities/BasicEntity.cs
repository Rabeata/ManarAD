using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entities
{
   public class BasicEntity
    {
        public BasicEntity()
        {
            this.UpdatedAt = DateTime.Now;
            if (this.CreatedAt == DateTime.MinValue)
            {
                this.CreatedAt = DateTime.Now;
            }
        }

        [Column("id")]
        [System.ComponentModel.DataAnnotations.Key]
        public int? Id { get; set; }

 
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }


        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("deleted_by")]
        public int? DeletedBy { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        public void Parse<T>(T source)
        {

            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead &&
                                                        prop.CanWrite &&
                                                        prop.Name.ToLower() != "id" &&
                                                        prop.Name.ToLower() != "createdat"// &&
                                                                                          //prop.Name.ToLower() != "updatedat"
                                                        );

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)

                    if (value.GetType() == typeof(int))
                    {
                        if (Convert.ToInt32(value) > 0)
                        {
                            prop.SetValue(this, value, null);
                        }
                    }
                    else
                    {
                        prop.SetValue(this, value, null);
                    }
            }
        }
    }
}
