using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    // Como a classe ExpensesRepository é internal, program não consegue
    // usa-lo, por isso esse método que recebe um serviceCollection foi
    // implementado
    public static void AddInfrastructure(this IServiceCollection services)
    {
        // Configuração do scoped
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
    }
}
