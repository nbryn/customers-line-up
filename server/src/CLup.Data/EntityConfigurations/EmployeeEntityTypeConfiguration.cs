using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain.Businesses.Employees;

namespace CLup.Data.EntityConfigurations
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