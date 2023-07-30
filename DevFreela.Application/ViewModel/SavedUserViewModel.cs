namespace DevFreela.Application.ViewModel
{
    
    public class SavedUserViewModel
    {
        
        public SavedUserViewModel(int id, string name, string email, DateTime birthDate, List<SavedUserSkillViewModel> skills)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public List<SavedUserSkillViewModel> Skills { get; private set; }

    }

}