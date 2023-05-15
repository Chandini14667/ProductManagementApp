create database ProductManagement
use ProductManagement

Create Table Products
(
ProductId int identity  primary key,
ProductName varchar(50),
ProductBrand varchar(100),
Quantity int,
Price int
)

select * from Products