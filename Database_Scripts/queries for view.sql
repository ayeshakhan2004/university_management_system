use uni_db;

-- 1. view all students
-- this shows every column in the student table
select * from student;

-- 2. view all teachers
select * from teacher;

-- 3. view just the names and phone numbers of students
-- useful if you don't want to see the ids or addresses
select first_name, last_name, phone 
from student;

-- 4. find students in a specific department
-- this only shows students in department 1 (computer science)
select * from student 
where department_id = 1;


-- 5. see who is logging into the system
-- shows usernames and their role type
select * from user_account;

-- 6. view the audit log (history of changes)
-- check who deleted or updated data
select * from audit_log;

-- 7. (advanced) view students with their grades
-- this joins the tables so you see names instead of just ID numbers
select 
    student.first_name, 
    student.last_name, 
    enrollment.grade, 
    enrollment.attendance_status
from student
join enrollment on student.student_id = enrollment.student_id;

-- this query brings everything together in one view
select 
    s.first_name as student_name,
    s.last_name as student_surname,
    d.department_name as department,
    c.course_name as subject,
    t.first_name as teacher_name,
    e.grade as final_grade
from student s
join department d on s.department_id = d.department_id
join enrollment e on s.student_id = e.student_id
join class_section cs on e.section_id = cs.section_id
join course c on cs.course_id = c.course_id
join teacher t on cs.teacher_id = t.teacher_id;

-- view the history of who did what
select 
    u.username, 
    u.is_active, 
    a.action_type, 
    a.timestamp 
from user_account u
join audit_log a on u.user_id = a.user_id;