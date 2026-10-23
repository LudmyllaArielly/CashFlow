using CashFlow.API.Filters;
using CashFlow.API.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração global do filtro de exceção para capturar e tratar exceções lançadas durante a execução da aplicação.
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// Injeção de dependência
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração do middleware para avisamos para a API que existe um middleare(interceptador) que vai tratar a cultura da aplicação com base no cabeçalho "Accept-Language" da requisição.
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
