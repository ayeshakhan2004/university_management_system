using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace UniversityProject.Pages {
    public class StudentPortalModel : PageModel {
        public string StudentName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public List<string[]> MyCourses = new List<string[]>();

        string connString = @"Server=localhost\SQLEXPRESS; Database=uni_db; Integrated Security=True; TrustServerCertificate=True;";

        public void OnGet(string username) {
            // Default to ali.raza if nothing is passed so the page isn't empty during testing
            if (string.IsNullOrEmpty(username)) username = "ali.raza"; 

            using (SqlConnection conn = new SqlConnection(connString)) {
                conn.Open();

                // 1. GET STUDENT PROFILE BASED ON USERNAME
                // We split 'ali.raza' to get 'ali' and match it against first_name
                string firstNameMatch = username.Split('.')[0];
                int currentStudentId = 0;

                string infoSql = @"SELECT s.student_id, s.first_name, s.last_name, s.address, s.phone, d.department_name 
                                   FROM student s
                                   JOIN department d ON s.department_id = d.department_id
                                   WHERE s.first_name LIKE @u";

                using (SqlCommand cmd = new SqlCommand(infoSql, conn)) {
                    cmd.Parameters.AddWithValue("@u", firstNameMatch + "%");
                    using (SqlDataReader r = cmd.ExecuteReader()) {
                        if (r.Read()) {
                            currentStudentId = Convert.ToInt32(r["student_id"]);
                            StudentName = r["first_name"].ToString() + " " + r["last_name"].ToString();
                            Address = r["address"].ToString();
                            Phone = r["phone"].ToString();
                            Department = r["department_name"].ToString();
                        }
                    }
                }

                // 2. GET COURSES FOR THIS SPECIFIC STUDENT ID
                if (currentStudentId > 0) {
                    string gradeSql = @"SELECT c.course_name, t.first_name as teacher, e.grade 
                                        FROM enrollment e
                                        JOIN class_section cs ON e.section_id = cs.section_id
                                        JOIN course c ON cs.course_id = c.course_id
                                        JOIN teacher t ON cs.teacher_id = t.teacher_id
                                        WHERE e.student_id = @sid";

                    using (SqlCommand cmd2 = new SqlCommand(gradeSql, conn)) {
                        cmd2.Parameters.AddWithValue("@sid", currentStudentId);
                        using (SqlDataReader r2 = cmd2.ExecuteReader()) {
                            while (r2.Read()) {
                                MyCourses.Add(new string[] { 
                                    r2["course_name"].ToString(), 
                                    "Dr. " + r2["teacher"].ToString(), 
                                    r2["grade"].ToString() 
                                });
                            }
                        }
                    }
                }
            }
        }
    }
}