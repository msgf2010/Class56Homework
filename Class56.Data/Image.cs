using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Class56.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int? UserId { get; set; }
        public int? Views { get; set; }
    }

    public class ImageManager
    {
        private string _connectionString;

        public ImageManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveImage(Image image)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Images (Description, FileName, UserId, Views) " +
                                  "VALUES (@desc, @fileName, @userId, 0)";
                cmd.Parameters.AddWithValue("@desc", image.Description);
                cmd.Parameters.AddWithValue("@fileName", image.FileName);
                cmd.Parameters.AddWithValue("@userId", image.UserId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Image> Get(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                var list = new List<Image>();
                cmd.CommandText = "SELECT * FROM Images WHERE UserId = @userId";
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Image
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        FileName = (string)reader["FileName"],
                        Views = (int)reader["Views"]
                    });
                }

                return list;
            }
        }

        public void IncrementViewCount(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE Images SET Views = Views + 1 WHERE UserId = @id";
                cmd.Parameters.AddWithValue("@id", userId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
