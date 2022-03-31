using Library.Models.Animal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                Health = new HealthLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Status = (Enum)dataReader["Status"],
                                },
                                Image = new ImageLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Tag = (string)dataReader["Tag"],
                                    Path = (string)dataReader["Path"]
                                },
                                Service = new ServiceLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Category = (string)dataReader["Category"]
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
                command.Parameters.AddWithValue("@IdPerson", Id);
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
                        Health = new HealthLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Status = (Enum)dataReader["Status"],
                        },
                        Image = new ImageLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Tag = (string)dataReader["Tag"],
                            Path = (string)dataReader["Path"]
                        },
                        Service = new ServiceLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Category = (string)dataReader["Category"]
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
                    command.Parameters.AddWithValue("@Category", petLibrary.Service.Category);
                   
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
                command.Parameters.AddWithValue("@IdPerson", Id);
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
                command.Parameters.AddWithValue("@Category", petLibrary.Service.Category);

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
                command.Parameters.AddWithValue("@IdPerson", Id);

                _sqlConnection.Open();
                var running = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
    }
}
