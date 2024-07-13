using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;

namespace DevFreela.Core.Entities;


public class Project
{

    public Project(){}
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
    public int Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public int ClientId { get; private set; }
    public User Client { get; private set; }

    public int FreelancerId { get; private set; }
    public User Freelancer { get; private set; }

    public decimal TotalCost { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? StartedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public ProjectStatusEnum Status { get; private set; }

    public virtual ICollection<ProjectComment> Comments { get; private set; }

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

    public Project Update(string title, string description, decimal totalCost)    
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
        return this;
    }

}
