using Microsoft.AspNetCore.Mvc;
using System;
using Web.Api;
using Web.Controllers;
using Library.Context.Perfil;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Web.Models.Perfil;

namespace Webx.Tests.Controllers
{
    public class PersonControllerTest
    {
        protected readonly ApiClient _clientPerson;
        protected readonly ClassPerson _classPerson;
        protected readonly Person _personTest;

        // http://www.mukeshkumar.net/articles/testing/crud-operations-unit-testing-in-aspnet-core-web-api-with-xunit
        // https://zubairmoosa.medium.com/unit-testing-a-net-core-web-api-crud-640d3a3cff37
        // https://docs.microsoft.com/pt-br/aspnet/mvc/overview/older-versions-1/unit-testing/creating-unit-tests-for-asp-net-mvc-applications-cs
        // https://stackoverflow.com/questions/19088800/unit-testing-in-crud-operation-in-entity-frame-work-in-mvc4
        // https://docs.microsoft.com/pt-br/aspnet/mvc/overview/older-versions-1/contact-manager/iteration-5-create-unit-tests-cs
        // https://www.c-sharpcorner.com/article/crud-operations-unit-testing-in-asp-net-core-web-api-with-xunit/
        // https://github.com/yogyogi/ASP.NET-Core-Unit-Testing-with-xUnit
        // https://www.hosting.work/aspnet-core-xunit-ef-core-unit-testing/
        // https://medium.com/c-sharp-progarmming/unit-testing-in-asp-net-core-web-api-b2e6f7bdb860
        // https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#override-injected-instances-of-the-di-container

        public PersonControllerTest()
        {
            _clientPerson = new ApiClient();
            _classPerson = new ClassPerson();
            _personTest = new Person()
            {
                Id = 1,
                FirstName = "Luiz",
                LastName = "Siqueira",
                Age = 32,
                Birthday = DateTime.Now,
                Genre = "Macho",
                Picture = new Picture() { Id = 1, Tag = "myfile.jpg", Path = "../localhost/myfile.jpg" },
                Contact = new Contact() { Id = 1, Email = "luiiz@siqueira.psk", Mobile = "21 97591-8265" },
                Address = new Address() { Id = 1, Country = "Brasil", States = "Rio de Janeiro", City = "Rio de Janeiro", Neighborhoods = "Leme" }
            };
        }

        #region List

        [Fact]
        public async void List()
        {
            // Arrange
            var allPeople = await _clientPerson.GetPerson();

            // Act

            // Assert
            Assert.IsType<BadRequestResult>(allPeople);

            if (allPeople.IsSuccessStatusCode)
            {
                var people = await allPeople.Content.ReadAsAsync<IEnumerable<Person>>();
                Assert.IsType<List<Person>>(people);

                var listPeople = people as BadRequestResult;
                Assert.IsType<List<Person>>(listPeople);

                //var listPerson = people as List<Person>;
                //Assert.Equal(_personTest.FirstName, listPerson);
            }
        }

        #endregion List
        /*
        #region Get
        [Fact]
        public async void Get()
        {
            
        }
        #endregion Get

        #region Post
        [Fact]
        public async void Post()
        {
            // Arrange
            
            // Act
            var allPeople =  _clientPerson.PostPerson(_personTest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(allPeople);

            var personModel = await allPeople as CreatedAtActionResult;
            Assert.IsType<Person>(personModel.Value);



        }
        #endregion Post*/
    }
}
