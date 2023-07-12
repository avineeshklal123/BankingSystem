using Microsoft.EntityFrameworkCore;
using static BankingSystem.Models.Loan;

namespace BankingSystem.Models
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
                
        }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<CashFlow> CashFlows { get; set;}
    }
}
