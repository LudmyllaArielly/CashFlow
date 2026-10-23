using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesRepository
{
    private readonly CashFlowDbContext _dbcontext;

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    /*
    * Essa linha "_dbcontext.SaveChanges()" pode criar alguns problemas. 
    * O saveChanges vai persistir de fato no banco de dados ele executa a 
    * query e modifica no banco de dados seja para inserir, editar, excluir e etc.
    * Imagine que temos um segundo addAdress e o usuário já foi persistido,
    * quando foi chamar o addAdress por um descuido não percebeu que tinha uma
    * propriedade nula e no banco de dados essa coluna não pode ser nula e na hora
    * de persistir isso deu erro e gerou uma exceção. Como gerou uma exceção no segundo
    * saveChanges a gente vai ficar no banco de dados com um usuário com dado inválido porque
    * todo usuário deveria ter um endereço.
    * Vamos criar uma classe chamada unidade de trabalho para mover o saveChanges
    * para lá. E a regra de negócio que vai ser responsável para chamar esse saveChanges.
    * E fazemos apenas um saveChanges no final, se tiver algum erro com user ou se
    * tiver algum erro com address nenhum dos dois vai ser persistido. Porque o entityFramework
    * vai iniciar um begin transaction e se tudo der certo fazemos o commit se não rollback.
    * 
    */
    public void Add(Expense expense)
    {
        _dbcontext.Expenses.Add(expense);
    }
}
