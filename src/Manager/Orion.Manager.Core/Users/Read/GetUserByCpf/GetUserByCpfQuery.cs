using Ardalis.Specification;
using MediatR;

namespace Orion.Manager.Core.Users.Read.GetUserByCpf
{
    public record GetUserByCpfQuery(string Cpf): IRequest<GetUserByCpfResult>;

    public sealed class GetUserByCpfSpecification : Specification<User>
    {
        public GetUserByCpfSpecification(string cpf)
        {
            Query.Where(e => e.Cpf.Value == cpf);
        }
    }
}