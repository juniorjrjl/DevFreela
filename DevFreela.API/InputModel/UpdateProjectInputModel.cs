namespace DevFreela.API.InputModel
{
    public class UpdateProjectInputModel
    {
        public UpdateProjectInputModel(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }

        public string Title { get; private set; } 

        public string Description { get; private set; }

        public decimal TotalCost { get; private set; }

    }
}