using Library.Models.Animal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Library.Context.Animal
{
    public class ClassPet : ThrowPet
    {
        public readonly Bridge _conn;
        public readonly SqlConnection _sqlConnection;

        public ClassPet()
        {
            _conn = new Bridge();
            _sqlConnection = new SqlConnection(_conn.Connect());
        }

        public new IEnumerable<PetLibrary> List()
        {
            var allPerson = new List<PetLibrary>();

            try
            {
                using (SqlCommand command = new SqlCommand("ListPet", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            var petLibrary = new PetLibrary()
                            {
                                Id = (int)dataReader["Id"],
                                Name = (string)dataReader["Name"],
                                Type = (string)dataReader["Type"],
                                Genre = (string)dataReader["Genre"],
                                Birthday = (DateTime)dataReader["Birthday"],
                                Age = (int)dataReader["Age"],
                                Image = new ImageLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Tag = (string)dataReader["Tag"],
                                    Path = (string)dataReader["Path"]
                                },
                                Health = new HealthLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Status = (string)dataReader["Status"],
                                },
                                Schedule = new ScheduleLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Services = (string)dataReader["Services"],
                                    Date = (DateTime)dataReader["Date"],
                                    Time = (DateTime)dataReader["Time"],
                                    Places = new PlacesLibrary()
                                    {
                                        City = (string)dataReader["City"],
                                        Street = (string)dataReader["Street"],
                                        Number = (int)dataReader["Number"],
                                    }
                                },
                                PersonId = (int)dataReader["PersonId"]
                            };

                            allPerson.Add(petLibrary);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return allPerson;
        }
        public new PetLibrary Get(int? Id)
        {
            var petLibrary = new PetLibrary();

            using (SqlCommand command = new SqlCommand("GetPet", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPet", Id);
                _sqlConnection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    petLibrary = new PetLibrary()
                    {
                        Id = (int)dataReader["Id"],
                        Name = (string)dataReader["Name"],
                        Type = (string)dataReader["Type"],
                        Genre = (string)dataReader["Genre"],
                        Birthday = (DateTime)dataReader["Birthday"],
                        Age = (int)dataReader["Age"],
                        Image = new ImageLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Tag = (string)dataReader["Tag"],
                            Path = (string)dataReader["Path"]
                        },
                        Health = new HealthLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Status = (string)dataReader["Status"],
                        },
                        Schedule = new ScheduleLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Services = (string)dataReader["Services"],
                            Date = (DateTime)dataReader["Date"],
                            Time = (DateTime)dataReader["Time"],
                            Places = new PlacesLibrary()
                            {
                                City = (string)dataReader["City"],
                                Street = (string)dataReader["Street"],
                                Number = (int)dataReader["Number"],
                            }                            
                        },
                        PersonId = (int)dataReader["PersonId"]
                    };
                }
            }
            _sqlConnection.Close();
            return petLibrary;
        }
        public new void Post(PetLibrary petLibrary)
        {
            using (SqlCommand command = new SqlCommand("PostPet", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // -- Pet
                    command.Parameters.AddWithValue("@Name", petLibrary.Name);
                    command.Parameters.AddWithValue("@Type", petLibrary.Type);
                    command.Parameters.AddWithValue("@Genre", petLibrary.Genre);
                    command.Parameters.AddWithValue("@Age", petLibrary.Age);
                    command.Parameters.AddWithValue("@Birthday", petLibrary.Birthday);
                    command.Parameters.AddWithValue("@Genre", petLibrary.Genre);
                    // -- Image
                    command.Parameters.AddWithValue("@Tag", petLibrary.Image.Tag);
                    command.Parameters.AddWithValue("@Path", petLibrary.Image.Path);
                    // -- Health
                    command.Parameters.AddWithValue("@Status", petLibrary.Health.Status);
                    // -- Service
                    command.Parameters.AddWithValue("@Services", petLibrary.Schedule.Services);
                    command.Parameters.AddWithValue("@Date", petLibrary.Schedule.Date);
                    command.Parameters.AddWithValue("@Time", petLibrary.Schedule.Time);
                    command.Parameters.AddWithValue("@City", petLibrary.Schedule.Places.City);
                    command.Parameters.AddWithValue("@Street", petLibrary.Schedule.Places.Street);
                    command.Parameters.AddWithValue("@Number", petLibrary.Schedule.Places.Number);

                    int running = command.ExecuteNonQuery();
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
        public new void Put(PetLibrary petLibrary, int? Id)
        {
            using (SqlCommand command = new SqlCommand("PutPet", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                // -- Pet
                command.Parameters.AddWithValue("@Name", petLibrary.Name);
                command.Parameters.AddWithValue("@Type", petLibrary.Type);
                command.Parameters.AddWithValue("@Genre", petLibrary.Genre);
                command.Parameters.AddWithValue("@Age", petLibrary.Age);
                command.Parameters.AddWithValue("@Birthday", petLibrary.Birthday);
                command.Parameters.AddWithValue("@Genre", petLibrary.Genre);
                // -- Image
                command.Parameters.AddWithValue("@Tag", petLibrary.Image.Tag);
                command.Parameters.AddWithValue("@Path", petLibrary.Image.Path);
                // -- Health
                command.Parameters.AddWithValue("@Status", petLibrary.Health.Status);
                // -- Service
                command.Parameters.AddWithValue("@Services", petLibrary.Schedule.Services);
                command.Parameters.AddWithValue("@Date", petLibrary.Schedule.Date);
                command.Parameters.AddWithValue("@Time", petLibrary.Schedule.Time);
                command.Parameters.AddWithValue("@City", petLibrary.Schedule.Places.City);
                command.Parameters.AddWithValue("@Street", petLibrary.Schedule.Places.Street);
                command.Parameters.AddWithValue("@Number", petLibrary.Schedule.Places.Number);

                _sqlConnection.Open();
                var running = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
        public new void Delete(int? Id)
        {
            using (SqlCommand command = new SqlCommand("DeletePet", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPet", Id);

                _sqlConnection.Open();
                var running = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
    }
}