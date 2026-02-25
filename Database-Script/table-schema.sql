

-- create table 1 side
create table tbl_category(
	category_id int primary key identity(1,1),
	[name] nvarchar(255) NOT NULL,
	remark nvarchar(300) null
);
create table tbl_brand(
	brand_id int primary key identity(1,1),
	[name] nvarchar(255) NOT NULL,
	remark nvarchar(300) null
);
create table tbl_uom(
	uom_id int primary key identity(1,1),
	[name] nvarchar(255) NOT NULL,
	remark nvarchar(300) null
);
create table tbl_customer(
	customer_id int primary key identity(1,1),
	[name] nvarchar(255) NOT NULL,
	phone nvarchar(50) NULL,
	balance decimal(18,6) NULL,
	[address] nvarchar(355) NULL
);

-- create table Many Side
create table tbl_product(
	product_id int primary key identity(1,1),
	[name] nvarchar(255) NOT NULL,
	code nvarchar(50) NULL,
	barcode nvarchar(100) NULL,
	stock_qty decimal(18,2) NOT NULL,
	cost_price decimal(18,6) NULL,
	retail_price decimal(18,6) NULL,
	whole_price decimal(18,6) NULL,
	-- setup foreign key
	category_id int null foreign key references tbl_category(category_id),
	brand_id int null foreign key references tbl_brand(brand_id),
	uom_id int null foreign key references tbl_uom(uom_id)
);
create table tbl_invoice(
	invoice_id int primary key identity(1,1),
	invoice_no varchar(100) NOT NULL,
	invoice_dt datetime default GETDATE(),
	total_amount decimal(18,6) NOT NULL,
	discount_amount decimal(18,6) NULL,
	paid_amount decimal(18,6) NULL,
	-- setup foreign key
	customer_id int not null foreign key references tbl_customer(customer_id)
);
create table tbl_invoice_detail(
	id int primary key identity(1,1),
	invocie_id int not null foreign key references tbl_invoice(invoice_id),
	product_id int not null foreign key references tbl_product(product_id),
	sale_qty int not null,
	unit_price decimal(18,6) not null
);