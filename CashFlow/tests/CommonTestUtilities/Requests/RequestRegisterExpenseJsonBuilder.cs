using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

/*
 * Nós precisamos fazer uma instância dessa classe simplesmente para
 * chamamos a função build? Não, existe uma forma de chamar essa função
 * sem fazer uma instância dessa classe. Essa função é independente, então
 * não tem nenhum propósito de fazemos um new na classe. E só vir e
 * colocar essa como estática.
 */
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        // var faker = new Faker();

        /*
        var request = new RequestRegisterExpenseJson
        {
            Title = faker.Commerce.Product(),
            Date = faker.Date.Past()
        };*/

      // Todas a vezes que fizer um new no faker ele vai preencher os atributos
      // com valores diferentes
       return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}
