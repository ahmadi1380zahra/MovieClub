using Xunit;

namespace MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

[CollectionDefinition(nameof(ConfigurationFixture), DisableParallelization = false)]
public class ConfigurationCollectionFixture : ICollectionFixture<ConfigurationFixture>
{
}