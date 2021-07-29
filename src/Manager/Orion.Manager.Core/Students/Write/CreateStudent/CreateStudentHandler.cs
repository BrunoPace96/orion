using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orion.DomainValidation.Domain;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Repository.Repository;
using Orion.Repository.UnitOfWork.Factories;

namespace Orion.Manager.Core.Students.Write.CreateStudent
{
    public class CreateStudentHandler : 
        IRequestHandler<CreateStudentCommand, CreateStudentResult>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkScopeFactory _unitOfWork;
        private readonly IRepository<Student> _repository;
        private readonly IDomainValidationProvider _validator;

        public CreateStudentHandler(
            IMapper mapper,
            IUnitOfWorkScopeFactory unitOfWork,
            IRepository<Student> repository,
            IDomainValidationProvider validator
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _validator = validator;
        }
        
        public async Task<CreateStudentResult> Handle(
            CreateStudentCommand command, 
            CancellationToken cancellationToken
        )
        {
            var name = Name.Create(command.Name);
            var cpf = Cpf.Create(command.Cpf);
            var email = Email.Create(command.Email);
            var phone = Phone.Create(command.Phone);
            
            _validator.ValidateValueObjects(name, cpf, email, phone);
            if (_validator.HasErrors())
                return null;
            
            var entity = Student.Create(name, cpf, email, phone);
            
            var unitOfWork = _unitOfWork.Get();
            await _repository.SaveAsync(entity);
            await unitOfWork.CommitAsync();

            return _mapper.Map<CreateStudentResult>(entity.Value);
        }
    }
}