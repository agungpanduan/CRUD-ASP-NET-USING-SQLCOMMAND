using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRUDUsingSQLCommand.Models.Commons;

namespace CRUDUsingSQLCommand.Models.CRUDStoreProcedures
{
    public class CRUDStoreProceduresRepo
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        SqlConnection connection;

        public List<CRUDStoreProcedures> getAllDataUsingProcedure(CRUDStoreProcedures data, int rowStart, int rowEnd)
        {
            List<CRUDStoreProcedures> ListDataClass = new List<CRUDStoreProcedures>();

            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("SP_GetDataClass", connection);
            com.CommandType = CommandType.StoredProcedure;

            //CARA 1
            if (data.Name == null || data.Email == null)
            {
                com.Parameters.AddWithValue("@Name", DBNull.Value);
                com.Parameters.AddWithValue("@Email", DBNull.Value);
                com.Parameters.AddWithValue("@RowStart", rowStart);
                com.Parameters.AddWithValue("@RowEnd", rowEnd);
            }
            else
            {
                com.Parameters.AddWithValue("@Name", data.Name);
                com.Parameters.AddWithValue("@Email", data.Email);
                com.Parameters.AddWithValue("@RowStart", rowStart);
                com.Parameters.AddWithValue("@RowEnd", rowEnd);
            }


            //CARA 2
            //if (data.Name == null || data.Gender==null)
            //{
            //    com.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = DBNull.Value;
            //    com.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = DBNull.Value;
            //    com.Parameters.Add("@RowStart", SqlDbType.Int, 30).Value =0;
            //    com.Parameters.Add("@RowEnd", SqlDbType.Int, 30).Value = 10;
            //}
            //else
            //{
            //    com.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = data.Name;
            //    com.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = data.Email;
            //    com.Parameters.Add("@RowStart", SqlDbType.Int, 30).Value = rowStart;
            //    com.Parameters.Add("@RowEnd", SqlDbType.Int, 30).Value = rowEnd;
            //}


            IDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                CRUDStoreProcedures classm = new CRUDStoreProcedures()
                {
                    Name = Convert.ToString(reader["Name"]), //Convert.ToInt32(reader["id"]),
                    Gender = Convert.ToString(reader["Gender"]),
                    Email = Convert.ToString(reader["Email"]),
                    ClassM = Convert.ToString(reader["ClassM"]),
                };

                ListDataClass.Add(classm);
            }

            reader.Close();
            connection.Close();

            return ListDataClass;
        }

        public RepoResult Insert(CRUDStoreProcedures Data)
        {
            SqlTransaction trx = null;
            connection = new SqlConnection(connectionString);
            connection.Open();
            trx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand("[dbo].[SP_InsertData]", connection, trx);
            cmd.CommandType = CommandType.StoredProcedure;
            RepoResult repoResult = new RepoResult();

            var outputErrMesg = new System.Data.SqlClient.SqlParameter("@ro_v_err_mesg", System.Data.SqlDbType.VarChar, 2000);
            outputErrMesg.Direction = System.Data.ParameterDirection.Output;

            var retVal = new System.Data.SqlClient.SqlParameter("@ro_n_return_value", System.Data.SqlDbType.Int);
            retVal.Direction = System.Data.ParameterDirection.Output;
            
            cmd.Parameters.Add(outputErrMesg);
            cmd.Parameters.Add(retVal);
            if (Data.Name == null || Data.Gender==null || Data.Email ==null || Data.ClassM == null)
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@ClassM", SqlDbType.VarChar, 30).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = Data.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Data.Email;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 30).Value = Data.Gender;
                cmd.Parameters.Add("@ClassM", SqlDbType.VarChar, 30).Value = Data.ClassM;
            }

            cmd.ExecuteNonQuery();
            trx.Commit();
            connection.Close();

            repoResult.Result = RepoResult.VALUE_SUCCESS;
            if ((int?)retVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;
                if (outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }
                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }
           
            return repoResult;
        }

        public RepoResult Update(CRUDStoreProcedures Data)
        {
            SqlTransaction trx = null;
            connection = new SqlConnection(connectionString);
            connection.Open();
            trx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand("[dbo].[SP_UpdateData]", connection, trx);
            cmd.CommandType = CommandType.StoredProcedure;
            RepoResult repoResult = new RepoResult();

            var outputErrMesg = new System.Data.SqlClient.SqlParameter("@ro_v_err_mesg", System.Data.SqlDbType.VarChar, 2000);
            outputErrMesg.Direction = System.Data.ParameterDirection.Output;

            var retVal = new System.Data.SqlClient.SqlParameter("@ro_n_return_value", System.Data.SqlDbType.Int);
            retVal.Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.Add(outputErrMesg);
            cmd.Parameters.Add(retVal);
            if (Data.Name == null || Data.Gender == null || Data.Email == null || Data.ClassM == null)
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@ClassM", SqlDbType.VarChar, 30).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = Data.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Data.Email;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 30).Value = Data.Gender;
                cmd.Parameters.Add("@ClassM", SqlDbType.VarChar, 30).Value = Data.ClassM;
            }

            cmd.ExecuteNonQuery();
            trx.Commit();
            connection.Close();

            repoResult.Result = RepoResult.VALUE_SUCCESS;
            if ((int?)retVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;
                if (outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }
                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }

        public RepoResult Delete(CRUDStoreProcedures Data)
        {
            SqlTransaction trx = null;
            connection = new SqlConnection(connectionString);
            connection.Open();
            trx = connection.BeginTransaction();

            SqlCommand cmd = new SqlCommand("[dbo].[SP_DeleteData]", connection, trx);
            cmd.CommandType = CommandType.StoredProcedure;
            RepoResult repoResult = new RepoResult();

            var outputErrMesg = new System.Data.SqlClient.SqlParameter("@ro_v_err_mesg", System.Data.SqlDbType.VarChar, 2000);
            outputErrMesg.Direction = System.Data.ParameterDirection.Output;

            var retVal = new System.Data.SqlClient.SqlParameter("@ro_n_return_value", System.Data.SqlDbType.Int);
            retVal.Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.Add(outputErrMesg);
            cmd.Parameters.Add(retVal);
            if (Data.Name == null || Data.Email == null)
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = Data.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Data.Email;
            }

            cmd.ExecuteNonQuery();
            trx.Commit();
            connection.Close();

            repoResult.Result = RepoResult.VALUE_SUCCESS;
            if ((int?)retVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;
                if (outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }
                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }


        public long getCount(CRUDStoreProcedures data)
        {
            Int32 count;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM [dbo].[CLASS] Where 1=1" +
                               " AND (NULLIF('" + data.Name + "','') IS NULL OR [Name] like '%" + data.Name + "%')" +
                               " AND (NULLIF('" + data.Gender + "','') IS NULL OR [Gender] like '%" + data.Gender + "%')";
                SqlCommand command = new SqlCommand(query, connection);
                count = (Int32)command.ExecuteScalar();
                //count = 
                connection.Close();
            }

            return count;
        }
    }
}