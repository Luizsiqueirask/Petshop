using Library.Context.Perfil;
using Library.Models.Perfil;
using Library.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Web.Controllers;

namespace Webs.Tests.Controllers
{
    [TestClass]
    public class PersonCheckPathTest
    {
        protected readonly string abosolutePath = @"C:\Users\Luiz Siqueira\Desktop\EDC_Assessment\CSharp\Petshop\Web\Storage\Person\";
        protected readonly PersonController _personnConntroller;
        protected readonly PersonLibrary _personTest;

        public PersonCheckPathTest()
        {
            _personnConntroller = new PersonController();
            _personTest = new PersonLibrary()
            {
                Id = 1,
                FirstName = "Luiz",
                LastName = "Siqueira",
                Age = 32,
                Birthday = DateTime.Now,
                Genre = "Macho",
                Picture = new PictureLibrary() { Id = 1, Tag = "myfile.jpg", Path = "../localhost/myfile.jpg" },
                Contact = new ContactLibrary() { Id = 1, Email = "luiiz@siqueira.psk", Mobile = "21 97591-8265" },
                Address = new AddressLibrary() { Id = 1, Country = "Brasil", States = "Rio de Janeiro", City = "Rio de Janeiro", Neighborhoods = "Leme" }
            };
        }

        [TestMethod]
        public void FileNameDoesExists()
        {
            string fileName = "01.jpg";
            FileProcess fileProcess = new FileProcess();
            bool fromCall = fileProcess.FileExists(Path.Combine(abosolutePath, fileName));
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fileProcess = new FileProcess();
            bool fromCall = fileProcess.FileExists(Path.Combine(abosolutePath, ""));
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmptyThrows()
        {
            FileProcess fileProcess = new FileProcess();
            fileProcess.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmptyTryCatch()
        {
            FileProcess fileProcess = new FileProcess();

            try
            {
                fileProcess.FileExists("");
            }
            catch (ArgumentException)
            {
                return;
            }
            Assert.Fail("Fail Expected");
        }        
    }
}
