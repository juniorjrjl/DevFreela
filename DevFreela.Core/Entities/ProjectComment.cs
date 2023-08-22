namespace DevFreela.Core.Entities
{
    public class ProjectComment
    {

        public int Id { get; set;}

        public string Comment { get; set; }

        public int ProjectId { get; set; }
        
        public virtual Project Project { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}