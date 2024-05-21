using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryApi.Entities
{
    public class Order : BaseEntity
    {
        public int Customer_ID { get; set; }
        public string Details { get; set; }

        public double Tip { get; set; }

        public bool isExpress { get; set; }


        [ForeignKey("Customer_ID")]
        public virtual Customer Customer { get; set; }

       
    }
}
