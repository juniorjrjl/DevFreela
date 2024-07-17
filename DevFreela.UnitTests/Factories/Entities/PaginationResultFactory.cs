using Bogus;
using DevFreela.Core.Persistence.model;

namespace DevFreela.UnitTests.Factories.Entities;

public abstract class PaginationResultFactory<T> : Faker<PaginationResult<T>>
{
    
    protected PaginationResultFactory(ICollection<T> data)
    {
        Locale = "pt_BR";
        CustomInstantiator(p => new 
            (
                p.Random.Number(1, int.MaxValue),
                p.Random.Number(1, int.MaxValue),
                p.Random.Number(1, int.MaxValue),
                p.Random.Number(1, int.MaxValue),
                data
            ));
    }

}