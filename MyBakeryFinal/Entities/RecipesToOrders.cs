using System.ComponentModel.DataAnnotations.Schema;

namespace MyBakeryFinal.Entities
{
    public class RecipesToOrders : BaseEntity
    {
     
        public int Recipe_ID { get; set; }
        public int Order_ID { get; set; }


        [ForeignKey("Recipe_ID")]
        public virtual Recipe Recipe { get; set; }

        [ForeignKey("Order_ID")]
        public virtual Order Order { get; set; }
    }
}
