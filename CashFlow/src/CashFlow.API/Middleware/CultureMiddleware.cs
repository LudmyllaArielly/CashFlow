using System.Globalization;

namespace CashFlow.API.Middleware;

/*
 Middleware para manipular as requisições antes de chegarem aos
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
        // Extrai qual idioma o cliente deseja receber a resposta da API
        var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        // Instancia uma cultura padrão (inglês) caso o cliente não tenha definido um idioma
        var cultureInfo = new System.Globalization.CultureInfo("en");

        // Se o cliente definiu um idioma, então sobrescreve a cultura padrão com a cultura desejada pelo cliente
        if (string.IsNullOrWhiteSpace(culture) == false)
        {
            cultureInfo = new CultureInfo(culture);
        }

        // Altera a cultura atual da aplicação para a cultura desejada pelo cliente
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        // Permite o fluxo continuar e ir para o nosso endpoint (controller)
        await _next(context);
    }
}
