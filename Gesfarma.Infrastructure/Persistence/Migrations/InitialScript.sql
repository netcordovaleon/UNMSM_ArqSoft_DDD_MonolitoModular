CREATE TABLE clients
(
	id VARCHAR(150) PRIMARY KEY,
	first_name VARCHAR(50),
	last_name VARCHAR(50),
	is_active BIT,
	created_at_utc DATETIME,
	updated_at_utc DATETIME
)
GO


CREATE TABLE product
(
	id VARCHAR(150) PRIMARY KEY,
	product_name VARCHAR(50),
	price DECIMAL(19,2),
	stock INT,
	is_active BIT,
	created_at_utc DATETIME,
	updated_at_utc DATETIME
)
GO


CREATE TABLE shopping
(
	id VARCHAR(150) PRIMARY KEY,
	client_id VARCHAR(150),
	shopping_state INT,
	is_active BIT,
	--created_at_utc DATETIME,
	--updated_at_utc DATETIME,
	FOREIGN KEY (client_id) REFERENCES clients(id)
)
GO

CREATE TABLE shopping_detail
(
	id VARCHAR(150) PRIMARY KEY,
	shopping_id VARCHAR(150),
	product_id VARCHAR(150),
	quantity INT,
	is_active BIT,
	--created_at_utc DATETIME,
	--updated_at_utc DATETIME,
	FOREIGN KEY (shopping_id) REFERENCES shopping(id),
	FOREIGN KEY (product_id) REFERENCES product(id)
)
GO