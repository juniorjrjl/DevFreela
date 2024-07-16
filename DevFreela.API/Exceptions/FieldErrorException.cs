using DevFreela.API.ViewModel;

namespace DevFreela.API.Exceptions;

public class FieldErrorException(string message, ICollection<FieldErrorViewModel> fields) : Exception(message)
{

    public ICollection<FieldErrorViewModel> Fields { get; private set; } = fields;
}