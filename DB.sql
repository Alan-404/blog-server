CREATE TABLE USER_SYSTEM(
	ID VARCHAR(11) PRIMARY KEY,
	EMAIL VARCHAR(51),
	FIRST_NAME NVARCHAR(11),
	LAST_NAME NVARCHAR(11),
	GENDER VARCHAR(11),
	PHONE_NUMBER VARCHAR(15),
	B_DATE DATE,
	CREATED_AT DATETIME,
	MODIFIED_AT DATETIME
)

CREATE TABLE SOCIAL_NETWORK(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	NAME VARCHAR(21)
)

CREATE TABLE USER_SOCIAL_NETWORK(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	SOCIAL_NETWORK_ID INT REFERENCES SOCIAL_NETWORK(ID),
	LINK TEXT
)

ALTER TABLE USER_SOCIAL_NETWORK ADD LINK TEXT

CREATE TABLE ACCOUNT(
	ID VARCHAR(11) PRIMARY KEY,
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	PASSWORD TEXT,
	ROLE VARCHAR(11),
	MODIFIED_AT DATETIME
)

CREATE TABLE BLOG(
	ID VARCHAR(11) PRIMARY KEY,
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	TITLE TEXT,
	INTRODUCTION TEXT,
	CONTENT TEXT,
	NUM_VIEWS INT,
	CREATED_AT DATETIME,
	MODIFIED_AT DATETIME
)

CREATE TABLE CATEGORY(
	ID VARCHAR(11) PRIMARY KEY,
	NAME VARCHAR(21)
)

CREATE TABLE BLOG_CATEGORY(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	BLOG_ID VARCHAR(11) REFERENCES BLOG(ID),
	CATEGORY_ID VARCHAR(11) REFERENCES CATEGORY(ID)
)

CREATE TABLE COMMENT(
	ID VARCHAR(21) PRIMARY KEY,
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	BLOG_ID VARCHAR(11) REFERENCES BLOG(ID),
	REPLY VARCHAR(21) REFERENCES COMMENT(ID),
	CONTENT TEXT,
	CREATED_AT DATETIME,
	MODIFIED_AT DATETIME
)

CREATE TABLE BLOG_VIEW(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	BLOG_ID VARCHAR(11) REFERENCES BLOG(ID),
	NUM INT
)

CREATE TABLE COMMENT_LIKE(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	COMMENT_ID VARCHAR(21) REFERENCES COMMENT(ID),
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID)
)

CREATE TABLE ROOM(
	ID VARCHAR(21) PRIMARY KEY,
	USER_1 VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	USER_2 VARCHAR(11) REFERENCES USER_SYSTEM(ID)
)

CREATE TABLE ROOM_MESSAGE(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	USER_ID VARCHAR(11) REFERENCES USER_SYSTEM(ID),
	ROOM_ID VARCHAR(21) REFERENCES ROOM(ID),
	CONTENT TEXT,
	CREATED_AT DATETIME,
	STATUS BIT
)

select * from blog_view


drop table media
drop table comment

ALTER TABLE USER_SYSTEM ADD B_DATE DATETIME
alter table user_system drop column b_date
delete from blog

SELECT * FROM USER_SYSTEM
SELECT * FROM ACCOUNT
SELECT * FROM BLOG
SELECT * FROM BLOG_CATEGORY
SELECT * FROM COMMENT
SELECT * FROM BLOG_VIEW
SELECT * FROM COMMENT_LIKE
SELECT * FROM CATEGORY
SELECT * FROM ROOM
SELECT * FROM USER_SOCIAL_NETWORK
