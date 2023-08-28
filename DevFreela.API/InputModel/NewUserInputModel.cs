using DevFreela.Core.Enums;

namespace DevFreela.API.InputModel
{
    public record NewUserInputModel(
        string Name, 
        string Email, 
        string Password,
        string PasswordConfirm,
        ICollection<RoleNameEnum> Roles,
        DateTime BirthDate, 
        ICollection<int>? SkillsId
    );

}
