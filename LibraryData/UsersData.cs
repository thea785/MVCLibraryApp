
using System.Data.SqlClient;
using System.Data;
using System;

namespace LibraryData
{
	public static class UsersData
	{
        const string connString =
            "Data Source=DESKTOP-GPLJ87I;Initial Catalog=LibraryApp;Integrated Security=True";

        public static int CreateUser(int roleID, string email, string firstName, string lastName, string hashedPassword, string salt)
        {
            try
            {
                int _pk = 0;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _RoleID = _sqlCommand.CreateParameter();
                        _RoleID.DbType = DbType.Int32;
                        _RoleID.ParameterName = "@RoleID";
                        _RoleID.Value = roleID;
                        _sqlCommand.Parameters.Add(_RoleID);

                        SqlParameter _Email = _sqlCommand.CreateParameter();
                        _Email.DbType = DbType.String;
                        _Email.ParameterName = "@Email";
                        _Email.Value = email;
                        _sqlCommand.Parameters.Add(_Email);

                        SqlParameter _FirstName = _sqlCommand.CreateParameter();
                        _FirstName.DbType = DbType.String;
                        _FirstName.ParameterName = "@FirstName";
                        _FirstName.Value = firstName;
                        _sqlCommand.Parameters.Add(_FirstName);

                        SqlParameter _LastName = _sqlCommand.CreateParameter();
                        _LastName.DbType = DbType.String;
                        _LastName.ParameterName = "@LastName";
                        _LastName.Value = firstName;
                        _sqlCommand.Parameters.Add(_LastName);

                        SqlParameter _HashedPassword = _sqlCommand.CreateParameter();
                        _HashedPassword.DbType = DbType.String;
                        _HashedPassword.ParameterName = "@HashedPassword";
                        _HashedPassword.Value = hashedPassword;
                        _sqlCommand.Parameters.Add(_HashedPassword);

                        SqlParameter _Salt = _sqlCommand.CreateParameter();
                        _Salt.DbType = DbType.String;
                        _Salt.ParameterName = "@Salt";
                        _Salt.Value = salt;
                        _sqlCommand.Parameters.Add(_Salt);

                        SqlParameter _paramUserIDReturn = _sqlCommand.CreateParameter();
                        _paramUserIDReturn.DbType = DbType.Int32;
                        _paramUserIDReturn.ParameterName = "@paramOutUserID";
                        var pk = _sqlCommand.Parameters.Add(_paramUserIDReturn);
                        _paramUserIDReturn.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        _pk = (int)_paramUserIDReturn.Value;
                        con.Close();
                        return _pk;
                    }
                }
            }
            catch (Exception ex)
            {
                //ExceptionLogDatabase.CreateExceptionLog(ex);
                return 0;
            }
        }
    }
}
