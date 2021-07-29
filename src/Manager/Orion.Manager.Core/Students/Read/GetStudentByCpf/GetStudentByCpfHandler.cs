using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orion.DomainValidation.Domain;
using Orion.Repository.Repository;

namespace Orion.Manager.Core.Students.Read.GetStudentByCpf
{
    public class GetStudentByCpfHandler : 
        IRequestHandler<GetStudentByCpfQuery, GetStudentByCpfResult>
    {
        private readonly IReadOnlyRepository<Student> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public GetStudentByCpfHandler(
            IReadOnlyRepository<Student> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<GetStudentByCpfResult> Handle(
            GetStudentByCpfQuery query, 
            CancellationToken cancellationToken
        )
        {
            var specification = new GetStudentByCpfSpecification(query.Cpf);
            var student = await _repository.FirstOrDefaultAsync(specification);

            if (student == null)
            {
                _validator.AddNotFoundError();
                return null;
            }

            return _mapper.Map<GetStudentByCpfResult>(student);
        }
    }
}