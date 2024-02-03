using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using MasterclassApiTest.Builders;
using MasterclassApiTest.Controllers.v2;
using MasterclassApiTest.Data;
using MasterclassApiTest.Fixtures;
using MasterclassApiTest.Models;
using MasterclassApiTest.Repositories;
using MasterclassApiTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterclassApiUnitTests
{
    public class KlantenRepositoryTests
    {
        [Fact]
        public async void CreateKlant_Should_Store_A_Klant_In_The_DB()
        {
            // Arrange
            var klant = new KlantEntityBuilder().WithLoginNaam("appel").BuildCreateKlantDTO();
            var fixture = new KlantenRepositoryFixture();
            var context = fixture.Context;

            // Act
            await fixture.Sut.CreateKlant(klant);

            // Assert
            context.Klanten.Count().Should().Be(1);
            context.Klanten.First().LoginNaam.Should().Be("appel");
        }

        [Fact]
        public async void UpdateKlant_Should_Change_The_Name_Of_The_Klant()
        {
            // Arrange
            var fixture = new KlantenRepositoryFixture();
            var context = fixture.Context;
            var klant = new KlantEntityBuilder().WithLoginNaam("appel").Build();
            var klantDTO = new KlantEntityBuilder().WithLoginNaam("banaan").BuildCreateKlantDTO();
            context.Klanten.Add(klant);
            context.SaveChanges();

            // Act
            await fixture.Sut.UpdateKlant(1, klantDTO);

            // Assert
            context.Klanten.Count().Should().Be(1);
            context.Klanten.First().LoginNaam.Should().Be("banaan");
        }

        [Fact]
        public async void DeleteKlant_Should_Reduce_The_Klanten_Count_To_Zero()
        {
            // Arrange
            var fixture = new KlantenRepositoryFixture();
            var context = fixture.Context;

            var klant = new KlantEntityBuilder().Build();
            klant.Achternaam = "aardbei";
            context.Klanten.Add(klant);
            context.SaveChanges();

            // Act
            var deletedKlant = await fixture.Sut.DeleteKlant(1);

            // Assert
            context.Klanten.Count().Should().Be(0);
            deletedKlant.Achternaam.Should().Be("aardbei");
        }

        [Fact]
        public async void DeleteKlant_Should_Return_Null_If_The_Id_Is_Not_Found()
        {
            // Arrange
            var fixture = new KlantenRepositoryFixture();
            var context = fixture.Context;

            var klant = new KlantEntityBuilder().Build();
            context.Klanten.Add(klant);
            context.SaveChanges();

            // Act
            var deletedKlant = await fixture.Sut.DeleteKlant(2);

            // Assert
            context.Klanten.Count().Should().Be(1);
            deletedKlant.Should().BeNull();
        }
    }
}