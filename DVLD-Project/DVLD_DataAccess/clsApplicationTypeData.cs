﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,ref string ApplicationTypeTitle,ref float ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;

                    ApplicationTypeTitle = (string)Reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(Reader["ApplicationFees"]);
                }
                else
                {
                    isFound = false;
                }
                Reader.Close();
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

        public static DataTable GetAllApplicationTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeTitle";

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
                // Console.WriteLine("Error: " + ex.Message);
                clsGlobal.ExceptionHandling(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public static int AddNewApplicationType(string Title,float Fees)
        {
            int ApplicationTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
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
            return ApplicationTypeID;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  ApplicationTypes  
                            set ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                                where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
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
