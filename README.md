# 🏛️ University Management System (UMS)

A full-stack web application designed to manage university operations. It features a role-based architecture allowing Admins, Teachers, and Students to interact with a centralized SQL Server database.

## 🚀 Key Features

### 👨‍💼 Admin Portal
* **Manage Users:** Add new teachers and delete student/teacher records.
* **Audit Logs:** Track system changes (who changed what and when).
* **Dashboard:** Overview of active students and faculty.

### 👩‍🏫 Teacher Portal
* **Course Management:** View assigned courses and schedules.
* **Grading System:** Update student grades (A-F) directly from the interface.
* **Student Rosters:** View list of enrolled students per course.

### 🎓 Student Portal
* **Profile Management:** View personal details and enrollment status.
* **Course Enrollment:** Browse available courses and request enrollment.
* **Gradebook:** View grades and status for current semester.

---

## 🛠️ Tech Stack

* **Frontend:** ASP.NET Core Razor Pages, Bootstrap 5, HTML/CSS.
* **Backend:** C# (.NET Core), ADO.NET.
* **Database:** Microsoft SQL Server (T-SQL).
* **Tools:** Visual Studio 2022.

---

## ⚙️ How to Run Locally

1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/ayeshakhan2004/University-Management-System.git](https://github.com/ayeshakhan2004/University-Management-System.git)
    ```

2.  **Set up the Database**
    * Open **SQL Server Management Studio (SSMS)**.
    * Open the folder `DatabaseScripts` in this project.
    * Run the scripts in this order:
        1.  `uni db.sql` (Creates tables)
        2.  `data in db.sql` (Inserts dummy data)

3.  **Configure Connection**
    * Open `appsettings.json` or check the `.cs` files (e.g., `Index.cshtml.cs`).
    * Ensure the `server` matches your local SQL instance (usually `localhost\SQLEXPRESS` or `.` ).

4.  **Run the Application**
    * Open the project in **Visual Studio**.
    * Press **F5** or the Green Play button.

---

## 🔐 Login Credentials (Demo Accounts)

You can use these pre-configured accounts to test the different roles:

| Role | Username | Password |
| :--- | :--- | :--- |
| **Admin** | `bilal.admin` | `admin123` |
| **Teacher** | `ayesha.khan` | `teach123` |
| **Student** | `ali.raza` | `stud123` |

---

## 📸 Database Schema
The system uses a relational database with the following core entities:
* `User_Account` & `Role` (Security)
* `Student` & `Teacher` (Profiles)
* `Course`, `Semester`, `Class_Section` (Scheduling)
* `Enrollment` (Links students to sections + grades)
