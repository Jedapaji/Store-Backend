# Store Backend API - Quick Reference Guide

## 🚀 Quick Start

### Access Swagger Documentation
- **URL**: `https://localhost:5001/swagger/index.html`
- **JSON Schema**: `https://localhost:5001/swagger/v1/swagger.json`

---

## 📋 Endpoint Summary

### Products (5 endpoints)
```
GET    /api/products                    → Get all products
GET    /api/products/{id}               → Get product by ID
POST   /api/products                    → Create product
PUT    /api/products/{id}               → Update product
DELETE /api/products/{id}               → Delete product
```

### Categories (5 endpoints)
```
GET    /api/categories                  → Get all categories
GET    /api/categories/{id}             → Get category by ID
POST   /api/categories                  → Create category
PUT    /api/categories/{id}             → Update category
DELETE /api/categories/{id}             → Delete category
```

### Customers (5 endpoints)
```
GET    /api/customers                   → Get all customers
GET    /api/customers/{id}              → Get customer by ID
POST   /api/customers                   → Create customer
PUT    /api/customers/{id}              → Update customer
DELETE /api/customers/{id}              → Delete customer
```

### Orders (6 endpoints)
```
GET    /api/orders                                        → Get all orders
GET    /api/orders/{id}                                   → Get order by ID
POST   /api/orders                                        → Create simple order
POST   /api/orders/CreatePurchaseOrder/{customerId}      → Create purchase order with inventory
PUT    /api/orders/{id}                                   → Update order
DELETE /api/orders/{id}                                   → Delete order
```

---

## 🔥 Most Important Endpoints

### 1. Create Purchase Order (NEW!)
**Endpoint**: `POST /api/orders/CreatePurchaseOrder/{customerId}`

**Features**:
- ✅ Multi-item orders
- ✅ Automatic stock deduction
- ✅ Transaction-based (all-or-nothing)
- ✅ Error rollback

**Example Request**:
```bash
POST https://localhost:5001/api/orders/CreatePurchaseOrder/1
Content-Type: application/json

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

**Success Response** (201 Created):
```json
{
  "orderId": 1,
  "customerId": 1,
  "orderState": "2024-09-25T12:30:00Z",
  "totalAmount": 1059.97
}
```

---

## 📝 Common Request/Response Patterns

### Create Resource
```bash
POST /api/{resource}
Content-Type: application/json

{
  "field1": "value1",
  "field2": "value2"
}
```

**Response**: `201 Created` + Resource object

---

### Update Resource
```bash
PUT /api/{resource}/{id}
Content-Type: application/json

{
  "field1": "updated_value"
}
```

**Response**: `200 OK` + Updated resource object

---

### Delete Resource
```bash
DELETE /api/{resource}/{id}
```

**Response**: `204 No Content`

---

## ⚠️ Error Responses

All errors follow this format:

```json
{
  "statusCode": 400,
  "message": "Descriptive error message",
  "details": "Stack trace (development only)",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

### Common Errors

| Status | Error | Solution |
|---|---|---|
| 400 | Validation error | Check required fields and data types |
| 404 | Resource not found | Verify the ID exists |
| 400 | Insufficient stock | Reduce quantity in purchase order |
| 500 | Server error | Check logs and retry |

---

## 🔐 Security Notes

- **CORS**: Enabled for all origins (⚠️ Configure for production)
- **Authentication**: Currently open (JWT planned for v2)
- **HTTPS**: Required in production
- **Database**: SQL Server with Windows authentication

---

## 💾 Data Models Quick Reference

### Product
```json
{
  "productId": 0,
  "name": "string",
  "description": "string",
  "price": 0.00,
  "stock": 0,
  "categoryId": 0
}
```

### Category
```json
{
  "categoryId": 0,
  "categoryName": "string",
  "description": "string"
}
```

### Customer
```json
{
  "customerId": 0,
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "address": "string"
}
```

### Order
```json
{
  "orderId": 0,
  "customerId": 0,
  "orderState": "2024-09-25T10:30:00Z",
  "totalAmount": 0.00
}
```

---

## 🧪 Testing with cURL

### Get All Products
```bash
curl -X GET "https://localhost:5001/api/products"
```

### Create Product
```bash
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
	"name": "New Product",
	"description": "Product description",
	"price": 99.99,
	"stock": 10,
	"categoryId": 1
  }'
```

### Create Purchase Order
```bash
curl -X POST "https://localhost:5001/api/orders/CreatePurchaseOrder/1" \
  -H "Content-Type: application/json" \
  -d '{
	"items": [
	  {
		"productId": 1,
		"name": "Laptop",
		"price": 999.99,
		"stock": 10,
		"categoryId": 1,
		"quantity": 1
	  }
	],
	"total": 999.99,
	"totalItems": 1
  }'
```

---

## 📊 API Statistics

- **Total Endpoints**: 21
- **Resource Types**: 4 (Products, Categories, Customers, Orders)
- **CRUD Operations**: 20 standard endpoints
- **Special Operations**: 1 (CreatePurchaseOrder)
- **Error Handling**: Centralized middleware
- **Response Format**: JSON

---

## 🔄 Workflow Example: Complete Order

1. **Create Category** (if needed)
   ```
   POST /api/categories
   ```

2. **Create Products** in the category
   ```
   POST /api/products
   ```

3. **Create Customer**
   ```
   POST /api/customers
   ```

4. **Create Purchase Order** (with items + inventory deduction)
   ```
   POST /api/orders/CreatePurchaseOrder/{customerId}
   ```

5. **Verify Order**
   ```
   GET /api/orders/{orderId}
   ```

---

## 🛠️ Development Notes

### Architecture
- **Pattern**: Layered/3-tier architecture
- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server (Entity Framework Core)
- **Dependencies**: 
  - Repository Pattern for data access
  - Service Layer for business logic
  - DTOs for API contracts
  - Middleware for error handling

### Key Features
✅ Async/await patterns  
✅ Dependency injection  
✅ Global error handling  
✅ Database transactions  
✅ Input validation  
✅ XML documentation  
✅ Swagger/OpenAPI support  

---

## 📚 Additional Resources

- **Full API Contract**: See `API_CONTRACT.md`
- **Swagger Documentation**: `https://localhost:5001/swagger/index.html`
- **Repository**: `https://github.com/Jedapaji/Store-Backend`

---

**Last Updated**: September 25, 2024  
**Version**: v1.0.0
