using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using LibraryCommon;

namespace LibraryData
{
    public static class BooksData
    {
        //const string connString =
        //    "Data Source=BIG-YELLOW;Initial Catalog=LibraryApp;Integrated Security=True";
        const string connString =
            "Data Source=DESKTOP-GPLJ87I;Initial Catalog=LibraryApp;Integrated Security=True";

        // Add the given book to the table
        public static int CreateBook(string title, string author)
        {
            try
            {
                int _pk = 0;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramTitle = _sqlCommand.CreateParameter();
                        _paramTitle.DbType = DbType.String;
                        _paramTitle.ParameterName = "@Title";
                        _paramTitle.Value = title;
                        _sqlCommand.Parameters.Add(_paramTitle);

                        SqlParameter _paramAuthor = _sqlCommand.CreateParameter();
                        _paramAuthor.DbType = DbType.String;
                        _paramAuthor.ParameterName = "@Author";
                        _paramAuthor.Value = author;
                        _sqlCommand.Parameters.Add(_paramAuthor);

                        SqlParameter _paramBookIDReturn = _sqlCommand.CreateParameter();
                        _paramBookIDReturn.DbType = DbType.Int32;
                        _paramBookIDReturn.ParameterName = "@paramOutBookID";
                        var pk = _sqlCommand.Parameters.Add(_paramBookIDReturn);
                        _paramBookIDReturn.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        _pk = (int)_paramBookIDReturn.Value;
                        con.Close();
                        return _pk;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return 0;
            }

        }

        // Remove the given book from the table
        public static void DeleteBook(int bookID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("DeleteBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramBookID = _sqlCommand.CreateParameter();
                        _paramBookID.DbType = DbType.String;
                        _paramBookID.ParameterName = "@paramBookID";
                        _paramBookID.Value = bookID;
                        _sqlCommand.Parameters.Add(_paramBookID);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }

        // Edit the author and title of the given book
        public static void EditBook(int bookID, string title, string author)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("EditBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramBookID = _sqlCommand.CreateParameter();
                        _paramBookID.DbType = DbType.Int32;
                        _paramBookID.ParameterName = "@BookID";
                        _paramBookID.Value = bookID;
                        _sqlCommand.Parameters.Add(_paramBookID);

                        SqlParameter _title = _sqlCommand.CreateParameter();
                        _title.DbType = DbType.String;
                        _title.ParameterName = "@Title";
                        _title.Value = title;
                        _sqlCommand.Parameters.Add(_title);

                        SqlParameter _author = _sqlCommand.CreateParameter();
                        _author.DbType = DbType.String;
                        _author.ParameterName = "@Author";
                        _author.Value = author;
                        _sqlCommand.Parameters.Add(_author);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }

        // Returns the Books table as a List of Book objects
        public static List<Book> GetBooks()
        {
            try
            {
                List<Book> books = new List<Book>();
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetBooks", dbcon))
                    {
                        dbcon.Open(); // Open SqlConnection

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read in Customer records from SqlDataReader
                            while (reader.Read())
                            {
                                Book _book = new Book()
                                {
                                    BookID = reader["BookID"] is DBNull ? 0 : (int)reader["BookID"],
                                    CheckedOutBy = reader["CheckedOutBy"] is DBNull ? 0 : (int)reader["CheckedOutBy"],
                                    OnHoldBy = reader["OnHoldBy"] is DBNull ? 0 : (int)reader["OnHoldBy"],
                                    Title = reader["Title"] is DBNull ? "" : (string)reader["Title"],
                                    Author = reader["Author"] is DBNull ? "" : (string)reader["Author"]
                                };

                                books.Add(_book);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return books;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return null;
            }
        }

        //// Returns the Books table as a List of Book objects
        //public static List<Book> SearchBooks(string searchExpression)
        //{
        //    try
        //    {
        //        List<Book> books = new List<Book>();
        //        using (SqlConnection dbcon = new SqlConnection(connString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("SearchBooks", dbcon))
        //            {
        //                dbcon.Open(); // Open SqlConnection

        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                SqlParameter _searchExpression = cmd.CreateParameter();
        //                _searchExpression.DbType = DbType.String;
        //                _searchExpression.ParameterName = "@SearchExpression";
        //                _searchExpression.Value = searchExpression;
        //                cmd.Parameters.Add(_searchExpression);

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    // Read in Customer records from SqlDataReader
        //                    while (reader.Read())
        //                    {
        //                        Book _book = new Book()
        //                        {
        //                            BookID = reader["BookID"] is DBNull ? 0 : (int)reader["BookID"],
        //                            CheckedOutBy = reader["CheckedOutBy"] is DBNull ? 0 : (int)reader["CheckedOutBy"],
        //                            OnHoldBy = reader["OnHoldBy"] is DBNull ? 0 : (int)reader["OnHoldBy"],
        //                            Title = reader["Title"] is DBNull ? "" : (string)reader["Title"],
        //                            Author = reader["Author"] is DBNull ? "" : (string)reader["Author"]
        //                        };
        //                        books.Add(_book);
        //                    }
        //                }
        //            }
        //            dbcon.Close();
        //        }
        //        return books;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLogData.CreateExceptionLog(ex);
        //        return null;
        //    }
        //}

        // Sets the CheckedOutBy of the given Book to the given UserID
        public static void CheckoutBook(int bookID, int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CheckoutBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramBookID = _sqlCommand.CreateParameter();
                        _paramBookID.DbType = DbType.String;
                        _paramBookID.ParameterName = "@BookID";
                        _paramBookID.Value = bookID;
                        _sqlCommand.Parameters.Add(_paramBookID);

                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.String;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = userID;
                        _sqlCommand.Parameters.Add(_paramUserID);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }

        // Sets the CheckOutBy of the given Book to the OnHoldBy userID
        public static void ReturnBook(int bookID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("ReturnBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramBookID = _sqlCommand.CreateParameter();
                        _paramBookID.DbType = DbType.String;
                        _paramBookID.ParameterName = "@BookID";
                        _paramBookID.Value = bookID;
                        _sqlCommand.Parameters.Add(_paramBookID);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }

        // Sets the OnHoldBy of the given Book to the given UserID
        public static void HoldBook(int bookID, int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("HoldBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramBookID = _sqlCommand.CreateParameter();
                        _paramBookID.DbType = DbType.String;
                        _paramBookID.ParameterName = "@BookID";
                        _paramBookID.Value = bookID;
                        _sqlCommand.Parameters.Add(_paramBookID);

                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.String;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = userID;
                        _sqlCommand.Parameters.Add(_paramUserID);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }
    }
}