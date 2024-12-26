namespace Domain.Exceptions
{
    public class LoanNotFoundException(int loanId) 
        : BaseException($"Loan with ID {loanId} was not found.", "LOAN_NOT_FOUND");
}
