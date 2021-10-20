using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain;

namespace CLup.Data.EntityConfigurations
{
    class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employeeConfiguration)
        {
            employeeConfiguration.ToTable("employees", CLupContext.DEFAULT_SCHEMA);
            employeeConfiguration.HasKey(b => b.Id);
        }
    }
}