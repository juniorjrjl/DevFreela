using DevFreela.API.ViewModel;

namespace DevFreela.API.Exceptions;

public class FieldErrorException : Exception
{
    
    public ICollection<FieldErrorViewModel> Fields { get; private set; }

    public FieldErrorException(string message, ICollection<FieldErrorViewModel> fields) : base(message) => Fields = fields;

}