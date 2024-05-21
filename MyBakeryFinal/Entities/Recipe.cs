using MyBakeryFinal.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBakeryFinal.Entities
{
    public class Recipe : BaseEntity
    {
        public int Baker_ID { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }


        [ForeignKey("Baker_ID")]
        public virtual Baker Baker { get; set; }
    }
}
