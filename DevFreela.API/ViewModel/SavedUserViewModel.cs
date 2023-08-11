namespace DevFreela.API.ViewModel
{
    
    public class SavedUserViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public List<SavedUserSkillViewModel>? Skills { get; set; }

    }

}