use uni_db;

-- variables to hold the login info
-- change the password to 'wrong' to see it block you
declare @admin_user varchar(50) = 'bilal.admin';
declare @admin_pass varchar(50) = 'admin123'; 

-- start of the security check
if exists (select * from user_account where username = @admin_user and password = @admin_pass and role_id = 1)
begin
    -- this part runs if the password is correct
    print '>> access granted: credentials verified.';
    
    -- changing the student address
    update student 
    set address = 'new address, verified by admin' 
    where student_id = 1;

    print '>> success: record has been updated.';
end
else
begin
    -- this part runs if you type the wrong password
    print '>> access denied: incorrect password or not an admin.';
end


use uni_db;

-- variables for teacher login
declare @teach_user varchar(50) = 'ayesha.khan';
declare @teach_pass varchar(50) = 'teach123'; 

-- check if user is a teacher (role_id 3)
if exists (select * from user_account where username = @teach_user and password = @teach_pass and role_id = 3)
begin
    print '>> teacher access granted.';
    
    -- teacher updating a student's grade
    update enrollment 
    set grade = 'a+' 
    where student_id = 2 and section_id = 1;

    print '>> success: grade updated by teacher.';
end
else
begin
    print '>> access denied: you do not have teacher permissions.';
end

use uni_db;

-- variables for student login
declare @std_user varchar(50) = 'ali.raza';
declare @std_pass varchar(50) = 'stud123'; 

-- check if user is a student (role_id 4)
if exists (select * from user_account where username = @std_user and password = @std_pass and role_id = 4)
begin
    print '>> student access granted. loading profile...';
    
    -- students can only see their own info, not others
    select * from student where first_name = 'ahmed'; -- pretending ahmed is the logged in user
end
else
begin
    print '>> login failed: incorrect student credentials.';
end

use uni_db;

-- variables for dba login
declare @dba_user varchar(50) = 'saad.dba';
declare @dba_pass varchar(50) = 'dbpass99'; 

-- check if user is a dba (role_id 2)
if exists (select * from user_account where username = @dba_user and password = @dba_pass and role_id = 2)
begin
    print '>> dba access granted. opening system logs...';
    
    -- dba checking the history of changes
    select * from audit_log;
end
else
begin
    print '>> access denied: dba credentials required.';
end