namespace DevFreela.API.ViewModel
{
    
    public class SavedUserSkillViewModel
    {
        
        public SavedUserSkillViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }

    }

}