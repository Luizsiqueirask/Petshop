using Library.Models.Animal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Library.Context.PerfilAuth.Schedule
{
    public class ClassSchedule : ThrowSchedule
    {
        public readonly Bridge _conn;
        public readonly SqlConnection _sqlConnection;

        public ClassSchedule()
        {
            _conn = new Bridge();
            _sqlConnection = new SqlConnection(_conn.Connect());
        }

        public new IEnumerable<ScheduleLibrary> List()
        {
            var allSchedule = new List<ScheduleLibrary>();

            try
            {
                using (SqlCommand command = new SqlCommand("ListSchedule", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            allSchedule.Add(new ScheduleLibrary()
                            {
                                Id = (int)dataReader["Id"],
                                Services = (string)dataReader["Services"],                                
                                Date = (DateTime)dataReader["Date"],
                                Time = (DateTime)dataReader["Time"]
                            });
                        }
                    }
                    _sqlConnection.Close();
                }
            }
            finally
            {
                _sqlConnection.Close();
            }
            return allSchedule;
        }
        public new ScheduleLibrary Get(int? Id)
        {
            var scheduleLibrary = new ScheduleLibrary();

            using (SqlCommand command = new SqlCommand("GetSchedule", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdSchedule", Id);
                _sqlConnection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    scheduleLibrary = new ScheduleLibrary()
                    {
                        Id = (int)dataReader["Id"],
                        Services = (string)dataReader["Services"],
                        Date = (DateTime)dataReader["Date"],
                        Time = (DateTime)dataReader["Time"]
                    };
                }
                _sqlConnection.Close();
                return scheduleLibrary;
            }
        }
        public new void Post(ScheduleLibrary scheduleLibrary)
        {
            using (SqlCommand command = new SqlCommand("PostSchedule", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();
                    
                    command.Parameters.AddWithValue("@Id", scheduleLibrary.Id);
                    command.Parameters.AddWithValue("@Services", scheduleLibrary.Services);
                    command.Parameters.AddWithValue("@Date", scheduleLibrary.Date.ToString("d"));
                    command.Parameters.AddWithValue("@Time", scheduleLibrary.Time.ToString("t"));

                    int running = command.ExecuteNonQuery();
                    _sqlConnection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQLException: " + ex.Message);
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }
        public new void Put(ScheduleLibrary scheduleLibrary, int? Id)
        {
            using (SqlCommand command = new SqlCommand("PutSchedule", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdSchedule", Id);
                _sqlConnection.Open();

                // -- Picture
                command.Parameters.AddWithValue("@Id", scheduleLibrary.Id);
                command.Parameters.AddWithValue("@Services", scheduleLibrary.Services);
                command.Parameters.AddWithValue("@Date", scheduleLibrary.Date.ToString("d"));
                command.Parameters.AddWithValue("@Time", scheduleLibrary.Time.ToString("t"));

                var running = command.ExecuteNonQuery();
                _sqlConnection.Close();
            }
        }
        public new void Delete(int? Id)
        {
            using (SqlCommand command = new SqlCommand("DeleteSchedule", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdSchedule", Id);

                    _sqlConnection.Open();
                    var running = command.ExecuteNonQuery();
                    _sqlConnection.Close();
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }
    }
}