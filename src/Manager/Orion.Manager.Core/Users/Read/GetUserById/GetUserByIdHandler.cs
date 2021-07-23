using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orion.DataContracts.Queries;
using Orion.DomainValidation.Domain;
using Orion.Repository.Repository;

namespace Orion.Manager.Core.Users.Read.GetUserById
{
    public class GetUserByIdHandler: 
        IRequestHandler<ByIdQuery<GetUserByIdResult>, GetUserByIdResult>
    {
        private readonly IReadOnlyRepository<User> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(
            IReadOnlyRepository<User> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<GetUserByIdResult> Handle(
            ByIdQuery<GetUserByIdResult> query, 
            CancellationToken cancellationToken
        )
        {
            var user = await _repository.FirstOrDefaultAsync(query.Id);

            if (user == null)
            {
                _validator.AddNotFoundError();
                return null;
            }

            return _mapper.Map<GetUserByIdResult>(user);
        }
    }
}