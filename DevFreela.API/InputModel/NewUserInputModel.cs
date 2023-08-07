namespace DevFreela.API.InputModel
{
    public class NewUserInputModel
    {

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public List<int>? SkillsId { get; private set; }

    }
}