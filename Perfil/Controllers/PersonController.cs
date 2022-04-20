using Perfil.Casting;
using Perfil.Models.Perfil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Perfil.Controllers
{
    public class PersonController : ApiController
    {
        private readonly PersonCast personCast;

        public PersonController()
        {
            personCast = new PersonCast();
        }

        // GET: api/Person
        public IEnumerable<Person> Get()
        {
            return personCast.List();
        }

        // GET: api/Person/5
        public Person Get(int? Id)
        {
            return personCast.Get(Id);
        }

        // POST: api/Person
        public string Post(Person person)
        {
            try
            {
                if (person != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    personCast.Post(person);
                    return httpResponseOk.ToString();
                }               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            var httpResponseBad = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error"),
                RequestMessage = new HttpRequestMessage(),
            };
            return httpResponseBad.ToString();
        }

        // PUT: api/Person/5
        public void Put(Person person, int? Id)
        {
            personCast.Put(person, Id);
        }

        // DELETE: api/Person/5
        public void Delete(int? Id)
        {
            personCast.Delete(Id);
        }
    }
}
