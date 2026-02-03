use uni_db;

-- 1. adding roles (admin, dba, etc)
insert into role (role_name) values ('Admin');
insert into role (role_name) values ('DBA');
insert into role (role_name) values ('Teacher');
insert into role (role_name) values ('Student');

-- 2. adding user accounts
-- note: linking these to the role_ids created above (1=admin, 2=dba, etc)
insert into user_account (username, password, role_id) 
values ('bilal.admin', 'admin123', 1);

insert into user_account (username, password, role_id) 
values ('saad.dba', 'dbpass99', 2);

insert into user_account (username, password, role_id) 
values ('ayesha.khan', 'teach123', 3);

insert into user_account (username, password, role_id) 
values ('ali.raza', 'stud123', 4);

-- 3. create some audit logs (history)
-- pretending bilal (user 1) changed some records
insert into audit_log (action_type, old_value, new_value, user_id)
values ('UPDATE', 'Grade: B', 'Grade: A', 1);

insert into audit_log (action_type, old_value, new_value, user_id)
values ('DELETE', 'Student: Zafar', 'NULL', 1);

-- 4. adding departments
insert into department (department_name) values ('Computer Science');
insert into department (department_name) values ('Electrical Engineering');
insert into department (department_name) values ('Business Administration');
insert into department (department_name) values ('Mathematics');

-- 5. adding teachers (pakistani names)
-- linking to departments: 1=CS, 2=EE, 3=BBA
insert into teacher (first_name, specialization, hire_date, department_id)
values ('Dr. Omar', 'Artificial Intelligence', '2015-08-15', 1);

insert into teacher (first_name, specialization, hire_date, department_id)
values ('Ms. Fatima', 'Digital Logic Design', '2018-02-01', 2);

insert into teacher (first_name, specialization, hire_date, department_id)
values ('Sir Zain', 'Marketing', '2020-01-10', 3);

insert into teacher (first_name, specialization, hire_date, department_id)
values ('Dr. Hina', 'Calculus', '2012-09-05', 4);

-- 6. adding students
insert into student (first_name, last_name, address, phone, department_id)
values ('Ahmed', 'Khan', 'House 12, Block 5, Gulshan, Karachi', '0300-1234567', 1);

insert into student (first_name, last_name, address, phone, department_id)
values ('Zainab', 'Malik', 'Flat 4B, DHA Phase 6, Lahore', '0321-9876543', 1);

insert into student (first_name, last_name, address, phone, department_id)
values ('Hamza', 'Sheikh', 'Sector G-10, Islamabad', '0333-5555555', 2);

insert into student (first_name, last_name, address, phone, department_id)
values ('Sara', 'Ahmed', 'Model Town, Lahore', '0345-1112223', 3);

insert into student (first_name, last_name, address, phone, department_id)
values ('Bilal', 'Siddiqui', 'North Nazimabad, Karachi', '0312-0000000', 1);

-- 7. adding semesters
insert into semester (semester_name, start_date, end_date)
values ('Fall 2023', '2023-09-01', '2023-12-30');

insert into semester (semester_name, start_date, end_date)
values ('Spring 2024', '2024-01-15', '2024-05-20');

-- 8. adding courses
insert into course (course_name, credits) values ('Intro to Programming', 4);
insert into course (course_name, credits) values ('Pakistan Studies', 2);
insert into course (course_name, credits) values ('Circuit Analysis', 3);
insert into course (course_name, credits) values ('Principles of Marketing', 3);

-- 9. scheduling classes (class_sections)
-- linking course 1 (programming) with teacher 1 (omar) in semester 1
insert into class_section (course_id, teacher_id, semester_id) 
values (1, 1, 1); 

-- linking course 3 (circuits) with teacher 2 (fatima) in semester 1
insert into class_section (course_id, teacher_id, semester_id) 
values (3, 2, 1);

-- linking course 4 (marketing) with teacher 3 (zain) in semester 2
insert into class_section (course_id, teacher_id, semester_id) 
values (4, 3, 2);

-- 10. enrolling students and giving grades
-- ahmed (student 1) takes programming (section 1)
insert into enrollment (grade, attendance_status, student_id, section_id)
values ('A', 'Present', 1, 1);

-- zainab (student 2) takes programming (section 1)
insert into enrollment (grade, attendance_status, student_id, section_id)
values ('B+', 'Present', 2, 1);

-- hamza (student 3) takes circuits (section 2)
insert into enrollment (grade, attendance_status, student_id, section_id)
values ('A-', 'Present', 3, 2);

-- sara (student 4) takes marketing (section 3)
insert into enrollment (grade, attendance_status, student_id, section_id)
values ('B', 'Absent', 4, 3);