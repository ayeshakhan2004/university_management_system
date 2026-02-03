using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace UniversityProject.Pages
{
    public class TeacherPortalModel : PageModel
    {
        // 1. DATA PROPERTIES
        public string TeacherName { get; set; }
        public string Specialization { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public class StudentGradeInfo {
            public int EnrollmentId { get; set; }
            public string Name { get; set; }
            public string Course { get; set; }
            public string Grade { get; set; }
        }

        public class ScheduleInfo {
            public string Course { get; set; }
            public string Semester { get; set; }
        }

        public List<StudentGradeInfo> MyStudents = new List<StudentGradeInfo>();
        public List<ScheduleInfo> MySchedule = new List<ScheduleInfo>();

        string connString = @"Server=localhost\SQLEXPRESS; Database=Uni_db; Integrated Security=True; TrustServerCertificate=True;";

        // 2. ON GET (Matches Student Portal Logic)
        public void OnGet(string teachername)
        {
            // Default to 'ayesha' if no name is provided so the page is never empty
            if (string.IsNullOrEmpty(teachername)) teachername = "ayesha";

            try {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // A. FIND TEACHER ID BY NAME
                    // We split 'ayesha.khan' to get 'ayesha' and match it against first_name
                    string firstNameMatch = teachername.Split('.')[0];
                    int currentTeacherId = 0;

                    string profileSql = "SELECT teacher_id, first_name, specialization FROM teacher WHERE first_name LIKE @u";

                    using (SqlCommand cmd = new SqlCommand(profileSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", firstNameMatch + "%");
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                currentTeacherId = Convert.ToInt32(r["teacher_id"]);
                                TeacherName = r["first_name"].ToString();
                                Specialization = r["specialization"].ToString();
                            }
                        }
                    }

                    // B. IF TEACHER FOUND, GET THEIR DATA
                    if (currentTeacherId > 0)
                    {
                        // Get Schedule
                        string sqlSchedule = "SELECT c.course_name, s.semester_name FROM class_section cs JOIN course c ON cs.course_id=c.course_id JOIN semester s ON cs.semester_id=s.semester_id WHERE cs.teacher_id=@id";
                        using (SqlCommand cmd = new SqlCommand(sqlSchedule, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", currentTeacherId);
                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    MySchedule.Add(new ScheduleInfo { Course = r.GetString(0), Semester = r.GetString(1) });
                                }
                            }
                        }

                        // Get Students
                        string sqlStudents = "SELECT e.enrollment_id, st.first_name + ' ' + st.last_name, c.course_name, e.grade FROM enrollment e JOIN class_section cs ON e.section_id=cs.section_id JOIN student st ON e.student_id=st.student_id JOIN course c ON cs.course_id=c.course_id WHERE cs.teacher_id=@id";
                        using (SqlCommand cmd = new SqlCommand(sqlStudents, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", currentTeacherId);
                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    MyStudents.Add(new StudentGradeInfo
                                    {
                                        EnrollmentId = r.GetInt32(0),
                                        Name = r.GetString(1),
                                        Course = r.GetString(2),
                                        Grade = r.IsDBNull(3) ? "" : r.GetString(3)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                ErrorMessage = "Error: " + ex.Message; 
            }
        }

        // 3. UPDATE GRADE FUNCTION
        public IActionResult OnPostUpdateGrade(int enrollmentId, string newGrade)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE enrollment SET grade=@g WHERE enrollment_id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@g", newGrade);
                        cmd.Parameters.AddWithValue("@id", enrollmentId);
                        cmd.ExecuteNonQuery();
                    }
                }
                SuccessMessage = "Grade Updated Successfully!";
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }

            // Reload page (re-runs OnGet automatically)
            return RedirectToPage(); 
        }
    }
}