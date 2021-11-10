using CLup.Domain.Business.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.EntityConfigurations
{
    class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employeeConfiguration)
        {
            employeeConfiguration.ToTable("employees");
            employeeConfiguration.HasKey(b => b.Id);
        }
    }
}