using CashFlow.Application.UseCases.Expense.Register;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

// Testes de unidade
public class RegisterExpenseValidatorTest
{
    /*
     * Isso define esssa função, como uma função especial de teste, 
     * que será executada pelo xUnit
     */
    [Fact]
    public void Success()
    {
        /*
        * Arrange(preparação)
        * Parte que vamos configurar, criar as instâncias de tudo 
        * que a gente precisa para executar esse teste de unidade
        */
        var validator = new RegisterExpenseValidator();
        var request = new RequestRegisterExpenseJson
        {
            Title = "Aluguel",
            Description = "Aluguel do mês de Janeiro",
            Date = DateTime.Now.AddDays(-1),
            Amount = 1000,
            PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
        };

        /*
        * Act(ação)
        * Precisamos obter o resultado da validação
        */
        var result = validator.Validate(request);

        /*
         * Assert(afirmação)
         * Estamos esperando que o resultado da validação seja 
         * válido, ou seja, que não tenha erros de validação
         */
        Assert.True(result.IsValid);
    }
}
