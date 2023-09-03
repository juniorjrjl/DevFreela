using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories
{
    
    public class UserFactory: Faker<User>
    {

        public UserFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Id, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.Name, f => f.Lorem.Word());
            RuleFor(p => p.Email, f => f.Internet.Email());
            RuleFor(p => p.BirthDate, f => f.Date.Recent());
            RuleFor(p => p.CreatedAt, f => f.Date.Recent());
            RuleFor(p => p.Active, f => f.Random.Bool());
            RuleFor(p => p.Password, f => f.Lorem.Word());
        }

    }

}