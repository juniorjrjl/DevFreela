namespace DevFreela.API.ViewModel
{
    public record ProjectDetailsViewModel(int Id, string Title, string Description, decimal TotalCost, DateTime CreatedAt, DateTime FinishedAt);
    
}
