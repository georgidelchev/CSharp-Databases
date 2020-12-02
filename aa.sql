-- old filtering
select * from customers where address = 'Sofia' or address = 'New-York';

-- new filtering
select * from customers where address in ('Burgas', 'Sofia');

select * from customers where IDCustomers between 1 and 3;

select * from customers where Email like '%.com';

select * from customers where Email like '%/_Car%' escape '/';

-- problems
-- 1
select * from customers where address not in ('Burgas', 'Sofia');

-- 2
select sum(Total) `sum` from orders where IDOrders between 1 and 3;

-- 3
-- (1) SELECT * FROM Customers WHERE Email LIKE ‘%/_%’ ESCAPE ‘/’;
-- извежда всички мейли, които съдържат _ посредата

-- (2) SELECT * FROM Customers WHERE Email LIKE ‘/_%’ ESCAPE ‘/’;
-- извежда всички мейли, които започват с _

-- (3) SELECT * FROM Customers WHERE Email LIKE ‘%/_’ ESCAPE ‘/’;
-- извежда всички мейли, които завършват с _

-- (4) SELECT * FROM Customers WHERE Email LIKE ‘%/_ _’ ESCAPE ‘/’;
-- извежда всички мейли, които завършват с _ и имат един символ накрая

-- (5) SELECT * FROM Customers WHERE Email LIKE ‘_/_%’ ESCAPE ‘/’;
-- извежда всички мейли, които имат 1 символ отпред, съдържат _ посредата

-- (6) SELECT * FROM Customers WHERE Email LIKE ‘%/%’ ESCAPE ‘/’;
-- извежда всички мейли, които завършват с %

-- (7) SELECT * FROM Customers WHERE Email LIKE ‘/%%’ ESCAPE ‘/’;
-- извежда всички мейли, които започват с %

-- 4
select * from customers where `Name` like 'D%';

-- 5
select * from customers where Email like '%.com';

-- 6
select * from customers where Name like '%i_';

-- 7
update customers
set Email = 'val_bg@example.net'
where `Name` = 'Valeria';

update customers
set Email = 'rusv_bg@example.net'
where `Name` = 'Rusv';

select * from customers where Email not like '%/_bg%' escape '/';