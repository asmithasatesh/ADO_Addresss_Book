using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_AddressBook
{
    public class TransactionClass
    {
        //Give path for Database Connection
        public static string connection = @"Server=.;Database=Address_Book_Service_DB;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection SqlConnection = new SqlConnection(connection);
        ContactDataManager addressBook = new ContactDataManager();
        public int AlterTableaddStartDate()
        {
            int result = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {

                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "alter table Contact_Person add Date_Added Date";
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            SqlConnection.Close();
            return result;
        }
        //Set date value based on query
        public int SetStartDateValue(string query)
        {
            int result = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = query;
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            SqlConnection.Close();
            return result;
        }
        //Insert into Table using Transactions
        public int InsertIntoTables()
        {
            int result = 0;
            using (SqlConnection)
            {
                SqlConnection.Open();

                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Insert data into Table
                    sqlCommand.CommandText = "insert into Contact_Person values(1,'Neha','Kejriwal','3645 Catherine Street','Mysore','Karnataka',223001,8842905050,'neha@gmail.com','2019-02-03')";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "insert into TypeManager values(3,5)";
                    sqlCommand.ExecuteNonQuery();
                    //Commit 
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated!");
                    result = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    result = 1;
                }
            }
            return result;
        }
    }
}
