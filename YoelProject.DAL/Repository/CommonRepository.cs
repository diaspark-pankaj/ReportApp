using YoelProject.DAL;
using YoelProject.DAL.SqlConnectionHelper;
using YoelProject.DAL.DataModels;
using YoelProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace YoelProject.DAL.Repository
{
    public class CommonRepository : IDisposable
    {

        #region Exigent Connection
        /// <summary>
        /// ExecuteSP on Exigent connection
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static DataSet ExecuteSP(string spName)
        {
            DataSet ds = new DataSet();
            using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetExigentConnection())
            {
                var da = new SqlDataAdapter(spName, currsqlConnection);
                da.Fill(ds);
                return ds;
            }
        }

        public static DataSet ExecuteQueryForGrid(string commandText, string DBConnection, CommandType commandType, params SqlParameter[] parameters)
        {
            ReportApplicationEntities db = new ReportApplicationEntities();
            DBConnection = db.Database.Connection.ConnectionString.ToString();
            using (var connection = new SqlConnection(DBConnection))
            using (var command = new SqlCommand(commandText, connection))
            {
                DataSet ds = new DataSet();
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                connection.Close();
                return ds;
            }
        }
        public static string GetExceptionLog(System.Exception ex)
        {
            System.Text.StringBuilder errorMessageText = new System.Text.StringBuilder();
            if (ex != null && !string.IsNullOrEmpty(ex.Message))
            {
                errorMessageText.Append("Exception" + ex.Message + System.Environment.NewLine + (ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            string strPath = HttpContext.Current.Server.MapPath("~/Log/ImportErrors.log");

            if (!System.IO.File.Exists(strPath))
            {
                System.IO.File.Create(strPath);
            }

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] formByte = encoding.GetBytes(errorMessageText.ToString());

            using (Stream file = File.OpenWrite(strPath))
            {
                file.Write(formByte, 0, formByte.Length);
            }

            return errorMessageText.ToString();
        }

        /// <summary>
        /// ExecuteSP on Exigent connection
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteSP_Exigent(string spName, IEnumerable<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetExigentConnection())
            {
                var cmdExecuteSP = new SqlCommand
                {
                    CommandText = spName,
                    CommandType = CommandType.StoredProcedure,
                    Connection = currsqlConnection
                };

                foreach (var sqlParam in parameters)
                {
                    cmdExecuteSP.Parameters.Add(sqlParam);
                }
                var da = new SqlDataAdapter
                {
                    SelectCommand = cmdExecuteSP
                };
                da.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// Execute given query on Exigent connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetViewResult<T>(string query)
        {
            using (var db = new ReportApplicationEntities())
            {
                return db.Database.SqlQuery<T>(query).ToList();
            }
        }

        /// <summary>
        /// Execute given query on Exigent connection for single object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public T GetSingleViewResult<T>(string query)
        {
            using (var db = new ReportApplicationEntities())
            {
                return db.Database.SqlQuery<T>(query).FirstOrDefault();
            }
        }

        /// <summary>
        /// Execute given query on Exigent connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int ExecuteQuery(string query)
        {
            using (var db = new ReportApplicationEntities())
            {
                return db.Database.ExecuteSqlCommand(query);
            }
        }

        /// <summary>
        /// Execute SP on connection of given context and returns first column of first row
        /// </summary>
        /// <param name="spName">Name of Stored Procedure to be executed</param>
        /// <param name="parameters">Collection of SQLParamaters to be passed</param>
        /// <param name="Dbcontext">Name of DB Context</param>
        /// <returns></returns>
        public static object ExecuteScalarSP(string spName, IEnumerable<SqlParameter> parameters, string Dbcontext)
        {
            if (Dbcontext == "Exigent")
            {
                using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetExigentConnection())
                {
                    currsqlConnection.Open();
                    var cmdExecuteSP = new SqlCommand
                    {
                        CommandText = spName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = currsqlConnection
                    };
                    foreach (var sqlParam in parameters)
                    {
                        cmdExecuteSP.Parameters.Add(sqlParam);
                    }
                    return cmdExecuteSP.ExecuteScalar();
                }
            }
            else
            {
                using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetClientConnection())
                {
                    currsqlConnection.Open();
                    var cmdExecuteSP = new SqlCommand
                    {
                        CommandText = spName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = currsqlConnection
                    };

                    foreach (var sqlParam in parameters)
                    {
                        cmdExecuteSP.Parameters.Add(sqlParam);
                    }
                    return cmdExecuteSP.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Client Connection

        /// <summary>
        /// Execute SP on Client connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteSP(string spName, IEnumerable<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetClientConnection())
            {
                var cmdExecuteSP = new SqlCommand
                {
                    CommandText = spName,
                    CommandType = CommandType.StoredProcedure,
                    Connection = currsqlConnection
                };

                foreach (var sqlParam in parameters)
                {
                    cmdExecuteSP.Parameters.Add(sqlParam);
                }
                var da = new SqlDataAdapter
                {
                    SelectCommand = cmdExecuteSP
                };
                da.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// Execute given query on Client connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetViewResult_Client<T>(string query)
        {
            using (var db = new ReportApplicationEntities())
            {
                return db.Database.SqlQuery<T>(query).ToList();
            }
        }

        #endregion

        /// <summary>
        /// Execute SP on connection of given context
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <param name="Dbcontext"></param>
        /// <returns></returns>
        public static int ExecuteDeleteSP(string spName, IEnumerable<SqlParameter> parameters, string Dbcontext)
        {
            if (Dbcontext == "Exigent")
                using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetExigentConnection())
                {
                    currsqlConnection.Open();
                    var cmdExecuteSP = new SqlCommand
                    {
                        CommandText = spName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = currsqlConnection
                    };
                    foreach (var sqlParam in parameters)
                    {
                        cmdExecuteSP.Parameters.Add(sqlParam);
                    }
                    return cmdExecuteSP.ExecuteNonQuery();
                }
            else
            {
                using (var currsqlConnection = SqlConnectionHelper.SqlConnectionHelper.GetClientConnection())
                {
                    currsqlConnection.Open();
                    var cmdExecuteSP = new SqlCommand
                    {
                        CommandText = spName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = currsqlConnection
                    };

                    foreach (var sqlParam in parameters)
                    {
                        cmdExecuteSP.Parameters.Add(sqlParam);
                    }
                    return cmdExecuteSP.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_context != null)
                //{
                //    _context.Dispose();
                //    _context = null;
                //}
            }
        }

    }
}