using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orion.DomainValidation.Domain;
using Orion.Repository.Repository;

namespace Orion.Manager.Core.Users.Read.GetUserByCpf
{
    public class GetUserByCpfHandler : 
        IRequestHandler<GetUserByCpfQuery, GetUserByCpfResult>
    {
        private readonly IReadOnlyRepository<User> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public GetUserByCpfHandler(
            IReadOnlyRepository<User> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<GetUserByCpfResult> Handle(
            GetUserByCpfQuery query, 
            CancellationToken cancellationToken
        )
        {
            var specification = new GetUserByCpfSpecification(query.Cpf);
            var user = await _repository.FirstOrDefaultAsync(specification);

            if (user == null)
            {
                _validator.AddNotFoundError();
                return null;
            }

            return _mapper.Map<GetUserByCpfResult>(user);
        }
    }
}