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
            var allPet = new List<PetLibrary>();

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
                            allPet.Add(new PetLibrary()
                            {
                                Id = (int)dataReader["Id"],
                                Name = (string)dataReader["Name"],
                                Type = (string)dataReader["Type"],
                                Age = (int)dataReader["Age"],
                                Birthday = (DateTime)dataReader["Birthday"],
                                Genre = (string)dataReader["Genre"],
                                PersonId = (int)dataReader["PersonId"],
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
                                }
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
            return allPet;
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
                        Age = (int)dataReader["Age"],
                        Birthday = (DateTime)dataReader["Birthday"],
                        Genre = (string)dataReader["Genre"],
                        PersonId = (int)dataReader["PersonId"],
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
                        }
                    };
                }
                _sqlConnection.Close();
                return petLibrary;
            }
        }
        public new void Post(PetLibrary petLibrary)
        {
            using (SqlCommand command = new SqlCommand("PostPet", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    // -- Image
                    command.Parameters.AddWithValue("@Tag", petLibrary.Image.Tag);
                    command.Parameters.AddWithValue("@Path", petLibrary.Image.Path);
                    command.Parameters.AddWithValue("@ImageId", Convert.ToInt32(petLibrary.Image.Id));
                    // -- Health
                    command.Parameters.AddWithValue("@Status", petLibrary.Health.Status);
                    command.Parameters.AddWithValue("@HealthId", Convert.ToInt32(petLibrary.Health.Id));
                    // -- Person
                    command.Parameters.AddWithValue("@PersonId", Convert.ToInt32(petLibrary.PersonId));
                    // -- Pet
                    command.Parameters.AddWithValue("@Name", petLibrary.Name);
                    command.Parameters.AddWithValue("@Type", petLibrary.Type);
                    command.Parameters.AddWithValue("@Genre", petLibrary.Genre);
                    command.Parameters.AddWithValue("@Age", Convert.ToInt32(petLibrary.Age));
                    command.Parameters.AddWithValue("@Birthday", petLibrary.Birthday.ToString("d"));

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
        public new void Put(PetLibrary petLibrary, int? Id)
        {
            using (SqlCommand command = new SqlCommand("PutPet", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPet", Id);
                _sqlConnection.Open();

                // -- Image
                command.Parameters.AddWithValue("@Tag", petLibrary.Image.Tag);
                command.Parameters.AddWithValue("@Path", petLibrary.Image.Path);
                command.Parameters.AddWithValue("@ImageId", Convert.ToInt32(petLibrary.Image.Id));
                // -- Health
                command.Parameters.AddWithValue("@Status", petLibrary.Health.Status);
                command.Parameters.AddWithValue("@HealthId", Convert.ToInt32(petLibrary.Health.Id));
                // -- Pet
                command.Parameters.AddWithValue("@Name", petLibrary.Name);
                command.Parameters.AddWithValue("@Type", petLibrary.Type);
                command.Parameters.AddWithValue("@Genre", petLibrary.Genre);
                command.Parameters.AddWithValue("@Age", Convert.ToInt32(petLibrary.Age));
                command.Parameters.AddWithValue("@Birthday", petLibrary.Birthday.ToString("d"));
                // -- Person
                command.Parameters.AddWithValue("@PersonId", Convert.ToInt32(petLibrary.PersonId));

                var running = command.ExecuteNonQuery();
                _sqlConnection.Close();
            }
        }
        public new void Delete(int? Id)
        {
            using (SqlCommand command = new SqlCommand("DeletePet", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdPet", Id);

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