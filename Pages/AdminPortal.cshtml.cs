using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace UniversityProject.Pages {
    public class AdminPortalModel : PageModel {
        public class AdminRow {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Extra { get; set; }    // Phone for students, Specialization for teachers
            public string Location { get; set; } // Address for students, Dept ID for teachers
        }

        public List<AdminRow> StudentList = new();
        public List<AdminRow> TeacherList = new();
        public string Msg { get; set; }

        string connStr = @"Server=localhost\SQLEXPRESS; Database=uni_db; Integrated Security=True; TrustServerCertificate=True;";

        public void OnGet() { LoadData(); }

        private void LoadData() {
            StudentList.Clear(); TeacherList.Clear();
            try {
                using SqlConnection conn = new(connStr); conn.Open();
                // Load Students
                using (SqlCommand cmd = new("SELECT student_id, first_name + ' ' + last_name, phone, address FROM student", conn))
                using (SqlDataReader r = cmd.ExecuteReader()) {
                    while (r.Read()) StudentList.Add(new AdminRow { Id = r[0].ToString(), Name = r[1].ToString(), Extra = r[2].ToString(), Location = r[3].ToString() });
                }
                // Load Teachers
                using (SqlCommand cmd = new("SELECT teacher_id, first_name, specialization, department_id FROM teacher", conn))
                using (SqlDataReader r = cmd.ExecuteReader()) {
                    while (r.Read()) TeacherList.Add(new AdminRow { Id = r[0].ToString(), Name = r[1].ToString(), Extra = r[2].ToString(), Location = r[3].ToString() });
                }
            } catch (Exception ex) { Msg = "Error: " + ex.Message; }
        }

        public IActionResult OnPostAddStudent(string f, string l, string p, string a, int d) {
            try {
                using SqlConnection conn = new(connStr); conn.Open();
                using SqlCommand cmd = new("INSERT INTO student (first_name, last_name, phone, address, department_id) VALUES (@f,@l,@p,@a,@d)", conn);
                cmd.Parameters.AddWithValue("@f", f); cmd.Parameters.AddWithValue("@l", l);
                cmd.Parameters.AddWithValue("@p", p); cmd.Parameters.AddWithValue("@a", a);
                cmd.Parameters.AddWithValue("@d", d); cmd.ExecuteNonQuery();
            } catch (Exception ex) { Msg = "Add Error: " + ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostAddTeacher(string f, string s, DateTime h, int d) {
            try {
                using SqlConnection conn = new(connStr); conn.Open();
                using SqlCommand cmd = new("INSERT INTO teacher (first_name, specialization, hire_date, department_id) VALUES (@f,@s,@h,@d)", conn);
                cmd.Parameters.AddWithValue("@f", f); cmd.Parameters.AddWithValue("@s", s);
                cmd.Parameters.AddWithValue("@h", h); cmd.Parameters.AddWithValue("@d", d);
                cmd.ExecuteNonQuery();
            } catch (Exception ex) { Msg = "Teacher Add Error: " + ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id, string type) {
            try {
                using SqlConnection conn = new(connStr); conn.Open();
                string table = type == "student" ? "student" : "teacher";
                string pk = type == "student" ? "student_id" : "teacher_id";
                new SqlCommand($"DELETE FROM {table} WHERE {pk}={id}", conn).ExecuteNonQuery();
            } catch { Msg = "Delete failed (Record linked to other data)."; }
            return RedirectToPage();
        }
    }
}