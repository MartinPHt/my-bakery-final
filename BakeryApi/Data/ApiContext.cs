using Microsoft.EntityFrameworkCore;
using BakeryApi.Entities;

namespace BakeryApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) 
            : base(options)
        {

        }
    }
}
