using PersonalInfoWebAPI.Models;
using System.Data;
using Npgsql;
namespace PersonalInfoWebAPI.DAO
{
    public class PersonDAOImplementation : IPersonDAO
    {
        private readonly NpgsqlConnection _connection;

        public PersonDAOImplementation(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<PersonalInfo> GetPersonalInfoById(int id)
        {
            PersonalInfo personalInfo = null;
            string errorMessage = string.Empty;
            string query = @"SELECT * FROM person.personal_infos WHERE id = @id";
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    using (var command = new NpgsqlCommand(query, _connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    personalInfo = new PersonalInfo
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        DateOfBirth = reader.GetDateTime(2),
                                        ResidentialAddress = reader.GetString(3),
                                        PermanentAddress = reader.GetString(4),
                                        PhoneNumber = reader.GetString(5),
                                        Email = reader.GetString(6),
                                        MaritalStatus = reader.GetString(7),
                                        Gender = reader.GetString(8),
                                        Occupation = reader.GetString(9),
                                        AadharCardNumber = reader.GetString(10),
                                        PANNumber = reader.GetString(11)
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                errorMessage = e.Message;
                Console.WriteLine("------- Exception ------- " + errorMessage);
            }
            return personalInfo;
        }

        public async Task<List<PersonalInfo>> GetPersonalInfos()
        {
            var personalInfos = new List<PersonalInfo>();
            string query = "SELECT * FROM person.personal_infos";
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    using (var command = new NpgsqlCommand(query, _connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var personalInfo = new PersonalInfo
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    DateOfBirth = reader.GetDateTime(2),
                                    ResidentialAddress = reader.GetString(3),
                                    PermanentAddress = reader.GetString(4),
                                    PhoneNumber = reader.GetString(5),
                                    Email = reader.GetString(6),
                                    MaritalStatus = reader.GetString(7),
                                    Gender = reader.GetString(8),
                                    Occupation = reader.GetString(9),
                                    AadharCardNumber = reader.GetString(10),
                                    PANNumber = reader.GetString(11)
                                };
                                personalInfos.Add(personalInfo);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------- Exception ------- " + e.Message);
            }
            return personalInfos;
        }

        public async Task<int> InsertPersonalInfo(PersonalInfo personalInfo)
        {
            int rowsInserted = 0;
            string insertQuery = @"INSERT INTO person.personal_infos (name, date_of_birth, residential_address, permanent_address, 
                                   phone_number, email_address, marital_status, gender, occupation, aadhar_card_number, pan_number)
                                   VALUES (@name, @dob, @residentialAddress, @permanentAddress, @phoneNumber, 
                                   @emailAddress, @maritalStatus, @gender, @occupation, @aadharCardNumber, @panNumber)";

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    using (var insertCommand = new NpgsqlCommand(insertQuery, _connection))
                    {
                        insertCommand.Parameters.AddWithValue("@name", personalInfo.Name);
                        insertCommand.Parameters.AddWithValue("@dob", personalInfo.DateOfBirth);
                        insertCommand.Parameters.AddWithValue("@residentialAddress", personalInfo.ResidentialAddress);
                        insertCommand.Parameters.AddWithValue("@permanentAddress", personalInfo.PermanentAddress);
                        insertCommand.Parameters.AddWithValue("@phoneNumber", personalInfo.PhoneNumber);
                        insertCommand.Parameters.AddWithValue("@emailAddress", personalInfo.Email);
                        insertCommand.Parameters.AddWithValue("@maritalStatus", personalInfo.MaritalStatus);
                        insertCommand.Parameters.AddWithValue("@gender", personalInfo.Gender);
                        insertCommand.Parameters.AddWithValue("@occupation", personalInfo.Occupation);
                        insertCommand.Parameters.AddWithValue("@aadharCardNumber", personalInfo.AadharCardNumber);
                        insertCommand.Parameters.AddWithValue("@panNumber", personalInfo.PANNumber);

                        rowsInserted = await insertCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------- Exception ------- " + e.Message);
            }
            return rowsInserted;
        }

        public async Task<int> UpdatePersonalInfo(PersonalInfo personalInfo)
        {
            int rowsUpdated = 0;
            string updateQuery = @"UPDATE person.personal_infos SET 
                                   name = @name, 
                                   date_of_birth = @dob, 
                                   residential_address = @residentialAddress, 
                                   permanent_address = @permanentAddress, 
                                   phone_number = @phoneNumber, 
                                   email_address = @emailAddress, 
                                   marital_status = @maritalStatus, 
                                   gender = @gender, 
                                   occupation = @occupation, 
                                   aadhar_card_number = @aadharCardNumber, 
                                   pan_number = @panNumber 
                                   WHERE id = @id";

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    using (var updateCommand = new NpgsqlCommand(updateQuery, _connection))
                    {
                        updateCommand.Parameters.AddWithValue("@id", personalInfo.Id);
                        updateCommand.Parameters.AddWithValue("@name", personalInfo.Name);
                        updateCommand.Parameters.AddWithValue("@dob", personalInfo.DateOfBirth);
                        updateCommand.Parameters.AddWithValue("@residentialAddress", personalInfo.ResidentialAddress);
                        updateCommand.Parameters.AddWithValue("@permanentAddress", personalInfo.PermanentAddress);
                        updateCommand.Parameters.AddWithValue("@phoneNumber", personalInfo.PhoneNumber);
                        updateCommand.Parameters.AddWithValue("@emailAddress", personalInfo.Email);
                        updateCommand.Parameters.AddWithValue("@maritalStatus", personalInfo.MaritalStatus);
                        updateCommand.Parameters.AddWithValue("@gender", personalInfo.Gender);
                        updateCommand.Parameters.AddWithValue("@occupation", personalInfo.Occupation);
                        updateCommand.Parameters.AddWithValue("@aadharCardNumber", personalInfo.AadharCardNumber);
                        updateCommand.Parameters.AddWithValue("@panNumber", personalInfo.PANNumber);

                        rowsUpdated = await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------- Exception ------- " + e.Message);
            }
            return rowsUpdated;
        }

        public async Task<int> DeletePersonalInfo(int id)
        {
            int rowsDeleted = 0;
            string deleteQuery = "DELETE FROM person.personal_infos WHERE id = @id";

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    using (var deleteCommand = new NpgsqlCommand(deleteQuery, _connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@id", id);
                        rowsDeleted = await deleteCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------- Exception ------- " + e.Message);
            }
            return rowsDeleted;
        }
    }
}
