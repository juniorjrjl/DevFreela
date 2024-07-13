namespace DevFreela.API.ViewModel;

public record CreatedProjectViewModel(int Id, string Title, string Description, int ClientId, int FreelancerId, decimal TotalCost);
