drop table if exists crud_test_db.person;

create table crud_test_db.person(
id BIGINT not null AUTO_INCREMENT,
first_name VARCHAR(100) not null,
last_name VARCHAR(100) not null,
full_name VARCHAR(200) GENERATED ALWAYS AS (CONCAT(first_name, ' ', last_name)),
gender CHAR(1) not null default 'E',
ssno varchar(20) not null COMMENT 'Social Security Number',
birth_date DATE null,
birth_date_int INT GENERATED ALWAYS AS (cast(DATE_FORMAT(birth_date, '%Y%m%d') as unsigned)),
image blob,
is_deleted boolean not null,
is_active boolean not null default true,
created_on DATETIME not null default NOW(),
created_on_tick BIGINT  GENERATED ALWAYS AS (cast(DATE_FORMAT(created_on, '%Y%m%d%H%i%s') as unsigned)), 
created_by BIGINT null,
updated_on DATETIME null,
updated_on_tick BIGINT  GENERATED ALWAYS AS (cast(DATE_FORMAT(updated_on, '%Y%m%d%H%i%s') as unsigned)), 
updated_by BIGINT null,
PRIMARY KEY (id) );