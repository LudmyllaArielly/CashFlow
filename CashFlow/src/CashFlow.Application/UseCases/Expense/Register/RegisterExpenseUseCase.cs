using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expense.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        return new ResponseRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request) 
    { 
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(p => p.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
