using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using MasterclassApiTest.Controllers.v2;
using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Fixtures;
using MasterclassApiTest.Pagination;
using MasterclassApiTest.Repositories;
using MasterclassApiTest.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace MasterclassApiUnitTests
{
    public class KlantTests
    {
        [Fact]
        public async void Get_Klanten_Returns_List_Of_Klanten_When_Called()
        {
            // Arrange
            var fixture = new KlantenControllerFixture();
            
            // Act
            var pageParams = new KlantPageParameters
            {
                PageNumber = 1,
                PageSize = 10,
            };
            var result = await fixture.Sut.Get(pageParams);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}