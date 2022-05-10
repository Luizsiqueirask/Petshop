﻿using Library.Models.Animal;
using Library.Models.Perfil;
using System.Collections.Generic;

namespace Library.Context.PerfilAuth.Schedule
{
    public interface INterfaceSchedule
    {
        IEnumerable<ScheduleLibrary> List();
        ScheduleLibrary Get(int? Id);
        void Post(ScheduleLibrary scheduleLibrary);
        void Put(ScheduleLibrary scheduleLibrary, int? Id);
        void Delete(int? Id);
    }
}
