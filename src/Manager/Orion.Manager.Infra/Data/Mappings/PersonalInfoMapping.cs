using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Manager.Core.Students;

namespace Orion.Manager.Infra.Data.Mappings
{
    public class PersonalInfoMapping: IEntityTypeConfiguration<PersonalInfo>
    {
        public void Configure(EntityTypeBuilder<PersonalInfo> builder)
        {
            builder.OwnsOne(e => e.Name, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(PersonalInfo.Name));
            }).Navigation(e => e.Name).IsRequired();
            
            builder.OwnsOne(e => e.Cpf, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(PersonalInfo.Cpf));
            }).Navigation(e => e.Cpf).IsRequired();
            
            builder.OwnsOne(e => e.Email, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(PersonalInfo.Email));
            }).Navigation(e => e.Email).IsRequired();
            
            builder.OwnsOne(e => e.Phone, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(PersonalInfo.Phone));
            }).Navigation(e => e.Phone).IsRequired();
        }
    }
}