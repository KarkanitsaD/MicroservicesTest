using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesService.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.CompanyId);

            builder.Property(b => b.Name)
                .IsRequired();

            builder.Property(b => b.Surname)
                .IsRequired();
        }
    }
}