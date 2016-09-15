using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace _wp_upload_point.Database
{
    class DataGeneral
    {
        private static SqlConnection createConnection()
        {
            string sqlCn = Properties.Settings.Default.dsn;
            SqlConnection cn = new SqlConnection(sqlCn);
            cn.Open();
            return cn;
        }

        /// <summary>
        /// Wraps the SqlCommand.ExecuteReader() method. 
        /// </summary>
        /// <param name="procedureNameOrSql">Stored procedure name or a Sql statement.</param>
        /// <param name="parameters">List of SqlParameter. Set to null if no parameters.</param>
        /// <param name="isStoredProcedure">True if the procedureNameOrSql is a stored procedure, false if it is a SQL statement.</param>
        /// <returns>Returns a SqlDataReader which MUST be wrapped in a using statement so that its SqlConnecion is closed as soon as the SqlDataReader is disposed.</returns>
        public static SqlDataReader ExecuteReader(string procedureNameOrSql, List<SqlParameter> parameters, bool isStoredProcedure)
        {
            //IMPORTANT: make sure you wrap the returned SqlDataReader in a using statement so that it is closed. (You do not need to close the SqlConnection object.)
            SqlConnection cn = createConnection();

            SqlCommand cmd = new SqlCommand(procedureNameOrSql, cn);

            if (isStoredProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Wraps the SqlCommand.ExecuteScalar() method. 
        /// </summary>
        /// <param name="procedureNameOrSql">Stored procedure name or a Sql statement.</param>
        /// <param name="parameters">List of SqlParameter. Set to null if no parameters.</param>
        /// <param name="isStoredProcedure">True if the procedureNameOrSql is a stored procedure, false if it is a SQL statement.</param>
        /// <returns>Returns the first value of the first row of the Sql Statement.</returns>
        public static object ExecuteScalar(string procedureNameOrSql, List<SqlParameter> parameters, bool isStoredProcedure)
        {
            object scalarValue;
            using (SqlConnection cn = createConnection())
            {
                SqlCommand cmd = new SqlCommand(procedureNameOrSql, cn);
                if (isStoredProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());
                scalarValue = cmd.ExecuteScalar();
            }
            return scalarValue;
        }

        /// <summary>
        /// Wraps the SqlCommand.ExecuteNonQuery() method. 
        /// </summary>
        /// <param name="procedureNameOrSql">Stored procedure name or a Sql statement.</param>
        /// <param name="parameters">List of SqlParameter. Set to null if no parameters.</param>
        /// <param name="isStoredProcedure">True if the procedureNameOrSql is a stored procedure, false if it is a SQL statement</param>
        /// <returns>Returns the number of rows affected by ExecuteNonQuery().</returns>
        public static int ExecuteNonQuery(string procedureNameOrSql, List<SqlParameter> parameters, bool isStoredProcedure)
        {
            int rowsAffected;
            using (SqlConnection cn = createConnection())
            {
                SqlCommand cmd = new SqlCommand(procedureNameOrSql, cn);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());
                if (isStoredProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;

                rowsAffected = cmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        public class ParamBuilder
        {
            private readonly List<SqlParameter> _parameters = new List<SqlParameter>();
            public List<SqlParameter> Parameters
            {
                get
                {
                    return _parameters;
                }
            }

            public void AddParam(SqlDbType sqlDbType, string paramName, object paramVal)
            {
                SqlParameter p = new SqlParameter(paramName, sqlDbType);
                p.Value = paramVal ?? DBNull.Value;
                _parameters.Add(p);
            }

            public SqlParameter AddOutputParam(SqlDbType sqlDbType, string paramName)
            {
                SqlParameter p = new SqlParameter(paramName, sqlDbType);
                p.Direction = ParameterDirection.Output;
                _parameters.Add(p);
                return p;
            }
        }

    }
}
