# Store Backend API - Complete Documentation Hub

> **RESTful API for managing a store's products, categories, customers, and orders with advanced inventory management**

---

## 🚀 Quick Start

### Access the API
- **Swagger UI**: `https://localhost:5001/swagger/index.html`
- **Base URL**: `https://localhost:5001/api`
- **OpenAPI JSON**: `https://localhost:5001/swagger/v1/swagger.json`

### Run the API
```bash
cd Store-Backend
dotnet run
```

---

## 📚 Documentation Guides

Choose your documentation based on your role:

### 👨‍💻 I'm a Developer Building Features
**→ Start here**: **QUICK_REFERENCE.md**
- Fast endpoint lookups
- cURL command examples
- Common patterns
- Testing examples
- Error scenarios

### 🔌 I'm Integrating with the API
**→ Start here**: **API_CONTRACT.md**
- Complete endpoint specifications
- Request/response schemas
- Data model definitions
- Validation rules
- All error codes and messages

### ⚙️ I'm Setting Up DevOps/Tools
**→ Start here**: **SWAGGER_DOCUMENTATION.md**
- Swagger configuration details
- Postman integration
- OpenAPI schema info
- Security setup
- Troubleshooting guide

### 🎓 I'm New to This Project
**→ Start here**: **DOCUMENTATION_SUMMARY.md**
- Project overview
- Architecture explanation
- File structure
- Common workflows
- Learning resources

---

## 📋 API Overview

### 📊 Statistics
- **Total Endpoints**: 21
- **Resource Types**: 4 (Products, Categories, Customers, Orders)
- **Standard CRUD**: 20 endpoints
- **Special Operations**: 1 (Purchase Order with inventory)

### 🔗 Endpoints

#### Products (5 endpoints)
```
GET     /api/products              → Get all products
GET     /api/products/{id}         → Get product by ID
POST    /api/products              → Create product
PUT     /api/products/{id}         → Update product
DELETE  /api/products/{id}         → Delete product
```

#### Categories (5 endpoints)
```
GET     /api/categories            → Get all categories
GET     /api/categories/{id}       → Get category by ID
POST    /api/categories            → Create category
PUT     /api/categories/{id}       → Update category
DELETE  /api/categories/{id}       → Delete category
```

#### Customers (5 endpoints)
```
GET     /api/customers             → Get all customers
GET     /api/customers/{id}        → Get customer by ID
POST    /api/customers             → Create customer
PUT     /api/customers/{id}        → Update customer
DELETE  /api/customers/{id}        → Delete customer
```

#### Orders (6 endpoints)
```
GET     /api/orders                → Get all orders
GET     /api/orders/{id}           → Get order by ID
POST    /api/orders                → Create simple order
POST    /api/orders/CreatePurchaseOrder/{customerId} → ⭐ Purchase order with inventory
PUT     /api/orders/{id}           → Update order
DELETE  /api/orders/{id}           → Delete order
```

---

## ⭐ Highlighted Features

### Advanced Purchase Order Management
```
POST /api/orders/CreatePurchaseOrder/{customerId}
```
- **Multi-item orders**: Process multiple products in one request
- **Inventory management**: Automatically deduct stock
- **Atomic transactions**: All-or-nothing processing
- **Error recovery**: Automatic rollback on validation failure
- **Verification**: Total calculation validation

**Example Request**:
```json
POST /api/orders/CreatePurchaseOrder/1

{
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
}
```

### Error Handling
All errors follow a standardized format:
```json
{
  "statusCode": 400,
  "message": "Descriptive error message",
  "details": "Stack trace (development only)",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

---

## 🏗️ Architecture

### 3-Tier Layered Architecture
```
┌─────────────────────────────┐
│  Controllers (API Layer)    │  ← HTTP endpoints, request routing
├─────────────────────────────┤
│  Services (Business Logic)  │  ← Validation, calculations, transactions
├─────────────────────────────┤
│  Repositories (Data Access) │  ← Database operations, CRUD
├─────────────────────────────┤
│  Database (SQL Server)      │  ← Persistent storage
└─────────────────────────────┘
```

### Design Patterns Used
✅ **Repository Pattern** - Data access abstraction  
✅ **Service Layer** - Business logic encapsulation  
✅ **Dependency Injection** - Loose coupling  
✅ **DTO Pattern** - API contracts  
✅ **Error Handling Middleware** - Global exception handling  
✅ **Async/Await** - Non-blocking operations  

---

## 🛠️ Technology Stack

| Component | Technology | Version |
|---|---|---|
| **Runtime** | .NET | 8.0 |
| **Framework** | ASP.NET Core | 8.0 |
| **ORM** | Entity Framework Core | 8.0.8 |
| **Database** | SQL Server | (configured) |
| **API Documentation** | Swagger/OpenAPI | 6.4.0 |
| **Package Manager** | NuGet | (default) |

---

## 📖 Documentation Files

### 1. 📋 API_CONTRACT.md
**Complete API specification document**
- All 21 endpoints fully documented
- Request/response schemas
- Validation rules
- Error scenarios
- Data model definitions
- Security notes
- ~500 lines of detailed documentation

### 2. ⚡ QUICK_REFERENCE.md
**Quick lookup for developers**
- Endpoint summary table
- cURL examples
- Common patterns
- Testing workflows
- ~300 lines of developer-focused content

### 3. 🔧 SWAGGER_DOCUMENTATION.md
**Swagger setup and usage guide**
- Configuration details
- Postman integration
- Feature overview
- Troubleshooting
- ~400 lines of DevOps content

### 4. 🎓 DOCUMENTATION_SUMMARY.md
**Overview and learning guide**
- File structure walkthrough
- Architecture explanation
- Common workflows
- Learning path
- ~600 lines of comprehensive overview

### 5. 📚 README.md (This File)
**Quick navigation hub**
- All documentation links
- Quick API overview
- Getting started guide

---

## 🚀 Getting Started

### Step 1: Start the Application
```bash
cd Store-Backend
dotnet run
```
Application runs at: `https://localhost:5001`

### Step 2: Access Swagger
Open in browser: `https://localhost:5001/swagger/index.html`

### Step 3: Test an Endpoint
- Find Products → GET /api/products
- Click "Try it out"
- Click "Execute"
- See response

### Step 4: Read Documentation
- For API details: **API_CONTRACT.md**
- For quick reference: **QUICK_REFERENCE.md**
- For setup help: **SWAGGER_DOCUMENTATION.md**
- For overview: **DOCUMENTATION_SUMMARY.md**

---

## 💻 Testing the API

### Using Swagger UI (Easiest)
1. Open: `https://localhost:5001/swagger`
2. Click endpoint
3. Click "Try it out"
4. Enter parameters
5. Click "Execute"

### Using cURL
```bash
# Get all products
curl -X GET "https://localhost:5001/api/products"

# Create a product
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
	"name": "Laptop",
	"description": "High-end laptop",
	"price": 999.99,
	"stock": 10,
	"categoryId": 1
  }'
```

### Using Postman
1. Copy OpenAPI JSON: `https://localhost:5001/swagger/v1/swagger.json`
2. Postman → Import → Link
3. Paste the URL
4. Click Import
5. Start testing

---

## 📊 API Response Examples

### Success Response (200 OK)
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

### Error Response (400 Bad Request)
```json
{
  "statusCode": 400,
  "message": "Category with ID 999 not found.",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

### Error Response (404 Not Found)
```json
{
  "statusCode": 404,
  "message": "Product with ID 999 not found.",
  "timestamp": "2024-09-25T10:30:00Z"
}
```

---

## 🔒 Security & Configuration

### Current Setup
- ✅ CORS enabled (all origins)
- ✅ HTTPS enforced
- ✅ SQL Server with Windows authentication
- ✅ Error details hidden in production
- ✅ Input validation on all endpoints

### Connection String
Update in `appsettings.json`:
```json
{
  "ConnectionStrings": {
	"Connection": "Server=YOUR_SERVER;Database=Store-Database;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

---

## 📈 Project Structure

```
Store-Backend/
├── Controllers/              (5 controller files)
│   ├── ProductsController.cs
│   ├── CategoriesController.cs
│   ├── CustomersController.cs
│   └── OrdersController.cs
├── Services/                 (8 service files)
│   ├── IProductService.cs & ProductService.cs
│   ├── ICategoryService.cs & CategoryService.cs
│   ├── ICustomerService.cs & CustomerService.cs
│   └── IOrderService.cs & OrderService.cs
├── Repositories/             (10 repository files)
│   ├── Interfaces: IProductRepository, ICategoryRepository, etc.
│   └── Implementations: ProductRepository, CategoryRepository, etc.
├── DTOs/                     (12 DTO files)
│   ├── ProductDto, CreateProductDto, UpdateProductDto
│   ├── CategoryDto, CreateCategoryDto, UpdateCategoryDto
│   ├── CustomerDto, CreateCustomerDto, UpdateCustomerDto
│   └── OrderDto, CreateOrderDto, UpdateOrderDto
├── Models/                   (6 model files)
├── Exceptions/               (2 files)
├── Middleware/               (1 file)
├── Context/                  (AppDbContext.cs)
├── Program.cs                (Configuration)
└── Documentation/            (4 markdown files)
	├── API_CONTRACT.md
	├── QUICK_REFERENCE.md
	├── SWAGGER_DOCUMENTATION.md
	└── DOCUMENTATION_SUMMARY.md
```

---

## ✨ Key Improvements Made

### ✅ Design Patterns Implemented
- Repository Pattern for data abstraction
- Service Layer for business logic
- Dependency Injection throughout
- DTO pattern for API contracts
- Error Handling Middleware

### ✅ Code Quality
- Async/await patterns
- XML documentation comments
- Input validation
- Error recovery with transactions
- Clean separation of concerns

### ✅ Documentation
- Comprehensive Swagger/OpenAPI support
- 4 detailed markdown guides
- cURL examples
- Postman integration
- Architecture diagrams

### ✅ Features
- 21 RESTful endpoints
- CRUD operations for 4 resources
- Advanced inventory management
- Atomic transactions
- Standardized error responses

---

## 🎯 Common Tasks

### Create an Order with Multiple Items
→ See: **API_CONTRACT.md** → Orders → Create Purchase Order

### List All Products
→ See: **QUICK_REFERENCE.md** → Common Request/Response Patterns

### Handle API Errors
→ See: **API_CONTRACT.md** → Error Handling section

### Test with Postman
→ See: **SWAGGER_DOCUMENTATION.md** → Postman Integration

### Understand the Architecture
→ See: **DOCUMENTATION_SUMMARY.md** → Architecture Overview

---

## 🐛 Troubleshooting

### API won't start
- Check port 5001 is available
- Verify SQL Server connection
- Check `appsettings.json` configuration

### Swagger not loading
- Ensure API is running
- Check URL: `https://localhost:5001/swagger`
- Clear browser cache

### Database errors
- Verify SQL Server is running
- Check connection string
- Run migrations if needed

### Endpoints returning 404
- Verify API is running
- Check endpoint URL spelling
- Review API_CONTRACT.md for correct paths

---

## 📞 Need Help?

### Find Information In:
1. **API Contract** - Technical specifications → **API_CONTRACT.md**
2. **Quick Reference** - Fast lookups → **QUICK_REFERENCE.md**
3. **Swagger Guide** - Setup/integration → **SWAGGER_DOCUMENTATION.md**
4. **Summary** - Learning path → **DOCUMENTATION_SUMMARY.md**
5. **Swagger UI** - Interactive testing → `https://localhost:5001/swagger`

### Check:
- Error message in response
- HTTP status code
- Validation rules in API_CONTRACT.md
- Common issues in QUICK_REFERENCE.md

---

## 🎉 You're All Set!

Your Store Backend API is:
- ✅ **Fully Documented** with 4 markdown guides + Swagger
- ✅ **Production Ready** with error handling and transactions
- ✅ **Well Architected** with layered design
- ✅ **Easy to Extend** with clear structure
- ✅ **Easy to Test** with multiple tools

### Next Steps:
1. Start the API: `dotnet run`
2. Open Swagger: `https://localhost:5001/swagger`
3. Choose your documentation guide
4. Start testing!

---

## 📝 Version Information

- **API Version**: v1.0.0
- **.NET Version**: 8.0
- **Status**: ✅ Production Ready
- **Last Updated**: September 25, 2024

---

## 📜 License

MIT License - See LICENSE file for details

---

## 👥 About Sana-Tech Store

**Store Backend API** - RESTful API for store management with advanced features

**Repository**: `https://github.com/Jedapaji/Store-Backend`

---

**Happy coding! 🚀**

For any questions, refer to the appropriate documentation file above.
