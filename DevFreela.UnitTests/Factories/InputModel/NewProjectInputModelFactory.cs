using AutoBogus;
using DevFreela.API.InputModel;

namespace DevFreela.UnitTests.Factories.InputModel;

public class NewProjectInputModelFactory : AutoFaker<NewProjectInputModel>
{
    
    private NewProjectInputModelFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Title, f => f.Lorem.Word());
        RuleFor(g => g.Description, f => f.Lorem.Word());
        RuleFor(g => g.ClientId, f => f.Random.Int(1, int.MaxValue));
        RuleFor(g => g.FreelancerId, f => f.Random.Int(1, int.MaxValue));
        RuleFor(g => g.TotalCost, f => f.Random.Decimal());
    }

    public static NewProjectInputModelFactory Instance() => new();

}
