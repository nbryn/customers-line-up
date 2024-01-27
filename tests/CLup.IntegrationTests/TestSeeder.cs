using CLup.Infrastructure.Persistence.Seed;

namespace tests.CLup.IntegrationTests;

public class TestSeeder : ISeeder
{
    public Task Seed() => Task.CompletedTask;
}
