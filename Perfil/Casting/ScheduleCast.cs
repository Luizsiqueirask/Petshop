using Library.Context.Schedule;
using Library.Models.Perfil;
using Perfil.Models.Perfil;
using System.Collections.Generic;

namespace Perfil.Casting
{
    public class ScheduleCast
    {
        private readonly ClassSchedule classAuthSchedule;
        public ScheduleCast()
        {
            classAuthSchedule = new ClassSchedule();
        }
        public IEnumerable<Schedule> List()
        {
            var listSchedule = new List<Schedule>();
            var allSchedule = classAuthSchedule.List();

            if (allSchedule != null)
            {
                foreach (var schedule in allSchedule)
                {
                    listSchedule.Add(new Schedule()
                    {
                        Id = schedule.Id,
                        Services = schedule.Services,
                        Date = schedule.Date,
                        Time = schedule.Time,
                        PersonId = schedule.PersonId
                    });
                }
                return listSchedule;
            }
            return new List<Schedule>();
        }
        public Schedule Get(int? Id)
        {
            var schedules = classAuthSchedule.Get(Id);

            if (schedules != null)
            {
                var schedule = new Schedule()
                {
                    Id = schedules.Id,
                    Services = schedules.Services,
                    Date = schedules.Date,
                    Time = schedules.Time,
                    PersonId = schedules.PersonId
                };
                return schedule;
            }
            return new Schedule();
        }
        public void Post(Schedule schedule)
        {
            if (schedule != null)
            {
                var scheduleAuthLibrary = new ScheduleLibrary()
                {
                    Id = schedule.Id,
                    Services = schedule.Services,
                    Date = schedule.Date,
                    Time = schedule.Time,
                    PersonId = schedule.PersonId
                };

                classAuthSchedule.Post(scheduleAuthLibrary);
            }
        }
        public void Put(Schedule schedule, int? Id)
        {
            if (schedule != null)
            {
                var scheduleAuthLibrary = new ScheduleLibrary()
                {
                    Id = schedule.Id,
                    Services = schedule.Services,
                    Date = schedule.Date,
                    Time = schedule.Time,
                    PersonId = schedule.PersonId
                };

                classAuthSchedule.Put(scheduleAuthLibrary, Id);
            }
        }
        public void Delete(int? Id)
        {
            classAuthSchedule.Delete(Id);
        }
    }
}