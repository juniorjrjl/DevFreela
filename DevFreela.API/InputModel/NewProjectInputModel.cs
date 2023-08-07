namespace DevFreela.API.InputModel
{
    public class NewProjectInputModel
    {
        public NewProjectInputModel(string title, string description, int clientId, int freelancerId, decimal totalCost)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            TotalCost = totalCost;
        }

        public string Title { get; private set; } 

        public string Description { get; private set; }

        public int ClientId { get; private set; }

        public int FreelancerId { get; private set; }

        public decimal TotalCost { get; private set; }

    }
}