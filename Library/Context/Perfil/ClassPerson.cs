using Library.Models.Perfil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Library.Context.Perfil
{
    public class ClassPerson : ThrowPerson
    {
        public readonly Bridge _conn;
        public readonly SqlConnection _sqlConnection;

        public ClassPerson()
        {
            _conn = new Bridge();
            _sqlConnection = new SqlConnection(_conn.Connect());
        }

        public new IEnumerable<PersonLibrary> List()
        {
            var allPerson = new List<PersonLibrary>();

            try
            {
                using (SqlCommand command = new SqlCommand("ListPerson", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    _sqlConnection.Open();

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader.HasRows)
                        {
                            var personLibrary = new PersonLibrary()
                            {
                                Id = (int)dataReader["Id"],
                                FirstName = (string)dataReader["FirstName"],
                                LastName = (string)dataReader["LastName"],
                                Age = (int)dataReader["Age"],
                                Birthday = (DateTime)dataReader["Birthday"],
                                Genre = (string)dataReader["Genre"],
                                Picture = new PictureLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Tag = (string)dataReader["Tag"],
                                    Path = (string)dataReader["Path"]
                                },
                                Contact = new ContactLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Email = (string)dataReader["Email"],
                                    Mobile = (string)dataReader["Mobile"]
                                },
                                Address = new AddressLibrary()
                                {
                                    Id = (int)dataReader["Id"],
                                    Country = (string)dataReader["Country"],
                                    States = (string)dataReader["States"],
                                    City = (string)dataReader["City"],
                                    Neighborhoods = (string)dataReader["Neighborhoods"]
                                }
                            };

                            allPerson.Add(personLibrary);
                            _sqlConnection.Close();
                        }
                        return allPerson;
                    }
                }
            }
            finally
            {
                _sqlConnection.Close();
            }

            return new List<PersonLibrary>();

        }
        public new PersonLibrary Get(int? Id)
        {
            var personLibrary = new PersonLibrary();

            using (SqlCommand command = new SqlCommand("GetPerson", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPerson", Id);
                _sqlConnection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    personLibrary = new PersonLibrary()
                    {
                        Id = (int)dataReader["Id"],
                        FirstName = (string)dataReader["FirstName"],
                        LastName = (string)dataReader["LastName"],
                        Age = (int)dataReader["Age"],
                        Birthday = (DateTime)dataReader["Birthday"],
                        Genre = (string)dataReader["Genre"],
                        Picture = new PictureLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Tag = (string)dataReader["Tag"],
                            Path = (string)dataReader["Path"]
                        },
                        Contact = new ContactLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Email = (string)dataReader["Email"],
                            Mobile = (string)dataReader["Mobile"]
                        },
                        Address = new AddressLibrary()
                        {
                            Id = (int)dataReader["Id"],
                            Country = (string)dataReader["Country"],
                            States = (string)dataReader["States"],
                            City = (string)dataReader["City"],
                            Neighborhoods = (string)dataReader["Neighborhoods"]
                        }
                    };
                }
            }
            _sqlConnection.Close();
            return personLibrary;
        }
        public new void Post(PersonLibrary personLibrary)
        {
            using (SqlCommand command = new SqlCommand("PostPerson", _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;                    
                    // -- Picture
                    command.Parameters.AddWithValue("@Tag", personLibrary.Picture.Tag);
                    command.Parameters.AddWithValue("@Path", personLibrary.Picture.Path);
                    command.Parameters.AddWithValue("@PictureId", Convert.ToInt32(personLibrary.Picture.Id));
                    // -- Contacts
                    command.Parameters.AddWithValue("@Email", personLibrary.Contact.Email);
                    command.Parameters.AddWithValue("@Mobile", personLibrary.Contact.Mobile);
                    command.Parameters.AddWithValue("@ContactId", Convert.ToInt32(personLibrary.Contact.Id));
                    // -- Address
                    command.Parameters.AddWithValue("@Country", personLibrary.Address.Country);
                    command.Parameters.AddWithValue("@States", personLibrary.Address.States);
                    command.Parameters.AddWithValue("@City", personLibrary.Address.City);
                    command.Parameters.AddWithValue("@Neighborhoods", personLibrary.Address.Neighborhoods);
                    command.Parameters.AddWithValue("@AddressId", Convert.ToInt32(personLibrary.Address.Id));
                    // -- Person
                    command.Parameters.AddWithValue("@FirstName", personLibrary.FirstName);
                    command.Parameters.AddWithValue("@LastName", personLibrary.LastName);
                    command.Parameters.AddWithValue("@Genre", personLibrary.Genre);
                    command.Parameters.AddWithValue("@Age", personLibrary.Age);
                    command.Parameters.AddWithValue("@Birthday", personLibrary.Birthday.ToString("d"));

                    _sqlConnection.Open();
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
        public new void Put(PersonLibrary personLibrary, int? Id)
        {
            using (SqlCommand command = new SqlCommand("PutPerson", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdPerson", Id);
                // -- Picture
                command.Parameters.AddWithValue("@Tag", personLibrary.Picture.Tag);
                command.Parameters.AddWithValue("@Path", personLibrary.Picture.Path);
                command.Parameters.AddWithValue("@PictureId", Convert.ToInt32(personLibrary.Picture.Id));
                // -- Contacts
                command.Parameters.AddWithValue("@Email", personLibrary.Contact.Email);
                command.Parameters.AddWithValue("@Mobile", personLibrary.Contact.Mobile);
                command.Parameters.AddWithValue("@ContactId", Convert.ToInt32(personLibrary.Contact.Id));
                // -- Address
                command.Parameters.AddWithValue("@Country", personLibrary.Address.Country);
                command.Parameters.AddWithValue("@States", personLibrary.Address.States);
                command.Parameters.AddWithValue("@City", personLibrary.Address.City);
                command.Parameters.AddWithValue("@Neighborhoods", personLibrary.Address.Neighborhoods);
                command.Parameters.AddWithValue("@AddressId", Convert.ToInt32(personLibrary.Address.Id));
                // -- Person
                command.Parameters.AddWithValue("@FirstName", personLibrary.FirstName);
                command.Parameters.AddWithValue("@LastName", personLibrary.LastName);
                command.Parameters.AddWithValue("@Genre", personLibrary.Genre);
                command.Parameters.AddWithValue("@Age", personLibrary.Age);
                command.Parameters.AddWithValue("@Birthday", personLibrary.Birthday.ToString("d"));

                _sqlConnection.Open();
                var running = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
        public new void Delete(int? Id)
        {
            using (SqlCommand command = new SqlCommand("DeletePerson", _sqlConnection))
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