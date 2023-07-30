namespace DevFreela.Application.InputModel
{
    public class NewUserInputModel
    {
        public NewUserInputModel(string name, string email, DateTime birthDate, List<int> skillsId)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            SkillsId = skillsId;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public List<int> SkillsId { get; private set; }

    }
}