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
        * que a gente precisa para executar esse teste de unidade.
        * 
        * Esses dados fixos aqui não é muito legal, vai gerar dois problemas.
        * Primeiro, o código é hardcoded, ou seja, serão sempre fixos.
        * Segundo, futuramente vamos querer compartilhar esse código, essa
        * instância dessa request com outros testes.
        * 
        * Vamos ter um projeto de teste para testar só os validators,
        * outro projeto para testar só os usecases. Então tem esses 
        * outros tipos. Futuramente quando criamos um teste para o nosso
        * usecase vão precisar receber uma instância de um request, porque
        * vamos testar o executar e essa função recebe uma instância de request. 
        * Então não quero criar uma nova instância de request em cada teste. E se um dia algum coisa mudar,
        * por exemplo, não ter mais a descrição, vamos precisar mudar em um só lugar.
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
