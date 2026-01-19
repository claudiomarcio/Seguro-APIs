using System.Net;
using System.Text.Json;
using Seguro.Hexagonal.Contratacao.Domain.Exceptions;

namespace Seguro.Hexagonal.Proposta.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Erro de domínio");

            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado");

            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro inesperado");
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = context.Response.StatusCode,
            error = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
