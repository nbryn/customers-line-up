using CLup.Domain.Businesses.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employeeConfiguration)
        {
            employeeConfiguration.ToTable("employees");
            employeeConfiguration.HasKey(b => b.Id);
        }
    }
}