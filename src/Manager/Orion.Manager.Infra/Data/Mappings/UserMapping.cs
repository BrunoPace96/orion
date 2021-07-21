using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Manager.Core.Users;

namespace Orion.Manager.Infra.Data.Mappings
{
    public class UserMapping: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            
            builder.OwnsOne(e => e.Name, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(User.Name));
            }).Navigation(e => e.Name).IsRequired();
            
            builder.OwnsOne(e => e.Cpf, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(User.Cpf));
            }).Navigation(e => e.Cpf).IsRequired();
            
            builder.OwnsOne(e => e.Email, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(User.Email));
            }).Navigation(e => e.Email).IsRequired();
            
            builder.OwnsOne(e => e.Phone, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(User.Phone));
            }).Navigation(e => e.Phone).IsRequired();
        }
    }
}