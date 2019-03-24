using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_ADO_shop.Entities;
using System.Data.SqlClient;
using System.Configuration;

namespace HW_ADO_shop.Repositories
{
    public class QrCodeRepository : IRepository<QrCodeEntity>
    {
        public void Add(QrCodeEntity obj)
        {
            string sqlCommand = $"INSERT INTO qrCodes(UserId, Content, QRcode_type) " +
                $"VALUES (@userId, @content, @qrCodeType)";

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userIdParam = new SqlParameter("@userId", obj.UserId);
                    SqlParameter contentParam = new SqlParameter("@content", obj.Content);
                    SqlParameter qrCodeTypeParam = new SqlParameter("@qrCodeType", (int)obj.QrCodeType);

                    command.Parameters.Add(userIdParam);
                    command.Parameters.Add(contentParam);
                    command.Parameters.Add(qrCodeTypeParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int id)
        {
            string sqlCommand = $"DELETE FROM qrCodes WHERE Id=@id";
            int result;
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter qid = new SqlParameter("@id", id);    
                    command.Parameters.Add(qid);
                    result=command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public QrCodeEntity Read(int id)
        {
            QrCodeEntity entity = new QrCodeEntity();
            string sql = $"SELECT * FROM qrCodes WHERE ID= @id";
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                SqlParameter IdParam = new SqlParameter("@id", id);

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdParam);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            entity.Id = Int32.Parse(reader["Id"].ToString());
                            entity.Content = (byte[])reader["Content"];
                            entity.QrCodeType = (QrCodeType)reader["QrCodeType"];
                        }
                    }
                    else throw new Exception("No data found!");
                }
                return entity;
            }
        }
        public int Update(int id, QrCodeEntity updated)
        {
            string sqlCommand = $"UPDATE qrCodes SET UserId=@userId, Content=@content, QRcode_type=@qrCodeType)";
            int result;

            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter userIdParam = new SqlParameter("@userId", updated.UserId);
                    SqlParameter contentParam = new SqlParameter("@content", updated.Content);
                    SqlParameter qrCodeTypeParam = new SqlParameter("@qrCodeType", (int)updated.QrCodeType);

                    command.Parameters.Add(userIdParam);
                    command.Parameters.Add(contentParam);
                    command.Parameters.Add(qrCodeTypeParam);
                    result=command.ExecuteNonQuery();
                }
            }
            return result;
        }
        public string GetConnectionString()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnectionString"]
                .ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("No connection string provided!");

            return connectionString;
        }
    }
}
