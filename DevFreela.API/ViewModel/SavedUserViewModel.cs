namespace DevFreela.API.ViewModel
{
    public record SavedUserViewModel(
        int Id, 
        string Name, 
        string Email, 
        DateTime BirthDate, 
        ICollection<SavedUserSkillViewModel>? Skills,
        ICollection<SavedUserRoleViewModel>? Roles
    );
    
}
