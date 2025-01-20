# Database Schema Documentation
## ERD
![ecommerce-erd](https://github.com/user-attachments/assets/e9a93acd-728b-4dbd-a6da-f8a4f766acc3)

## Tables Overview

### category
**Purpose**: Stores information about product categories (e.g., Electronics, Mobiles).

| Column               | Description                                           |
|----------------------|-------------------------------------------------------|
| `id`                 | Unique identifier for the category (Primary Key).     |
| `name`               | The name of the category (e.g., "Electronics").       |
| `description`        | A brief description of the category.                  |
| `image_url`          | URL to an image representing the category.            |
| `parent_category_id` | References another category if this category is a subcategory (self-referencing foreign key). |
| `active`             | Indicates whether the category is active or not (default is TRUE). |

### product
**Purpose**: Stores information about products available for purchase.

| Column       | Description                                                                 |
|--------------|-----------------------------------------------------------------------------|
| `id`         | Unique identifier for the product (Primary Key).                           |
| `name`       | The name of the product (e.g., "iPhone 14").                               |
| `price`      | The current price of the product.                                          |
| `mrp`        | Maximum Retail Price of the product.                                       |
| `category_id`| Foreign key referencing the category table (denotes the category to which the product belongs). |
| `created_at` | Timestamp when the product was created.                                    |
| `description`| Detailed description of the product.                                       |
| `thumbnail`  | A URL to the product’s thumbnail image.                                     |

### product_images
**Purpose**: Stores multiple images associated with products.

| Column     | Description                                               |
|------------|-----------------------------------------------------------|
| `id`       | Unique identifier for the product image (Primary Key).     |
| `product_id`| Foreign key referencing the product table.               |
| `image_url`| URL of the product image.                                 |
| `created_at`| Timestamp when the image was added.                       |

### user
**Purpose**: Stores information about users (customers).

| Column       | Description                                    |
|--------------|------------------------------------------------|
| `id`         | Unique identifier for the user (Primary Key).  |
| `first_name` | User's first name.                             |
| `last_name`  | User's last name.                              |
| `dob`        | User's date of birth.                          |
| `phone`      | User's contact phone number.                   |

### user_addresses
**Purpose**: Stores user addresses for delivery purposes.

| Column       | Description                                               |
|--------------|-----------------------------------------------------------|
| `id`         | Unique identifier for the address (Primary Key).         |
| `user_id`    | Foreign key referencing the user table (user associated with the address). |
| `latitude`   | Latitude coordinate of the address.                      |
| `longitude`  | Longitude coordinate of the address.                     |
| `name`       | Name/label of the address (e.g., "Home", "Office").      |
| `landmark`   | Nearby landmark for easier location.                     |
| `pincode`    | Postal code for the address.                             |
| `is_default` | Indicates if the address is the user's default address.  |

### order
**Purpose**: Stores order details made by users.

| Column         | Description                                               |
|----------------|-----------------------------------------------------------|
| `id`           | Unique identifier for the order (Primary Key).            |
| `price`        | Total price of the order.                                 |
| `mrp`          | Maximum Retail Price of the items in the order.           |
| `user_id`      | Foreign key referencing the user table (user who placed the order). |
| `created_at`   | Timestamp when the order was placed.                      |
| `address_id`   | Foreign key referencing the user_addresses table (shipping address for the order). |
| `payment_mode` | Payment method used (e.g., upi, cod).                     |
| `payment_status`| Current status of the payment (e.g., pending, completed). |
| `order_status` | Current status of the order (e.g., placed, shipped, delivered, cancelled). |

### order_items
**Purpose**: Stores the details of products in each order (many-to-many relationship between orders and products).

| Column       | Description                                               |
|--------------|-----------------------------------------------------------|
| `id`         | Unique identifier for the order item (Primary Key).       |
| `product_id` | Foreign key referencing the product table (product associated with the order). |
| `order_id`   | Foreign key referencing the order table (order to which the product belongs). |
| `quantity`   | Number of units of the product ordered.                   |
| `price`      | Price of the product at the time of the order.            |
| `mrp`        | Maximum Retail Price of the product at the time of the order. |

### cart_items
**Purpose**: Stores items in the user's shopping cart before they are ordered.

| Column       | Description                                               |
|--------------|-----------------------------------------------------------|
| `user_id`    | Foreign key referencing the user table (user who owns the cart). |
| `product_id` | Foreign key referencing the product table (product added to the cart). |
| `quantity`   | Number of units of the product in the cart.               |

## Relationships Between Tables

### category ↔ product:
One-to-many relationship: A category can have multiple products, but each product belongs to one category.

### product ↔ product_images:
One-to-many relationship: A product can have multiple images, but each image is associated with one product.

### user ↔ user_addresses:
One-to-many relationship: A user can have multiple addresses, but each address belongs to one user.

### user ↔ order:
One-to-many relationship: A user can place many orders, but each order belongs to one user.

### order ↔ order_items:
One-to-many relationship: An order can have multiple order items, but each order item is associated with one order.

### product ↔ order_items:
Many-to-many relationship: A product can appear in multiple orders, and an order can contain multiple products.

### user ↔ cart_items:
One-to-many relationship: A user can have many items in the cart, but each cart item belongs to one user.

## SQL Code

### Creating Tables

```sql
CREATE TABLE category(
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    decription TEXT,
    image_url TEXT,
    parent_category_id INT,
    active BOOLEAN DEFAULT TRUE
);

CREATE TABLE product(
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price INT,
    mrp INT,
    category_id INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    description TEXT,
    thumbnail TEXT,
    FOREIGN KEY (category_id) REFERENCES category(id)
    ON DELETE SET NULL
    ON UPDATE CASCADE
);

CREATE TABLE product_images (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    image_url TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES product(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE user (
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    dob DATE,
    phone VARCHAR(15)
);

CREATE TABLE user_addresses (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    latitude DECIMAL(10, 8) NOT NULL,
    longitude DECIMAL(10, 8) NOT NULL,
    name VARCHAR(255) NOT NULL,
    landmark VARCHAR(255),
    pincode VARCHAR(10) NOT NULL,
    is_default BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES user(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE order (
    id INT AUTO_INCREMENT PRIMARY KEY,
    price INT NOT NULL,
    mrp INT NOT NULL,
    user_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    address_id INT NOT NULL,
    payment_mode ENUM ('upi', 'cod') NOT NULL,
    payment_status ENUM('pending', 'completed', 'failed') DEFAULT 'pending',
    order_status ENUM('placed', 'shipped', 'delivered', 'cancelled') DEFAULT 'placed',
    FOREIGN KEY (user_id) REFERENCES user(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
    FOREIGN KEY (address_id) REFERENCES user_addresses(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE order_items (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    order_id INT NOT NULL,
    quantity INT NOT NULL,
    price INT NOT NULL,
    mrp INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES `order`(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
    FOREIGN KEY (product_id) REFERENCES product(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE cart_items(
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    PRIMARY KEY  (user_id, product_id),
    FOREIGN KEY (user_id) REFERENCES user(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
    FOREIGN KEY (product_id) REFERENCES product(id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);
```

### Inserting Sample Data
```sql
-- Insert sample categories
INSERT INTO category (name, description, image_url, parent_category_id) 
VALUES 
('Electronics', 'Electronic gadgets and devices', 'https://example.com/electronics.jpg', NULL),
('Mobiles', 'Smartphones and accessories', 'https://example.com/mobiles.jpg', 1);

-- Insert sample product
INSERT INTO product (name, price, mrp, category_id, description, thumbnail) 
VALUES 
('iPhone 14', 799, 999, 2, 'Latest model of iPhone with A15 Bionic chip', 'https://example.com/iphone14.jpg');

-- Insert sample user
INSERT INTO user (first_name, last_name, dob, phone) 
VALUES ('John', 'Doe', '1990-01-01', '1234567890');

-- Insert sample address
INSERT INTO user_addresses (user_id, latitude, longitude, name, landmark, pincode, is_default) 
VALUES (1, 28.7041, 77.1025, 'Home', 'Near Connaught Place', '110001', TRUE);

-- Insert sample order
INSERT INTO `order` (price, mrp, user_id, address_id, payment_mode, payment_status, order_status) 
VALUES (799, 999, 1, 1, 'upi', 'completed', 'placed');

-- Insert sample order item
INSERT INTO order_items (product_id, order_id, quantity, price, mrp) 
VALUES (1, 1, 1, 799, 999);
```

### Quering and CRUD Operations
```sql
-- Insert a new product into the product table:
INSERT INTO product (name, price, mrp, category_id, description, thumbnail) 
VALUES ('MacBook Air M2', 120000, 125000, 3, 'Apple MacBook Air with M2 chip', 'https://example.com/macbook_air.jpg');

-- Insert a new customer into the user table:
INSERT INTO user (first_name, last_name, dob, phone) 
VALUES ('Alice', 'Brown', '1992-11-25', '9876543210');


-- Retrieve all products in a specific category (Electronics):
SELECT p.id, p.name, p.price, p.mrp, c.name AS category_name 
FROM product p 
JOIN category c ON p.category_id = c.id 
WHERE c.name = 'Electronics';

-- Retrieve products within a price range:
SELECT id, name, price, mrp 
FROM product 
WHERE price BETWEEN 50000 AND 100000;

-- Retrieve customers with specific criteria (e.g., first name starts with 'J'):
SELECT id, first_name, last_name, dob, phone 
FROM user 
WHERE first_name LIKE 'J%';

-- Retrieve orders placed by a specific customer (e.g., user_id = 1):
SELECT o.id, o.price, o.order_status, o.payment_mode, o.created_at 
FROM `order` o 
WHERE o.user_id = 1;

-- Update the price of a product:
UPDATE product 
SET price = 65000, mrp = 70000 
WHERE id = 1;

-- Update customer details (phone number):
UPDATE user 
SET phone = '1234509876' 
WHERE id = 2;

-- Update order status for a specific order:
UPDATE `order` 
SET order_status = 'shipped', payment_status = 'completed' 
WHERE id = 1;


-- Delete a product by ID:
DELETE FROM product 
WHERE id = 3;

-- Delete a customer and cascade their associated data:
DELETE FROM user 
WHERE id = 2;
```

### Advanced Queries and Joins 
```sql
-- Query: List orders with customer name, product name, and order details.
SELECT 
    o.id AS order_id,
    CONCAT(u.first_name, ' ', u.last_name) AS customer_name,
    p.name AS product_name,
    oi.quantity,
    oi.price AS item_price,
    o.created_at AS order_date
FROM `order` o
JOIN order_items oi ON o.id = oi.order_id
JOIN product p ON oi.product_id = p.id
JOIN user u ON o.user_id = u.id
ORDER BY o.created_at DESC;

-- Query: Calculate the total revenue for orders placed in January 2025.
SELECT 
    SUM(o.price) AS total_revenue,
    COUNT(o.id) AS total_orders
FROM `order` o
WHERE o.created_at BETWEEN '2025-01-01' AND '2025-01-31';

-- Query: List customers with the number of orders they placed, filtered to show only customers with more than one order.
SELECT 
    CONCAT(u.first_name, ' ', u.last_name) AS customer_name,
    COUNT(o.id) AS total_orders
FROM user u
JOIN `order` o ON u.id = o.user_id
GROUP BY u.id
HAVING COUNT(o.id) > 1
ORDER BY total_orders DESC;

-- Query: List products with their total quantity sold and total revenue generated.
SELECT 
    p.name AS product_name,
    SUM(oi.quantity) AS total_quantity_sold,
    SUM(oi.price * oi.quantity) AS total_revenue
FROM product p
JOIN order_items oi ON p.id = oi.product_id
GROUP BY p.id
ORDER BY total_revenue DESC;

-- Query: List orders with customer name and delivery address details.
SELECT 
    o.id AS order_id,
    CONCAT(u.first_name, ' ', u.last_name) AS customer_name,
    ua.name AS delivery_address,
    ua.pincode,
    o.price AS total_price,
    o.order_status
FROM `order` o
JOIN user u ON o.user_id = u.id
JOIN user_addresses ua ON o.address_id = ua.id
ORDER BY o.created_at DESC;

```
### Indexing and Optimization 
#### Indexing:
**Purpose**: Indexes are used to speed up the retrieval of rows from a database table by creating an efficient structure that allows for faster searching, sorting, and filtering.
**Benefits**:
- Faster Query Performance: Indexes make query execution faster, particularly for large datasets.
- Efficient Search Operations: They are particularly helpful for SELECT, WHERE, JOIN, ORDER BY, and GROUP BY operations.
- Reduced I/O: By minimizing the number of rows to be scanned, indexes reduce the I/O load.
#### When to Use Indexing:
- Primary Key and Foreign Key Columns: These are indexed by default in most database systems.
- Frequently Queried Columns: Columns that are frequently involved in WHERE, JOIN, ORDER BY, and GROUP BY clauses should be indexed.
- Columns with Uniqueness: Columns that have unique values (e.g., email, username) benefit from indexes.
#### Identifying Columns for Indexing Based on Query Patterns
```sql
-- Create Index on created_at for Orders:
CREATE INDEX idx_order_created_at ON `order` (created_at);

-- Create Index on user_id in order and user_addresses:
CREATE INDEX idx_order_user_id ON `order` (user_id);
CREATE INDEX idx_user_addresses_user_id ON user_addresses (user_id);


-- Create Composite Index on order_items for Faster Joins:
CREATE INDEX idx_order_items_order_product ON order_items (order_id, product_id);
```
