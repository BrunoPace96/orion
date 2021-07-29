using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Manager.Core.Students;

namespace Orion.Manager.Infra.Data.Mappings
{
    public class StudentMapping: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.OwnsOne(e => e.Name, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(Student.Name));
            }).Navigation(e => e.Name).IsRequired();
            
            builder.OwnsOne(e => e.Cpf, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(Student.Cpf));
            }).Navigation(e => e.Cpf).IsRequired();
            
            builder.OwnsOne(e => e.Email, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(Student.Email));
            }).Navigation(e => e.Email).IsRequired();
            
            builder.OwnsOne(e => e.Phone, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(Student.Phone));
            }).Navigation(e => e.Phone).IsRequired();
        }
    }
}