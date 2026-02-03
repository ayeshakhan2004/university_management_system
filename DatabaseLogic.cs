using System.Data.SqlClient;

public class DatabaseLogic {
    // This is your SQL Server connection from your screenshot
    string connString = "Server=localhost\\SQLEXPRESS; Database=Uni_db; Integrated Security=True; TrustServerCertificate=True;";

    public void GetStudentData(string user, string pass) {
        using (SqlConnection conn = new SqlConnection(connString)) {
            conn.Open();
            // This query checks the role just like your assignment requires
            string sql = "select role_id from user_account where username=@u and password=@p";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", pass);
            
            var roleId = cmd.ExecuteScalar();
            
            if (roleId != null && (int)roleId == 1) {
                // Only role_id 1 (Admin) gets to see this message
                Console.WriteLine("Access Granted: Welcome Admin");
            }
        }
    }
}