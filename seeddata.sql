insert into location (name) values ('Reston, VA');
insert into location (name) values ('Dallas, TX');
insert into location (name) values ('Tampa, FL');
insert into location (name) values ('New York, NY');
insert into location (name) values ('Orlando, FL');
insert into location (name) values ('Morgantown, WV');

insert into customer (first_name, last_name) values ('Antonio','Mendez');
insert into shoppingcart (customer_id) values (1);
insert into customer (first_name, last_name) values ('Darko','Mendez');
insert into shoppingcart (customer_id) values (2);
insert into customer (first_name, last_name) values ('Gavin','Mendez');
insert into shoppingcart (customer_id) values (3);
insert into customer (first_name, last_name) values ('Kayla','Mendez'); 
insert into shoppingcart (customer_id) values (4);

insert into genre(name) values ('Psychology')
insert into genre(name) values ('Science Fiction')
insert into genre(name) values ('Adult Fiction')
insert into genre(name) values ('Fantasy')
insert into genre(name) values ('Crime')
insert into genre(name) values ('Mystery')
insert into genre(name) values ('Horror')
insert into genre(name) values ('Learning Resource')
insert into genre(name) values ('Drama')
insert into genre(name) values ('Ethics')

insert into book (isbn, name, price, author_first_name, author_last_name, genre_id, image_link) values 
('978-0525948926', 'Atlas Shrugged', 29.99, 'Ayn', 'Rand', 10, 'https://upload.wikimedia.org/wikipedia/commons/3/3e/Atlas_Shrugged_%281957_1st_ed%29_-_Ayn_Rand.jpg');
insert into book (isbn, name, price, author_first_name, author_last_name, genre_id, image_link) values 
('978-0452286757', 'The Fountainhead', 27.99, 'Ayn', 'Rand', 10, 'https://images.penguinrandomhouse.com/cover/9780452286757');
insert into book (isbn, name, price, author_first_name, author_last_name, genre_id, image_link) values 
('978-1640320437', 'Anthem', 9.99, 'Ayn', 'Rand', 10, 'https://prodimage.images-bn.com/pimages/9780451191137_p0_v6_s1200x630.jpg');
insert into book (isbn, name, price, author_first_name, author_last_name, genre_id, image_link) values 
('978-0553103540', 'A Game of Thrones (Song of Ice and Fire)', 26.53, 'George R.R.', 'Martin', 4, 'https://images-na.ssl-images-amazon.com/images/I/91dSMhdIzTL.jpg');
insert into book (isbn, name, price, author_first_name, author_last_name, genre_id, image_link) values 
('978-1617294563', 'Entity Framework Core in Action', 45.28, 'Jon P', 'Smith', 8, 'https://images-na.ssl-images-amazon.com/images/I/71yu1E6ZLDL.jpg');

-- Reston Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (1, '978-0525948926',0)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-0452286757',0)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-1640320437',15)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-0553103540',3)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-1617294563',999)

-- Dallas Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (2, '978-0525948926', 873)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-0452286757', 0)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-1640320437', 48)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-0553103540', 16)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-1617294563', 999)

-- Tampa Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (3, '978-0525948926', 78)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-0452286757', 11)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-1640320437', 65)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-0553103540', 100)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-1617294563', 0)

-- New York Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (4, '978-0525948926', 95)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-0452286757', 22)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-1640320437', 123)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-0553103540', 999)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-1617294563', 10)

-- Orlando Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (5, '978-0525948926', 78)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-0452286757', 22)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-1640320437', 136)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-0553103540', 41)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-1617294563', 52)

-- Morgantown Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (6, '978-0525948926', 78)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-0452286757', 0)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-1640320437', 0)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-0553103540', 0)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-1617294563', 665)

insert into orders (customer_id, location_id) values (1,1);
insert into orders (customer_id, location_id) values (2,1);

insert into orderline(order_id, book_isbn, quantity, total) values (1, '978-0525948926',1, 29.99)
insert into orderline(order_id, book_isbn, quantity, total) values (1, '978-0452286757',1, 27.99)
insert into orderline(order_id, book_isbn, quantity, total) values (1, '978-1640320437',1, 9.99)


insert into orderline(order_id, book_isbn, quantity, total) values (2, '978-0553103540',1, 9.99)
insert into orderline(order_id, book_isbn, quantity, total) values (2, '978-1617294563',23, 45.28)