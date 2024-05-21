using System.ComponentModel.DataAnnotations;

namespace BakeryApi.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double AccountBalance { get; set; }
        public bool DeluxeAccount { get; set; }
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders;
    }
}
