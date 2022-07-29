create table categories(
	categoryId int auto_increment,
    category varchar(200),
    primary key(categoryId)
);
create table categoryRequests(
	categoryRequestId int auto_increment,
    categoryRequest varchar(200),
    primary key(categoryRequestId)
);
create table blogs(
	blogId int auto_increment primary key,
    title varchar(255),
    blog text,
    publishDate date,
    visits int,
    categoryId int,
    foreign key(categoryId) references categories(categoryId)
);
create table comments(
	commentId int auto_increment,
    comment text,
    blogId int,
    foreign key(blogId) references blogs(blogId),
    primary key(commentId,blogId)
);
create table images(
	imageId int auto_increment,
    imagePath varchar(255),
    blogId int,
    foreign key(blogId) references blogs(blogId),
    primary key(imageId, imagePath,blogId)
);
insert into categories values (null, "programming");
insert into categories values (null, "cyber security");
insert into categories values (null, "problem solving");
insert into categories values (null, "computer science");
insert into categories values (null, "artificial inteligence");
drop table categories cascade;
drop table blogs cascade;
drop table categoryRequests cascade;
drop table comments cascade;
drop table images cascade;