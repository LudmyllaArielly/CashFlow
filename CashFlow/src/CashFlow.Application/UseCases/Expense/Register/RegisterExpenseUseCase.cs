using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expense.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;

    public RegisterExpenseUseCase(IExpensesRepository repository)
    {
        _repository = repository;
    }

    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = new CashFlow.Domain.Entities.Expense
        {
            Title = request.Title,
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };

        _repository.Add(entity);

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
