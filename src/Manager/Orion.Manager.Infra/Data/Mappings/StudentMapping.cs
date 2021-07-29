using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Manager.Core.Students;

namespace Orion.Manager.Infra.Data.Mappings
{
    public class StudentMapping: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.OwnsOne(e => e.RegistrationNumber, f =>
            {
                f.Property(x => x.Value).IsRequired().HasColumnName(nameof(Student.RegistrationNumber));
            }).Navigation(e => e.RegistrationNumber).IsRequired();

            builder.HasOne(e => e.PersonalInfo).WithOne().IsRequired();
        }
    }
}