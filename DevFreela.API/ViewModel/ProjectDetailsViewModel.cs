namespace DevFreela.API.ViewModel
{
    public class ProjectDetailsViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime?  CreatedAt { get; set; }

        public DateTime?  FinishedAt { get; set; }

    }
}