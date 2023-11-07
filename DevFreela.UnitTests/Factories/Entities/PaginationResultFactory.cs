using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DevFreela.Core.Persistence.model;

namespace DevFreela.UnitTests.Factories.Entities
{
    public abstract class PaginationResultFactory<T> : Faker<PaginationResult<T>>
    {
        
        protected PaginationResultFactory()
        {
            Locale = "pt_BR";
            RuleFor(p => p.Page, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.TotalPages, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.PageSize, f=> f.Random.Number(1, int.MaxValue));
            RuleFor(p => p.ItemCount, f=> f.Random.Number(1, int.MaxValue));
        }

    }
}