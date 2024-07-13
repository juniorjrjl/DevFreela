using System.Net;
using System.Net.Mime;
using DevFreela.API.Exceptions;
using DevFreela.API.ViewModel;
using DevFreela.Core.Exceptions;
using Newtonsoft.Json;

namespace DevFreela.API.Middlewares;

public class ErrorHandlingMiddleware
{
    
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var codeNumber = (int)code;
        ErrorViewModel viewModel;

        switch (ex)
        {
            case FieldErrorException fieldError:
                code = HttpStatusCode.BadRequest;
                codeNumber = (int) code;
                viewModel = new ErrorViewModel(codeNumber, fieldError.Message, "Corriga sua requisição e tente novamente", fieldError.Fields);
                break;
            case NotFoundException notFound:
                code = HttpStatusCode.NotFound;
                codeNumber = (int) code;
                viewModel = new ErrorViewModel(codeNumber, "Erro na requisição", notFound.Message, null);
                break;
            default:
                viewModel = new ErrorViewModel(codeNumber, "Erro inesperado", "Um erro inesperado aconteceu", null);
                break;
        }

        var json = JsonConvert.SerializeObject(viewModel);
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = codeNumber;
        await context.Response.WriteAsync(json);
    }
}