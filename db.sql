drop table if exists orderline;
drop table if exists orders;
drop table if exists inventory;
drop table if exists book;
drop table if exists customer;
drop table if exists location;

create table location(
	id int primary key identity,
	name nvarchar(50) not null
)

create table customer(
	id int primary key identity,
	first_name nvarchar(50) not null,
	last_name nvarchar(50) not null,
	location_id int foreign key  references location(id)
)

ALTER TABLE customer
ADD CONSTRAINT df_location
DEFAULT 1 FOR location_id;

create table book(
	isbn nvarchar(15) primary key,
	name nvarchar(50) not null,
	price decimal(19,4) not null,
	author_first_name nvarchar(50),
	author_last_name nvarchar(50)
)

create table inventory(
	location_id int foreign key references location(id),
	book_isbn nvarchar(15) foreign key references book(isbn),
	quantity int,
	primary key (location_id, book_isbn)
)

create table orders(
	id int primary key identity,
	customer_id int not null foreign key references customer(id),
	location_id int not null foreign key references location(id),
	order_date datetime default current_timestamp
)

create table orderline(
	id int primary key identity,
	order_id int not null foreign key references orders(id),
	book_isbn nvarchar(15) not null foreign key references book(isbn),
	quantity int not null,
	total decimal(19,4) not null

)

create table shoppingcart(
	cart_id int primary key identity,
	customer_id int not null foreign key references customer(id),
	create_data datetime default current_timestamp
)

create table cartitem(
	item_id int primary key identity,
	book_isbn nvarchar(15) foreign key references book(isbn),
	quantity int not null,
	data_added datetime default current_timestamp,
	shoppingcart_id int foreign key references shoppingcart(cart_id)
)