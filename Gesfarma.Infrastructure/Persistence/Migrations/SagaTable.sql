CREATE TABLE shopping_saga_data(
  Id VARCHAR(40) PRIMARY KEY,
  Originator VARCHAR(255),
  OriginalMessageId VARCHAR(255),
  SaleOrderId VARCHAR(50),
  ClientId VARCHAR(50),
  ShoppingState INT
)
GO


CREATE TABLE shopping_detail_saga_data(
  Id VARCHAR(40) PRIMARY KEY,
  Originator VARCHAR(255),
  OriginalMessageId VARCHAR(255),
  SaleOrderId VARCHAR(50),
  ProductId VARCHAR(50),
  Quantity INT
)
GO