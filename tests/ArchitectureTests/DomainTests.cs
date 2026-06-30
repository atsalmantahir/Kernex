using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests
{
    public class DomainTests
    {
        [Fact]
        public void Domain_Should_Not_Depend_On_Any_Project() 
        {
            var result = Types
            .InAssembly(typeof(Domain.AssemblyReference).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                "Application",
                "Infrastructure",
                "Kernex.Api")
            .GetResult();

            result.IsSuccessful.Should().BeTrue();

        }
    }
}
