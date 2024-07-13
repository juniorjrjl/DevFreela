namespace DevFreela.API.ViewModel;


public record ProblemViewModel(int Status, DateTime Timestamp, string Message, ICollection<FieldErrorDetailsViewModel>? Fields = null);
