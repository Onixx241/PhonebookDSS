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

    public string sampleString { get; set; }

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

    //example to run stored procedure from sqlConn
    public void runSqlQuery()
    {
        var builder = BuildConnectionString();

        string connectionString = builder.ConnectionString;

        //string command = "SELECT * FROM dbo.test_table";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {

            conn.Open();

            using (SqlCommand cmd = new SqlCommand("dbo.USP_GET_REPORT_FREQUENT_LANDLORD", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@REFERRAL_DATE_RANGE", 120);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.sampleString = $"SQL result: {reader[0].ToString()}, {reader[1].ToString()}, {reader[2].ToString()}, {reader[3].ToString()}, {reader[4].ToString()}";
                    reader.NextResult();
                }

            }
        }
    }

}