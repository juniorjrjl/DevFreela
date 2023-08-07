using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{

    public class Project : BaseEntity
    {
        public Project(string title, string description, int clientId, int freelancerId, decimal totalCost)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            TotalCost = totalCost;
            CreatedAt = DateTime.Now;
            Status = ProjectStatusEnum.CREATED;
            Comments = new List<ProjectComment>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ClientId { get; set; }
        public User Client { get; set; }

        public int FreelancerId { get; set; }
        public User Freelancer { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public ProjectStatusEnum Status { get; private set; }

        public List<ProjectComment> Comments { get; set; }

        public void Cancel()
        {
            if (Status != ProjectStatusEnum.CANCELED || Status != ProjectStatusEnum.FINISHED)
            {
                Status = ProjectStatusEnum.CANCELED;
            }
        }

        public void Finish()
        {
            if (Status == ProjectStatusEnum.IN_PROGRESS)
            {
                Status = ProjectStatusEnum.FINISHED;
                FinishedAt = DateTime.Now;
            }
        }

        public void Start()
        {
            if (Status == ProjectStatusEnum.CREATED)
            {
                Status = ProjectStatusEnum.IN_PROGRESS;
                StartedAt = DateTime.Now;
            }
        }

    }

}
