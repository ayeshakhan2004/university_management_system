use Uni_db;

-- 1. security and login tables
-- holding permission levels
create table role (
    role_id int identity(1,1) primary key,
    role_name varchar(50) not null
);

-- user accounts
-- note: 'bit' is used for true/false in sql server
create table user_account (
    user_id int identity(1,1) primary key,
    username varchar(50) not null,
    password varchar(255) not null,
    is_active bit default 1,
    role_id int,
    foreign key (role_id) references role(role_id)
);

-- audit log for history
create table audit_log (
    log_id int identity(1,1) primary key,
    action_type varchar(20),
    old_value varchar(max),
    new_value varchar(max),
    timestamp datetime default getdate(),
    user_id int,
    foreign key (user_id) references user_account(user_id)
);

-- 2. main university entities
create table department (
    department_id int identity(1,1) primary key,
    department_name varchar(100) not null
);

create table student (
    student_id int identity(1,1) primary key,
    first_name varchar(50),
    last_name varchar(50),
    address varchar(255),
    phone varchar(20),
    department_id int,
    foreign key (department_id) references department(department_id)
);

create table teacher (
    teacher_id int identity(1,1) primary key,
    first_name varchar(50),
    specialization varchar(100),
    hire_date date,
    department_id int,
    foreign key (department_id) references department(department_id)
);

-- 3. scheduling tables
create table course (
    course_id int identity(1,1) primary key,
    course_name varchar(100),
    credits int
);

create table semester (
    semester_id int identity(1,1) primary key,
    semester_name varchar(50),
    start_date date,
    end_date date
);

-- actual class sections
create table class_section (
    section_id int identity(1,1) primary key,
    course_id int,
    teacher_id int,
    semester_id int,
    foreign key (course_id) references course(course_id),
    foreign key (teacher_id) references teacher(teacher_id),
    foreign key (semester_id) references semester(semester_id)
);

-- 4. grading
create table enrollment (
    enrollment_id int identity(1,1) primary key,
    grade varchar(5),
    attendance_status varchar(20),
    student_id int,
    section_id int,
    foreign key (student_id) references student(student_id),
    foreign key (section_id) references class_section(section_id)
);