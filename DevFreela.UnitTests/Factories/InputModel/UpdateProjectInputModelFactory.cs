using AutoBogus;
using DevFreela.API.InputModel;

namespace DevFreela.UnitTests.InputModel.Commands;

public class UpdateProjectInputModelFactory : AutoFaker<UpdateProjectInputModel>
{

    private UpdateProjectInputModelFactory()
    {
        RuleFor(p => p.Title, f => f.Random.Word());
        RuleFor(p => p.Description, f => f.Random.Word());
        RuleFor(p => p.TotalCost, f => f.Random.Decimal());
    }

    public static UpdateProjectInputModelFactory Instance() => new();

}
