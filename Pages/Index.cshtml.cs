using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;

namespace UniversityProject.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string ConnectionString = @"Server=localhost\SQLEXPRESS; Database=Uni_db; Integrated Security=True; TrustServerCertificate=True;";

        public void OnGet()
        {
            // Initial page load
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    // We fetch role_id to determine where to send the user
                    string sql = "SELECT role_id FROM user_account WHERE username=@u AND password=@p AND is_active = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", Username);
                        cmd.Parameters.AddWithValue("@p", Password);
                        
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int roleId = Convert.ToInt32(result);

                            // ROLE REDIRECTION LOGIC
                            if (roleId == 1) // Admin
                            {
                                return RedirectToPage("/AdminPortal");
                            }
                            else if (roleId == 2) // Teacher
                            {
                                return RedirectToPage("/TeacherPortal", new { username = Username });
                            }
                            else if (roleId == 3) // Student
                            {
                                return RedirectToPage("/StudentPortal", new { username = Username });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = "Database Error: " + ex.Message;
                    return Page();
                }
            }

            ViewData["Message"] = "Invalid Username or Password";
            return Page();
        }
    }
}