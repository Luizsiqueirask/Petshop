using Library.Models.Animal;
using System;
using System.Collections.Generic;

namespace Library.Context.PerfilAuth.Schedule
{
    public class ThrowSchedule : INterfaceSchedule
    {
        public IEnumerable<ScheduleLibrary> List()
        {
            throw new NotImplementedException();
        }
        public ScheduleLibrary Get(int? Id)
        {
            throw new NotImplementedException();
        }
        public void Post(ScheduleLibrary scheduleLibrary)
        {
            throw new NotImplementedException();
        }
        public void Put(ScheduleLibrary scheduleLibrary, int? Id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int? Id)
        {
            throw new NotImplementedException();
        }
    }
}
