using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using DevFreela.API.Exceptions;
using DevFreela.API.ViewModel;
using DevFreela.Core.Exceptions;
using Newtonsoft.Json;

namespace DevFreela.API.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    
    private readonly RequestDelegate _next = next;

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
        ErrorViewModel viewModel = ex switch
        {
            FieldErrorException fieldError => new ((int)HttpStatusCode.BadRequest, fieldError.Message, "Corriga sua requisição e tente novamente", fieldError.Fields),
            NotFoundException notFound => new ((int)HttpStatusCode.NotFound, "Erro na requisição", notFound.Message),
            InvalidCredentialException invalidCredential => new ((int)HttpStatusCode.Unauthorized, "Erro na requisição", invalidCredential.Message),
            ProjectStatusException projectStatus => new ((int)HttpStatusCode.BadRequest, "Erro na requisição", projectStatus.Message),
            _ => new ((int)HttpStatusCode.InternalServerError, "Erro inesperado", "Um erro inesperado aconteceu"),
        };
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };
        var json = JsonConvert.SerializeObject(viewModel, settings);
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = viewModel.Status;
        await context.Response.WriteAsync(json);
    }
}