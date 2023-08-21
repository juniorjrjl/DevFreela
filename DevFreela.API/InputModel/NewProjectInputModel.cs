namespace DevFreela.API.InputModel
{
    public record NewProjectInputModel(string Title, string Description, int ClientId, int FreelancerId, decimal TotalCost);

}
