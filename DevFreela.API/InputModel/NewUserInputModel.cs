namespace DevFreela.API.InputModel
{
    public record NewUserInputModel(string Name, string Email, DateTime BirthDate, ICollection<int>? SkillsId);

}
