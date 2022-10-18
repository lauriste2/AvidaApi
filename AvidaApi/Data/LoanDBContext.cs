using AvidaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AvidaApi.Data
{
    public class LoanDBContext : DbContext
    {
        public LoanDBContext(DbContextOptions<LoanDBContext> options)
            : base(options)

        {
        }

        public DbSet<AdressModel> Adress { get; set; }
        public DbSet<LoanApplicationModel> LoanApplication { get; set; }

        public DbSet<LoanModel> LoanModel { get; set; }

        public DbSet<PersonModel> Person { get; set; }
    }
}
