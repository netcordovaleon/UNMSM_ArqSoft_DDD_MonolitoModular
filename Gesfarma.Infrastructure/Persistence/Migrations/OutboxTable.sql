CREATE TABLE outbox(
  outbox_id INT PRIMARY KEY IDENTITY,
  message_id VARCHAR(255),
  dispatched BIT,
  dispatched_at DATETIME,
  transport_operations VARCHAR(MAX)
)
GO