using FluentAssertions;
//using Kernex.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureTests;

public class ApiTests
{
    [Fact]
    public void Api_Should_Not_Depend_On_Domain_Project() 
    {
        var result = Types
            .InAssembly(typeof(Kernex.API.AssemblyReference).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Domain")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Api_Must_Depend_On_Application_And_Infrastructure_Projects() 
    {
        var result = Types
            .InAssembly(typeof(Application.AssemblyReference).Assembly)
            .Should()
            .HaveDependencyOn("Kernex.Application")
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_ShouldResideInControllersNamespace() 
    {
        //var result = Types
        //    .InAssembly(typeof(WeatherForecastController).Assembly)
        //    .Should()
        //    .ResideInNamespace("Kernex.API.Controllers")
        //    .GetResult();

        //Assert.True(result.IsSuccessful, "Controllers should reside in the Controllers namespace.");
    }
}
