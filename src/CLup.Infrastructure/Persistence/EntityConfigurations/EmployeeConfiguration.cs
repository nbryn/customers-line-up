using CLup.Domain.Employees;
using CLup.Domain.Employees.ValueObjects;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
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
