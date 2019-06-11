using System;
using System.Linq;
using Dolittle.Queries;
using Dolittle.ReadModels;

namespace {{namespace}}
{
    public class {{name}} : IQueryFor<{{readModel.value}}>
    {
        readonly IReadModelRepositoryFor<{{readModel.value}}> _repositoryFor{{readModel.value}};

        public {{name}}(IReadModelRepositoryFor<{{readModel.value}}> repositoryFor{{readModel.value}})
        {
            _repositoryFor{{readModel.value}} = repositoryFor{{readModel.value}};
        }

        public IQueryable<{{readModel.value}}> Query => _repositoryFor{{readModel.value}}.Query;
    }
}
