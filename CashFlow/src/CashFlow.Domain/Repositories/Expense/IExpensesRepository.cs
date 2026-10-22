using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expense;
public interface IExpensesRepository
{
    void Add(CashFlow.Domain.Entities.Expense expense);

}
