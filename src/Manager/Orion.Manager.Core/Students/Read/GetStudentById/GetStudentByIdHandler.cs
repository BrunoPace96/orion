using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orion.DataContracts.Queries;
using Orion.DomainValidation.Domain;
using Orion.Repository.Repository;

namespace Orion.Manager.Core.Students.Read.GetStudentById
{
    public class GetStudentByIdHandler: 
        IRequestHandler<ByIdQuery<GetStudentByIdResult>, GetStudentByIdResult>
    {
        private readonly IReadOnlyRepository<Student> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public GetStudentByIdHandler(
            IReadOnlyRepository<Student> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<GetStudentByIdResult> Handle(
            ByIdQuery<GetStudentByIdResult> query, 
            CancellationToken cancellationToken
        )
        {
            var specification = new GetStudentByIdSpecification(query.Id);
            var student = await _repository.FirstOrDefaultAsync(specification);

            if (student == null)
            {
                _validator.AddNotFoundError();
                return null;
            }

            return _mapper.Map<GetStudentByIdResult>(student);
        }
    }
}