namespace Domain.Exceptions
{
    public class LoanNotFoundException : BaseException
    {
        public LoanNotFoundException(int loanId)
            : base($"Loan with ID {loanId} was not found.", "LOAN_NOT_FOUND")
        {
        }
    }
}
