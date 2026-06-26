# Store Backend API - Contract Documentation

## API Overview

**Base URL**: `https://localhost:5001/api`  
**Version**: v1  
**Framework**: ASP.NET Core 8.0  
**Database**: SQL Server  

---

## Table of Contents

1. [Authentication](#authentication)
2. [Error Handling](#error-handling)
3. [Products Endpoints](#products-endpoints)
4. [Categories Endpoints](#categories-endpoints)
5. [Customers Endpoints](#customers-endpoints)
6. [Orders Endpoints](#orders-endpoints)
7. [Data Models](#data-models)

---

## Authentication

Currently, the API is open to all requests with CORS enabled for all origins. Future versions will implement JWT authentication.

**Future Bearer Token Format**:
```
Authorization: Bearer {token}
```

---

## Error Handling

All endpoints return standardized error responses with the following structure:

### Error Response Schema
```json
{
  "statusCode": 400,
  "message": "Descriptive error message",
  "details": "Stack trace (only in development)",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

### Common HTTP Status Codes

| Status Code | Meaning |
|---|---|
| 200 | OK - Request successful |
| 201 | Created - Resource created successfully |
| 204 | No Content - Successful request with no response body |
| 400 | Bad Request - Invalid input or validation error |
| 404 | Not Found - Resource not found |
| 500 | Internal Server Error - Server-side error |

---

## Products Endpoints

### 1. Get All Products
```http
GET /api/products
```

**Description**: Retrieve all products from the store.

**Response** (200 OK):
```json
[
  {
	"productId": 1,
	"name": "Laptop",
	"description": "High-performance laptop",
	"price": 999.99,
	"stock": 10,
	"categoryId": 1
  },
  {
	"productId": 2,
	"name": "Mouse",
	"description": "Wireless mouse",
	"price": 29.99,
	"stock": 50,
	"categoryId": 2
  }
]
```

---

### 2. Get Product by ID
```http
GET /api/products/{id}
```

**Parameters**:
- `id` (integer, required): Product identifier

**Response** (200 OK):
```json
{
  "productId": 1,
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99,
  "stock": 10,
  "categoryId": 1
}
```

**Error Response** (404 Not Found):
```json
{
  "statusCode": 404,
  "message": "Product with ID 999 not found.",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

---

### 3. Create Product
```http
POST /api/products
Content-Type: application/json
```

**Request Body**:
```json
{
  "name": "Keyboard",
  "description": "Mechanical keyboard",
  "price": 149.99,
  "stock": 30,
  "categoryId": 2
}
```

**Response** (201 Created):
```json
{
  "productId": 3,
  "name": "Keyboard",
  "description": "Mechanical keyboard",
  "price": 149.99,
  "stock": 30,
  "categoryId": 2
}
```

**Validation Rules**:
- `name`: Required, not empty
- `description`: Required, not empty
- `price`: Required, must be >= 0
- `stock`: Required, must be >= 0
- `categoryId`: Required, category must exist

**Error Response** (400 Bad Request):
```json
{
  "statusCode": 400,
  "message": "Category with ID 999 not found.",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

---

### 4. Update Product
```http
PUT /api/products/{id}
Content-Type: application/json
```

**Parameters**:
- `id` (integer, required): Product identifier

**Request Body** (all fields optional):
```json
{
  "name": "Updated Keyboard",
  "description": "Updated description",
  "price": 159.99,
  "stock": 25,
  "categoryId": 2
}
```

**Response** (200 OK):
```json
{
  "productId": 3,
  "name": "Updated Keyboard",
  "description": "Updated description",
  "price": 159.99,
  "stock": 25,
  "categoryId": 2
}
```

---

### 5. Delete Product
```http
DELETE /api/products/{id}
```

**Parameters**:
- `id` (integer, required): Product identifier

**Response** (204 No Content):
```
(empty body)
```

---

## Categories Endpoints

### 1. Get All Categories
```http
GET /api/categories
```

**Response** (200 OK):
```json
[
  {
	"categoryId": 1,
	"categoryName": "Electronics",
	"description": "Electronic devices and accessories"
  },
  {
	"categoryId": 2,
	"categoryName": "Computers",
	"description": "Computers and computer accessories"
  }
]
```

---

### 2. Get Category by ID
```http
GET /api/categories/{id}
```

**Parameters**:
- `id` (integer, required): Category identifier

**Response** (200 OK):
```json
{
  "categoryId": 1,
  "categoryName": "Electronics",
  "description": "Electronic devices and accessories"
}
```

---

### 3. Create Category
```http
POST /api/categories
Content-Type: application/json
```

**Request Body**:
```json
{
  "categoryName": "Peripherals",
  "description": "Computer peripherals and accessories"
}
```

**Response** (201 Created):
```json
{
  "categoryId": 3,
  "categoryName": "Peripherals",
  "description": "Computer peripherals and accessories"
}
```

**Validation Rules**:
- `categoryName`: Required, not empty
- `description`: Required, not empty

---

### 4. Update Category
```http
PUT /api/categories/{id}
Content-Type: application/json
```

**Parameters**:
- `id` (integer, required): Category identifier

**Request Body** (all fields optional):
```json
{
  "categoryName": "Updated Peripherals",
  "description": "Updated description"
}
```

**Response** (200 OK):
```json
{
  "categoryId": 3,
  "categoryName": "Updated Peripherals",
  "description": "Updated description"
}
```

---

### 5. Delete Category
```http
DELETE /api/categories/{id}
```

**Parameters**:
- `id` (integer, required): Category identifier

**Response** (204 No Content):
```
(empty body)
```

---

## Customers Endpoints

### 1. Get All Customers
```http
GET /api/customers
```

**Response** (200 OK):
```json
[
  {
	"customerId": 1,
	"firstName": "John",
	"lastName": "Doe",
	"email": "john@example.com",
	"address": "123 Main St"
  },
  {
	"customerId": 2,
	"firstName": "Jane",
	"lastName": "Smith",
	"email": "jane@example.com",
	"address": "456 Oak Ave"
  }
]
```

---

### 2. Get Customer by ID
```http
GET /api/customers/{id}
```

**Parameters**:
- `id` (integer, required): Customer identifier

**Response** (200 OK):
```json
{
  "customerId": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "address": "123 Main St"
}
```

---

### 3. Create Customer
```http
POST /api/customers
Content-Type: application/json
```

**Request Body**:
```json
{
  "firstName": "Robert",
  "lastName": "Johnson",
  "email": "robert@example.com",
  "address": "789 Pine Rd"
}
```

**Response** (201 Created):
```json
{
  "customerId": 3,
  "firstName": "Robert",
  "lastName": "Johnson",
  "email": "robert@example.com",
  "address": "789 Pine Rd"
}
```

**Validation Rules**:
- `firstName`: Required, not empty
- `lastName`: Required, not empty
- `email`: Required, not empty
- `address`: Required, not empty

---

### 4. Update Customer
```http
PUT /api/customers/{id}
Content-Type: application/json
```

**Parameters**:
- `id` (integer, required): Customer identifier

**Request Body** (all fields optional):
```json
{
  "firstName": "Robert",
  "lastName": "Johnson",
  "email": "robert.new@example.com",
  "address": "999 Elm St"
}
```

**Response** (200 OK):
```json
{
  "customerId": 3,
  "firstName": "Robert",
  "lastName": "Johnson",
  "email": "robert.new@example.com",
  "address": "999 Elm St"
}
```

---

### 5. Delete Customer
```http
DELETE /api/customers/{id}
```

**Parameters**:
- `id` (integer, required): Customer identifier

**Response** (204 No Content):
```
(empty body)
```

---

## Orders Endpoints

### 1. Get All Orders
```http
GET /api/orders
```

**Response** (200 OK):
```json
[
  {
	"orderId": 1,
	"customerId": 1,
	"orderState": "2024-09-25T10:30:00Z",
	"totalAmount": 1029.98
  },
  {
	"orderId": 2,
	"customerId": 2,
	"orderState": "2024-09-25T11:45:00Z",
	"totalAmount": 299.99
  }
]
```

---

### 2. Get Order by ID
```http
GET /api/orders/{id}
```

**Parameters**:
- `id` (integer, required): Order identifier

**Response** (200 OK):
```json
{
  "orderId": 1,
  "customerId": 1,
  "orderState": "2024-09-25T10:30:00Z",
  "totalAmount": 1029.98
}
```

---

### 3. Create Order
```http
POST /api/orders
Content-Type: application/json
```

**Description**: Create a simple order without managing inventory.

**Request Body**:
```json
{
  "customerId": 1,
  "orderState": "2024-09-25T12:00:00Z",
  "totalAmount": 500.00
}
```

**Response** (201 Created):
```json
{
  "orderId": 3,
  "customerId": 1,
  "orderState": "2024-09-25T12:00:00Z",
  "totalAmount": 500.00
}
```

**Validation Rules**:
- `customerId`: Required, customer must exist
- `orderState`: Required, valid datetime
- `totalAmount`: Required, must be >= 0

---

### 4. Create Purchase Order (with Inventory Management)
```http
POST /api/orders/CreatePurchaseOrder/{customerId}
Content-Type: application/json
```

**Parameters**:
- `customerId` (integer, required): Customer identifier

**Description**: Create an order with multiple items and automatic inventory management. Uses database transactions to ensure data consistency.

**Request Body**:
```json
{
  "items": [
	{
	  "productId": 1,
	  "name": "Laptop",
	  "price": 999.99,
	  "stock": 10,
	  "categoryId": 1,
	  "quantity": 1
	},
	{
	  "productId": 2,
	  "name": "Mouse",
	  "price": 29.99,
	  "stock": 50,
	  "categoryId": 2,
	  "quantity": 2
	}
  ],
  "total": 1059.97,
  "totalItems": 3
}
```

**Response** (201 Created):
```json
{
  "orderId": 4,
  "customerId": 1,
  "orderState": "2024-09-25T12:30:00Z",
  "totalAmount": 1059.97
}
```

**Features**:
- ✅ Multi-item order processing
- ✅ Automatic stock deduction
- ✅ Database transaction (all-or-nothing)
- ✅ Automatic order detail creation
- ✅ Total calculation verification

**Validation Rules**:
- `customerId`: Required, customer must exist
- `items`: Required, at least 1 item
- `items[].productId`: Required, product must exist
- `items[].quantity`: Required, must be > 0 and not exceed available stock
- `total`: Must match calculated total (within 0.01 tolerance)

**Error Responses**:

**404 Not Found - Customer not found**:
```json
{
  "statusCode": 404,
  "message": "Customer with ID 999 not found.",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

**400 Bad Request - Product not found**:
```json
{
  "statusCode": 400,
  "message": "The product with ID 999 was not found.",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

**400 Bad Request - Insufficient stock**:
```json
{
  "statusCode": 400,
  "message": "There is not enough stock for the product 'Laptop'. Available: 5, Requested: 10",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

**400 Bad Request - Total mismatch**:
```json
{
  "statusCode": 400,
  "message": "Purchase order total mismatch. Expected: 1059.97, Provided: 1000.00",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

---

### 5. Update Order
```http
PUT /api/orders/{id}
Content-Type: application/json
```

**Parameters**:
- `id` (integer, required): Order identifier

**Request Body** (all fields optional):
```json
{
  "customerId": 2,
  "orderState": "2024-09-25T14:00:00Z",
  "totalAmount": 600.00
}
```

**Response** (200 OK):
```json
{
  "orderId": 3,
  "customerId": 2,
  "orderState": "2024-09-25T14:00:00Z",
  "totalAmount": 600.00
}
```

---

### 6. Delete Order
```http
DELETE /api/orders/{id}
```

**Parameters**:
- `id` (integer, required): Order identifier

**Response** (204 No Content):
```
(empty body)
```

---

## Data Models

### ProductDto
```json
{
  "productId": 1,
  "name": "string",
  "description": "string",
  "price": 0.00,
  "stock": 0,
  "categoryId": 0
}
```

### CreateProductDto
```json
{
  "name": "string (required)",
  "description": "string (required)",
  "price": 0.00,
  "stock": 0,
  "categoryId": 0
}
```

### UpdateProductDto
```json
{
  "name": "string (optional)",
  "description": "string (optional)",
  "price": 0.00,
  "stock": 0,
  "categoryId": 0
}
```

### CategoryDto
```json
{
  "categoryId": 1,
  "categoryName": "string",
  "description": "string"
}
```

### CreateCategoryDto
```json
{
  "categoryName": "string (required)",
  "description": "string (required)"
}
```

### UpdateCategoryDto
```json
{
  "categoryName": "string (optional)",
  "description": "string (optional)"
}
```

### CustomerDto
```json
{
  "customerId": 1,
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "address": "string"
}
```

### CreateCustomerDto
```json
{
  "firstName": "string (required)",
  "lastName": "string (required)",
  "email": "string (required)",
  "address": "string (required)"
}
```

### UpdateCustomerDto
```json
{
  "firstName": "string (optional)",
  "lastName": "string (optional)",
  "email": "string (optional)",
  "address": "string (optional)"
}
```

### OrderDto
```json
{
  "orderId": 1,
  "customerId": 1,
  "orderState": "2024-09-25T10:30:00Z",
  "totalAmount": 0.00
}
```

### CreateOrderDto
```json
{
  "customerId": 1,
  "orderState": "2024-09-25T10:30:00Z",
  "totalAmount": 0.00
}
```

### UpdateOrderDto
```json
{
  "customerId": 1,
  "orderState": "2024-09-25T10:30:00Z",
  "totalAmount": 0.00
}
```

### PurchaseOrderItem
```json
{
  "productId": 1,
  "name": "string",
  "price": 0.00,
  "stock": 0,
  "categoryId": 1,
  "quantity": 1
}
```

### PurchaseOrder
```json
{
  "items": [
	{
	  "productId": 1,
	  "name": "string",
	  "price": 0.00,
	  "stock": 0,
	  "categoryId": 1,
	  "quantity": 1
	}
  ],
  "total": 0,
  "totalItems": 0
}
```

### ErrorResponse
```json
{
  "statusCode": 400,
  "message": "string",
  "details": "string (development only)",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

---

## API Summary Table

| Resource | Method | Endpoint | Description |
|---|---|---|---|
| **Products** | GET | `/api/products` | Get all products |
| | GET | `/api/products/{id}` | Get product by ID |
| | POST | `/api/products` | Create new product |
| | PUT | `/api/products/{id}` | Update product |
| | DELETE | `/api/products/{id}` | Delete product |
| **Categories** | GET | `/api/categories` | Get all categories |
| | GET | `/api/categories/{id}` | Get category by ID |
| | POST | `/api/categories` | Create new category |
| | PUT | `/api/categories/{id}` | Update category |
| | DELETE | `/api/categories/{id}` | Delete category |
| **Customers** | GET | `/api/customers` | Get all customers |
| | GET | `/api/customers/{id}` | Get customer by ID |
| | POST | `/api/customers` | Create new customer |
| | PUT | `/api/customers/{id}` | Update customer |
| | DELETE | `/api/customers/{id}` | Delete customer |
| **Orders** | GET | `/api/orders` | Get all orders |
| | GET | `/api/orders/{id}` | Get order by ID |
| | POST | `/api/orders` | Create new order |
| | POST | `/api/orders/CreatePurchaseOrder/{customerId}` | Create purchase order with inventory management |
| | PUT | `/api/orders/{id}` | Update order |
| | DELETE | `/api/orders/{id}` | Delete order |

---

## Accessing Swagger UI

Once the application is running:

- **Swagger UI**: `https://localhost:5001/swagger/index.html`
- **OpenAPI JSON**: `https://localhost:5001/swagger/v1/swagger.json`

---

## Architecture Overview

The API follows a **3-tier layered architecture**:

```
┌─────────────────────────────────────┐
│     API Layer (Controllers)          │
│  ✓ Handles HTTP requests/responses  │
│  ✓ Returns DTOs (Data Transfer Objects)
└──────────────┬──────────────────────┘
			   │
┌──────────────▼──────────────────────┐
│   Business Logic Layer (Services)   │
│  ✓ Validates business rules         │
│  ✓ Performs calculations            │
│  ✓ Maps entities to DTOs            │
│  ✓ Handles transactions             │
└──────────────┬──────────────────────┘
			   │
┌──────────────▼──────────────────────┐
│   Data Access Layer (Repositories)  │
│  ✓ Database operations              │
│  ✓ CRUD operations                  │
│  ✓ Query abstraction                │
└──────────────┬──────────────────────┘
			   │
┌──────────────▼──────────────────────┐
│       SQL Server Database           │
│  ✓ Store & retrieve data            │
└─────────────────────────────────────┘
```

---

## Version History

| Version | Date | Changes |
|---|---|---|
| v1 | 2024-09-25 | Initial release with full CRUD operations, inventory management, and error handling |

---

**Last Updated**: September 25, 2024  
**API Version**: v1.0.0  
**Status**: Production Ready ✅
