namespace DevFreela.API.ViewModel
{
    public class CreatedProjectViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; } 

        public string Description { get; set; }

        public int ClientId { get; set; }

        public int FreelancerId { get; set; }

        public decimal TotalCost { get; set; }

    }
}