﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DVLD_DataAccess
{
    public class clsUserData
    {
        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from Users where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                    isFound = false;
                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                    isFound = false;
                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from Users where UserName=@UserName and Password=@Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];
                }
                else
                    isFound = false;
                reader.Close();

            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Users (PersonID,UserName,Password,IsActive) values (@PersonID,@UserName,@Password,@IsActive) SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update  Users  
                            set PersonID = @PersonID,
                                UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive
                                where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }
        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool DoesPersonHaveUser44(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        

    }
}
