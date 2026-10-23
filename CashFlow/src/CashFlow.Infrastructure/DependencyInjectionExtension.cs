using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    // Como a classe ExpensesRepository é internal, program não consegue
    // usa-lo, por isso esse método que recebe um serviceCollection foi
    // implementado
    
    /*
     * Metódo de extensão, quando definimos um tipo string ele já vem como várias 
     * funções como toString(), trim() e etc. Quando temos um método e recebemos
     * uma string por parâmetro com this, podemos extender esse método, ou seja,
     * a string passa a ter esse método como trim(), StartWith e etc.
     */
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRespositories(services);
    }

    private static void AddRespositories(IServiceCollection services)
    {
        // Configuração do scoped
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        // Obtém a string de conexão no appsettings.Developmenbuilder
        var connectionString = configuration.GetConnectionString("Connection");
        
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 46));

        services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
