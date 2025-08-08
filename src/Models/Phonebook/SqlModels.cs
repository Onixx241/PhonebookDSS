using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class SqlModel
{
    public string dataSource { get; set; }
    public string initialCatalog { get; set; }
    public string userID { get; set; }
    public string password { get; set; }

    public string currentEmail { get; set; }
    public string sampleString { get; set; }

    public SqlModel(string datasource, string initialcatalog, string userid, string password, string currentUser) 
    {
        this.dataSource = datasource;
        this.initialCatalog = initialcatalog;
        this.userID = userid;
        this.password = password;
        this.currentEmail = currentUser;
    }

    public SqlConnectionStringBuilder BuildConnectionString()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = this.dataSource,
            InitialCatalog = this.initialCatalog,
            UserID = this.userID,
            Password = this.password
        };

        return builder;
    }
    //DataSource=;InitialCatalog=;UserID=;Password=;

    public void CreateContact(Contact contact, string user)
    {
        var builder = BuildConnectionString();

        string connectionString = builder.ConnectionString;

        

        using (SqlConnection conn = new SqlConnection(connectionString)) 
        {

            conn.Open();

            using (SqlCommand cmd = new SqlCommand("dbo.CreateContact",conn)) 
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMAIL", user);
                cmd.Parameters.AddWithValue("@FIRSTNAME", contact.firstName);
                cmd.Parameters.AddWithValue("@LASTNAME", contact.lastName);
                cmd.Parameters.AddWithValue("@ADDRESS", contact.address);
                cmd.Parameters.AddWithValue("@CITY", contact.city);
                cmd.Parameters.AddWithValue("@STATE", contact.State);
                cmd.Parameters.AddWithValue("@ZIP", contact.zip);


                cmd.ExecuteNonQuery();
            }

        }
    }

    public void DeleteContact(int id) 
    {
        var builder = BuildConnectionString();

        string connectionString = builder.ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString)) 
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand("dbo.DeleteContact", conn)) 
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<Contact> GetContacts() 
    {
        List<Contact> contacts = new List<Contact>();

        var builder = BuildConnectionString();


        using (SqlConnection conn = new SqlConnection(builder.ConnectionString)) 
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM CONTACTS WHERE EMAIL = '{this.currentEmail}'",conn)) 
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    var temp = new Contact
                    {

                        ID = Convert.ToInt32(reader["ID"].ToString()),
                        User = (string)reader["Email"].ToString(),
                        firstName = (string)reader["FirstName"].ToString(),
                        lastName = (string)reader["LastName"].ToString(),
                        address = (string)reader["Address"].ToString(),
                        city = (string)reader["City"].ToString(),
                        State = (string)reader["State"].ToString(),
                        zip = Convert.ToInt32(reader["Zip"].ToString())

                    };

                    contacts.Add(temp);
                }
            }
        }

        return contacts;
    }

    public Contact GetContact() 
    {
        return new Contact();
    }

}
