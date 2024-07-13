namespace DevFreela.API.ViewModel;

public record ErrorViewModel(int Status, string Title, string Details, ICollection<FieldErrorViewModel>? Fields);
