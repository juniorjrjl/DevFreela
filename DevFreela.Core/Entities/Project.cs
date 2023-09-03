using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;

namespace DevFreela.Core.Entities
{

    public class Project
    {

        public int Id { get; set;}

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

        public ProjectStatusEnum Status { get; set; }

        public virtual ICollection<ProjectComment> Comments { get; set; }

        public void Cancel()
        {
            if (Status == ProjectStatusEnum.CANCELED || Status == ProjectStatusEnum.FINISHED)
            {
                throw new ProjectStatusException($"O projeto {Id} não pode ser cancelado porque está no status '{Status}'");
            }
            Status = ProjectStatusEnum.CANCELED;
        }

        public void Finish()
        {
            if (Status != ProjectStatusEnum.IN_PROGRESS)
            {
                throw new ProjectStatusException($"O projeto {Id} não pode ser finalizado porque ele não está no Status 'IN_PROGRESS'");
            }
                Status = ProjectStatusEnum.FINISHED;
                FinishedAt = DateTime.Now;
        }

        public void Start()
        {
            if (Status != ProjectStatusEnum.CREATED)
            {
                throw new ProjectStatusException($"O projeto {Id} não pode ser iniciado porque ele não está no Status 'CREATED'");
            }
                Status = ProjectStatusEnum.IN_PROGRESS;
                StartedAt = DateTime.Now;
        }

    }

}
