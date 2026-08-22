using CashFlow.Application.UseCases.Expense.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Expenses.Register;

// Testes de unidade
public class RegisterExpenseValidatorTest
{
    /*
     * Isso define essa função, como uma função especial de teste, 
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
        var request = RequestRegisterExpenseJsonBuilder.Build();

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
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Title_Empty()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Forçar o título ser vazio
        request.Title = string.Empty;

        // Act
        var result = validator.Validate(request);

        // Assert
        /*
         * Veja que ele não reclamou do ResourceErrorMessages e no meu projeto
         * Validators.Tests não tem referência para o projeto 
         * CashFlow.Exception, mas tem referência com projeto de Application
         * e as nossas exceptions são públicas. Então a partir de application
         * ele consegue identificar a classe ResourceErrorMessages e 
         * acessar a propriedade TITLE_REQUIRED. Porém agora eu adicionei
         * essa referência para o projeto Validators.Tests.
         * 
         */

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.
            ShouldBe(ResourceErrorMessages.TITLE_REQUIRED);
    }

    [Fact]
    public void Error_Date_Future()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Forçar a data ser no futuro
        request.Date = DateTime.UtcNow.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.
            ShouldBe(ResourceErrorMessages.EXPENSE_CANNOT_FOR_THE_FUTURE);
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Forçar o tipo de pagamento ser inválido
        request.PaymentType = (PaymentType)700;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.
            ShouldBe(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    /*
     * Eu quero que execute essa função várias vezes, trocando
     * o valor do parâmentro do amount, para testar valores inválidos.
     * A ordem não é especifíca, não quer dizer que a primeira execução
     * do teste o parâmentro vai ser zero.
     */
    public void Error_Amount_Invalid(decimal amount)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Forçar o valor ser inválido (menor ou igual a zero)
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.
            ShouldBe(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
}
