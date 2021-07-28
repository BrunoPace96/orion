using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orion.Repository.Repository;
using Orion.Repository.UnitOfWork.Factories;

namespace Orion.Manager.Core.Users.Write.CreateUser
{
    public class CreateUserHandler : 
        IRequestHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkScopeFactory _unitOfWork;
        private readonly IRepository<User> _repository;

        public CreateUserHandler(
            IMapper mapper,
            IUnitOfWorkScopeFactory unitOfWork,
            IRepository<User> repository
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        
        public async Task<CreateUserResult> Handle(
            CreateUserCommand command, 
            CancellationToken cancellationToken
        )
        {
            var entity = User.Create(
                command.Name,
                command.Cpf,
                command.Email,
                command.Phone
            );
            
            var unitOfWork = _unitOfWork.Get();
            await _repository.SaveAsync(entity);
            await unitOfWork.CommitAsync();

            return _mapper.Map<CreateUserResult>(entity.Value);
        }
    }
}