namespace Domain.Exceptions
{
    public class LoanNotFoundException : BaseException
    {
        public LoanNotFoundException(Guid loanId)
            : base($"Loan with ID {loanId} was not found.", "LOAN_NOT_FOUND")
        {
        }
    }
}
