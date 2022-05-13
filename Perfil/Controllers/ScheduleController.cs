using Perfil.Casting;
using Perfil.Models.Perfil;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Perfil.Controllers
{
    public class ScheduleController : ApiController
    {
        private readonly ScheduleCast scheduleCast;

        public ScheduleController()
        {
            scheduleCast = new ScheduleCast();
        }

        // GET: api/Schedule
        public IEnumerable<Schedule> Get()
        {
            return scheduleCast.List();
        }

        // GET: api/Schedule/5
        public Schedule Get(int? Id)
        {
            return scheduleCast.Get(Id);
        }

        // POST: api/Schedule
        public string Post(Schedule schedule)
        {
            try
            {
                if (schedule != null)
                {
                    var httpResponseOk = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Sucesso"),
                        RequestMessage = new HttpRequestMessage(),
                    };
                    scheduleCast.Post(schedule);
                    return httpResponseOk.ToString();
                }
            }
            catch (Exception ex)
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

        // PUT: api/Schedule/5
        public void Put(Schedule schedule, int? Id)
        {
            scheduleCast.Put(schedule, Id);
        }

        // DELETE: api/Schedule/5
        public void Delete(int? Id)
        {
            scheduleCast.Delete(Id);
        }
    }
}
