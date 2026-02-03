using System.Data.SqlClient;

public class DatabaseConnection
{
    // The 'Address' to your database
    // @"" makes sure the backslash (\) is read correctly
    string connectionString = @"Server=localhost\SQLEXPRESS; Database=Uni_db; Integrated Security=True;";

    public void TestConnection()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection Successful! Linked to Uni_db.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to database: " + ex.Message);
            }
        }
    }
}