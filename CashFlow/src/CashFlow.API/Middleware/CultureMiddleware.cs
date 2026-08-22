using System.Globalization;

namespace CashFlow.API.Middleware;

/*
 Middleware de cultura para manipular as requisições antes de chegarem aos
 controllers. Pode ser usado para autenticação, logging, manipulação 
 de erros, definir idioma das mensagens de resposta da API e etc.
 */
public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        // Lista de todos idiomas que o dotnet da suporte.
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        // Extrai qual idioma o cliente deseja receber a resposta da API
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        // Instancia uma cultura padrão (inglês) caso o cliente não tenha definido um idioma
        var cultureInfo = new CultureInfo("en");

        // Se o cliente definiu um idioma, então sobrescreve a cultura padrão com a cultura desejada pelo cliente
        /*
         * Estou indo lá na minha lista, utilizando linq para verificar se existe dentro dessa lista
         * uma linguagem. Ele vai verificar dentro da lista cada CultureInfo,
         * se existe alguma linguagem com o nome igual a cultura que foi solicitada.
         */
        if (string.IsNullOrWhiteSpace(requestedCulture) == false
            && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        // Altera a cultura atual da aplicação para a cultura desejada pelo cliente
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        // Permite o fluxo continuar e ir para o nosso endpoint (controller)
        await _next(context);
    }
}
