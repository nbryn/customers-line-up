using CLup.Domain.Employees;
using CLup.Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(employee => employee.Id);
        builder.Property(employee => employee.Id)
            .ValueGeneratedNever()
            .HasConversion(
            employeeId => employeeId.Value,
            value => EmployeeId.Create(value));
    }
}