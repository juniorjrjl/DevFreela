namespace DevFreela.API.InputModel
{
    public record NewUserInputModel(
        string Name, 
        string Email, 
        string Password,
        string PasswordConfirm,
        string Role,
        DateTime BirthDate, 
        ICollection<int>? SkillsId
    );

}
