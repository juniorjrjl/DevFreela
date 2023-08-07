namespace DevFreela.API.ViewModel
{
    public class CreatedProjectViewModel
    {
        public CreatedProjectViewModel(int id, string title, string description, int clientId, int freelancerId, decimal totalCost)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            TotalCost = totalCost;
        }

        public int Id { get; private set; }

        public string Title { get; private set; } 

        public string Description { get; private set; }

        public int ClientId { get; private set; }

        public int FreelancerId { get; private set; }

        public decimal TotalCost { get; private set; }

    }
}