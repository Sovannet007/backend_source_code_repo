
-- read data or select data
select * from tbl_category
select * from tbl_brand
select * from tbl_uom

-- join table for readable data
select * from tbl_category
select * from tbl_brand
select * from tbl_product

select 
 p.name as product_name,
 c.name as category_name,
 b.name as brand_name,
 u.name as uom_name
from tbl_product as p
left join tbl_category as c on p.category_id = c.category_id
left join tbl_brand as b on p.brand_id = b.brand_id
left join tbl_uom as u on p.uom_id = u.uom_id



-- insert data
insert into tbl_product(
	[name]
	,code
	,barcode
	,stock_qty
	,cost_price
	,retail_price
	,whole_price
	,category_id
	,brand_id
	,uom_id
)
values
(
	'Dell Optiplix 570',
	'D0570',
	NULL,
	10,
	899.99,
	999.99,
	959.99,
	3,
	4,
	1
);

-- update record
update tbl_customer
set [name] = N'សុខ'
where customer_id = 2


-- delete wrong record
delete from tbl_category where category_id >=4