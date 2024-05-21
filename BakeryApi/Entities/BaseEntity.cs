using System.ComponentModel.DataAnnotations;

namespace BakeryApi.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
