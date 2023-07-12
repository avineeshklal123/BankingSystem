using BankingSystem.Models;
using BankingSystem.Pages;
using Microsoft.EntityFrameworkCore;
namespace BankingSystem.Services
{
    public class LoanService
    {
        private readonly DataContext _ctx;

        private readonly int LoanDuration = 360;
        public LoanService(DataContext ctx)
        {

            _ctx = ctx;

        }

        public async Task<List<Loan>> GetLoans()
        {
            return await _ctx.Loans.ToListAsync();
        }

        public async Task<bool> AddLoanAsync (Loan loan)
        {
            await _ctx.Loans.AddAsync(loan);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<Loan> GetLoanbyID(int id)
        {
            Loan loan = await _ctx.Loans.FirstOrDefaultAsync(l => l.Id.Equals(id));
            return loan;

        }

       
        public async Task<List<CashFlow>> GetCashFlowsAsync(Loan loan)
        {
            
            decimal TotalAmount = loan.Amount * (1 + (loan.InterestRate / 100));      
            
            int NumberofInstal = LoanDuration / (loan.MonthFrequency * 30);
            decimal AmountperInstal = TotalAmount / NumberofInstal;
           
            //DateTime EndDate = loan.DateAdded.AddYears(1);
            List<CashFlow> Instal  = new List<CashFlow>();
            if (!loan.TaxLevied)
            {
                for (int i = 0; i < NumberofInstal; i++)
                {
                    CashFlow Temp = new CashFlow();
                    Temp.CashFlowName = loan.LoanName;
                    Temp.StartDate = loan.DateAdded.AddMonths(i * loan.MonthFrequency);
                    Temp.EndDate = loan.DateAdded.AddMonths((i + 1) * loan.MonthFrequency);
                    Temp.DateofPayment = Temp.StartDate;
                    Temp.TaxAmount = 0;
                    Temp.Amount = AmountperInstal;
                    Temp.Balance = i==NumberofInstal-1? AmountperInstal : TotalAmount - (i * AmountperInstal);
                    TimeSpan difference = Temp.EndDate - Temp.StartDate;
                    Temp.InterestDays = difference.Days;
                    Instal.Add(Temp);
                }
            }
            else
            {
                for (int i = 0; i < NumberofInstal; i++)
                {
                    CashFlow Temp = new CashFlow();
                    Temp.CashFlowName = loan.LoanName;
                    Temp.StartDate = loan.DateAdded.AddMonths(i * loan.MonthFrequency);
                    Temp.EndDate = loan.DateAdded.AddMonths((i + 1) * loan.MonthFrequency);
                    Temp.DateofPayment = Temp.StartDate;
                    Temp.TaxAmount = AmountperInstal * (loan.TaxRate / 100);
                    Temp.Amount = AmountperInstal*(1 + (loan.TaxRate / 100));
                    TimeSpan difference = Temp.EndDate - Temp.StartDate;
                    Temp.InterestDays = difference.Days;
                    Instal.Add(Temp);
                }
            }

            return Instal;

        }

        

    }
}
