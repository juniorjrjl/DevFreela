namespace DevFreela.Application.ViewModel
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel(int id, string title, string description, decimal totalCost, DateTime? createdAt, DateTime? finishedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            TotalCost = totalCost;
            CreatedAt = createdAt;
            FinishedAt = finishedAt;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public decimal TotalCost { get; private set; }

        public DateTime?  CreatedAt { get; private set; }

        public DateTime?  FinishedAt { get; private set; }

    }
}