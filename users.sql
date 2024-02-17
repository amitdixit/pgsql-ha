CREATE TABLE users (
 id serial primary key,
 customerid varchar (50),
 modifieddate timestamp,
 title varchar (50),
 firstname varchar (50),
 lastname varchar (50),
 middlename varchar (50),
 namestyle bool,
 suffix varchar (50),
 companyname varchar (50),
 salesperson varchar (50),
 emailaddress varchar (50),
 phone varchar (50)
 );