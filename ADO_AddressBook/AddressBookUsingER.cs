using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_AddressBook
{
    public class AddressBookUsingER
    {
        //Give path for Database Connection
        public static string connection = @"Server=.;Database=Address_Book_Service_DB;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
        ContactDataManager addressBook = new ContactDataManager();

        //UseCase 5: Ability to Retrieve Person belonging to a City or State from the Address Book
        public string PrintDataBasedOnCityAfterER(string City,string State)
        {
            string nameList = "";
            //query to be executed
            string query = @"select Address_BookName,FirstName,SecondName,Address,City,State,zip,PhoneNumber,Email,ContactType_Name from Address_Book Full JOIN Contact_Person on Address_Book.Address_BookID = AddressBook_ID Full JOIN TypeManager on TypeManager.Contact_Identity = Contact_ID Full JOIN ContactType on TypeManager.ContactType_Identity = ContactType_ID";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    DisplayEmployeeDetails(sqlDataReader);
                    nameList += sqlDataReader["FirstName"].ToString() + " ";
                }
            }
            return nameList;
        }
        //UC 6: Ability to Retrieve Count of Person belonging to a City or State
        public string PrintCountBasedOnCityAndStateNameAfterER()
        {
            string nameList = "";
            //query to be executed
            string query = @"select Count(*),state,City from Contact_Person  INNER JOIN  Address_Book on Address_Book.Address_BookID=AddressBook_ID  Group by state,City";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("{0} \t {1} \t {2}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                    nameList += sqlDataReader[0].ToString() + " ";
                }
            }
            else
            {
                return null;
            }
            return nameList;
        }
        //UC 7: Ability to retrieve entries sorted alphabetically
        public string PrintSortedNameBasedOnCityAfterER(string city)
        {
            string nameList = "";
            //query to be executed
            string query = "select Address_BookName,FirstName,SecondName,Address,City,State,zip,PhoneNumber,Email,ContactType_Name from Contact_Person  INNER JOIN  Address_Book on Address_Book.Address_BookID=AddressBook_ID and (City='" + city+"') INNER JOIN TypeManager on TypeManager.Contact_Identity=Contact_ID INNER JOIN ContactType on TypeManager.ContactType_Identity=ContactType_ID order by(FirstName)";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    DisplayEmployeeDetails(sqlDataReader);
                    nameList += sqlDataReader["FirstName"].ToString() + " ";
                }
            }
            return nameList;
        }
        //UC 8: Ability to get number of contact persons by Type
        public string PrintCountBasedOnAddressBookTypeAfterER()
        {
            string nameList = "";
            //query to be executed
            string query = @"select Count(*) as NumberOfContacts,ContactType.ContactType_Name from Contact_Person  INNER JOIN  Address_Book on Address_Book.Address_BookID=AddressBook_ID INNER JOIN TypeManager on TypeManager.Contact_Identity=Contact_ID INNER JOIN ContactType on TypeManager.ContactType_Identity=ContactType_ID Group by ContactType_Name";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    {
                        Console.WriteLine("{0} \t {1}", sqlDataReader[0], sqlDataReader[1]);
                        nameList += sqlDataReader[0].ToString() + " ";
                    }
                }
            }
            return nameList;
        }
        //Display all Object details
        public void DisplayEmployeeDetails(SqlDataReader sqlDataReader)
        {
            addressBook.FirstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.LastName = Convert.ToString(sqlDataReader["SecondName"]);
            addressBook.Address = Convert.ToString(sqlDataReader["Address"] + " " + sqlDataReader["City"] + " " + sqlDataReader["State"] + " " + sqlDataReader["zip"]);
            addressBook.PhoneNumber = Convert.ToInt64(sqlDataReader["PhoneNumber"]);
            addressBook.Email = Convert.ToString(sqlDataReader["Email"]);
            addressBook.AddressBookName = Convert.ToString(sqlDataReader["Address_BookName"]);
            addressBook.Type = Convert.ToString(sqlDataReader["ContactType_Name"]);
            Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5} \t {6}", addressBook.FirstName, addressBook.LastName, addressBook.Address, addressBook.PhoneNumber, addressBook.Email, addressBook.AddressBookName, addressBook.Type);

        }
    }
}
