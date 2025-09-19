using backend.Models;
using Microsoft.Data.SqlClient;

namespace backend.Repositories
{
    public class DesignationRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public IEnumerable<Designation> GetAll()
        {
            var designations = new List<Designation>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM Designation", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                designations.Add(MapDesignation(reader));
            }

            return designations;
        }

        public Designation? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM Designation WHERE Designation_Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapDesignation(reader);

            return null;
        }

        public bool Add(Designation des)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand(
                "INSERT INTO Designation (Name) VALUES (@Name)", conn);
            cmd.Parameters.AddWithValue("@Name", des.Name);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("DELETE FROM Designation WHERE Designation_Id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        private static Designation MapDesignation(SqlDataReader reader)
        {
            return new Designation
            {
                Designation_Id = reader.GetInt32(reader.GetOrdinal("Designation_Id")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };
        }
    }
}
