# Store Backend API - Swagger Configuration & Documentation

## Overview

The Store Backend API includes comprehensive Swagger/OpenAPI documentation that is automatically generated from the code's XML comments and controller definitions.

---

## Accessing Swagger UI

### Local Development
```
https://localhost:5001/swagger/index.html
```

### OpenAPI JSON Schema
```
https://localhost:5001/swagger/v1/swagger.json
```

---

## Swagger Configuration (Program.cs)

### Current Configuration

The API is configured with the following Swagger settings:

```csharp
builder.Services.AddSwaggerGen(c =>
{
	// API Information
	c.SwaggerDoc("v1", new OpenApiInfo 
	{ 
		Title = "Store Backend API",
		Version = "v1",
		Description = "RESTful API for managing a store's products, categories, customers, and orders with inventory management.",
		Contact = new OpenApiContact
		{
			Name = "Sana-Tech Store",
			Email = "support@sana-tech.com"
		},
		License = new OpenApiLicense
		{
			Name = "MIT License"
		}
	});

	// XML Documentation Comments
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	// Bearer Token Security Scheme (for JWT in future)
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		Description = "JWT Authorization header using the Bearer scheme."
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] { }
		}
	});
});
```

---

## API Information Displayed in Swagger

| Property | Value |
|---|---|
| **Title** | Store Backend API |
| **Version** | v1 |
| **Base Path** | `/api` |
| **Schemes** | HTTPS |
| **Contact Name** | Sana-Tech Store |
| **Contact Email** | support@sana-tech.com |
| **License** | MIT License |

---

## Documented Endpoints

### Products Endpoints (5)
- ✅ GET /api/products - Get all products
- ✅ GET /api/products/{id} - Get product by ID
- ✅ POST /api/products - Create product
- ✅ PUT /api/products/{id} - Update product
- ✅ DELETE /api/products/{id} - Delete product

### Categories Endpoints (5)
- ✅ GET /api/categories - Get all categories
- ✅ GET /api/categories/{id} - Get category by ID
- ✅ POST /api/categories - Create category
- ✅ PUT /api/categories/{id} - Update category
- ✅ DELETE /api/categories/{id} - Delete category

### Customers Endpoints (5)
- ✅ GET /api/customers - Get all customers
- ✅ GET /api/customers/{id} - Get customer by ID
- ✅ POST /api/customers - Create customer
- ✅ PUT /api/customers/{id} - Update customer
- ✅ DELETE /api/customers/{id} - Delete customer

### Orders Endpoints (6)
- ✅ GET /api/orders - Get all orders
- ✅ GET /api/orders/{id} - Get order by ID
- ✅ POST /api/orders - Create order
- ✅ POST /api/orders/CreatePurchaseOrder/{customerId} - Create purchase order
- ✅ PUT /api/orders/{id} - Update order
- ✅ DELETE /api/orders/{id} - Delete order

**Total: 21 Endpoints Documented**

---

## XML Documentation Comments

All endpoints have comprehensive XML documentation in the controller files:

### Example: GetProduct Endpoint
```csharp
/// <summary>
/// Get product by id.
/// </summary>
/// <param name="id">Product Id</param>
/// <response code="200">Ok</response>
/// <response code="404">Not Found</response>
[HttpGet("{id}")]
public async Task<ActionResult<ProductDto>> GetProduct(int id)
{
	var product = await _productService.GetProductByIdAsync(id);
	return Ok(product);
}
```

**Displayed in Swagger as**:
- Summary: "Get product by id."
- Parameter documentation
- Response code descriptions

### Example: CreatePurchaseOrder Endpoint
```csharp
/// <summary>
/// Create a new purchase order with multiple items and manage stock.
/// Handles inventory management and uses database transaction for consistency.
/// </summary>
/// <param name="customerId">Customer identifier</param>
/// <param name="purchaseOrder">Purchase order with items to process</param>
/// <response code="201">Created</response>
/// <response code="400">Bad Request (invalid data, insufficient stock, or product not found)</response>
/// <response code="404">Not Found (customer not found)</response>
/// <response code="500">Internal Server Error</response>
[HttpPost("CreatePurchaseOrder/{customerId}")]
public async Task<ActionResult<OrderDto>> CreatePurchaseOrder(int customerId, [FromBody] PurchaseOrder purchaseOrder)
{
	var order = await _orderService.CreatePurchaseOrderAsync(customerId, purchaseOrder);
	return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
}
```

---

## Data Model Documentation in Swagger

### ProductDto
```csharp
public class ProductDto
{
	/// <summary>
	/// Product identifier.
	/// </summary>
	public int ProductId { get; set; }

	/// <summary>
	/// Product name.
	/// </summary>
	public required string Name { get; set; }

	// ... more properties documented
}
```

**Displayed in Swagger Schema** as:
- Object with typed properties
- Required vs optional fields
- Type information (string, integer, decimal, etc.)
- Field descriptions

---

## Swagger Features Available

### 1. Interactive API Testing
- **Try it out** button on each endpoint
- Enter request parameters
- View request/response details
- Test directly from UI

### 2. Request/Response Examples
- Shows example schemas
- Includes data types
- Displays required fields
- Shows default values

### 3. Authentication Preview
- Bearer token input field (prepared for JWT)
- Security scheme documentation
- Ready for v2 implementation

### 4. Download Options
- JSON format: `/swagger/v1/swagger.json`
- YAML format: Available via Swagger UI
- Import into Postman or other tools

---

## Using Swagger UI

### Step-by-Step Guide

1. **Open Swagger UI**
   ```
   https://localhost:5001/swagger/index.html
   ```

2. **Browse Endpoints**
   - Click on endpoint to expand
   - Read description and parameters
   - Review request/response schemas

3. **Test an Endpoint**
   - Click "Try it out"
   - Enter required parameters
   - Click "Execute"
   - View response

4. **Example: Create a Product**
   - Find POST /api/products
   - Click "Try it out"
   - Enter request body:
   ```json
   {
	 "name": "Test Product",
	 "description": "A test product",
	 "price": 99.99,
	 "stock": 10,
	 "categoryId": 1
   }
   ```
   - Click "Execute"
   - See 201 Created response with product details

---

## Response Models in Swagger

### Success Response
```json
{
  "productId": 1,
  "name": "string",
  "description": "string",
  "price": 0,
  "stock": 0,
  "categoryId": 0
}
```

### Error Response
```json
{
  "statusCode": 0,
  "message": "string",
  "details": "string",
  "timestamp": "2024-09-25T10:30:00.000Z"
}
```

---

## HTTP Status Codes Documented

| Code | Description | Common Causes |
|---|---|---|
| **200** | OK | Successful GET/PUT requests |
| **201** | Created | Successful POST requests |
| **204** | No Content | Successful DELETE requests |
| **400** | Bad Request | Validation errors, missing fields |
| **404** | Not Found | Resource ID doesn't exist |
| **500** | Server Error | Unhandled exceptions |

---

## Postman Integration

### Export from Swagger

1. Open Swagger UI: `https://localhost:5001/swagger/index.html`
2. Copy the JSON URL: `https://localhost:5001/swagger/v1/swagger.json`
3. In Postman:
   - Click "Import"
   - Select "Link"
   - Paste the Swagger JSON URL
   - Click "Import"

### Collections Available
- Products
- Categories
- Customers
- Orders

---

## Security Scheme Configuration

### Bearer Token (Ready for JWT)

The API is configured to support JWT authentication in the future:

```csharp
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
	Name = "Authorization",
	Type = SecuritySchemeType.Http,
	Scheme = "Bearer",
	BearerFormat = "JWT",
	Description = "JWT Authorization header using the Bearer scheme."
});
```

**When enabled in v2**:
- Users will see lock icon on secured endpoints
- Bearer token input field will be active
- All requests will include: `Authorization: Bearer {token}`

---

## Best Practices for API Documentation

### ✅ Do's
- Provide clear endpoint summaries
- Document all parameters
- Include response examples
- List error conditions
- Use consistent naming
- Document required fields
- Provide curl examples (in markdown files)

### ❌ Don'ts
- Expose sensitive information in examples
- Use undefined parameters
- Leave endpoints undocumented
- Mix multiple operations in one endpoint
- Use vague descriptions

---

## Generating Updated Swagger Documentation

### After Code Changes

1. **Add XML Comments** to new endpoints:
   ```csharp
   /// <summary>
   /// Brief description of what the endpoint does.
   /// </summary>
   /// <param name="id">Description of the id parameter</param>
   /// <response code="200">OK - Success response</response>
   /// <response code="404">Not Found - Resource not found</response>
   ```

2. **Build the project**:
   ```bash
   dotnet build
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```

4. **Access Swagger UI**:
   ```
   https://localhost:5001/swagger/index.html
   ```

---

## Troubleshooting Swagger

### Swagger UI Not Loading
- Ensure application is running
- Check URL: `https://localhost:5001/swagger/index.html`
- Verify SSL certificate is trusted

### Endpoints Not Appearing
- Check XML comments are present
- Verify `GenerateDocumentationFile` is true in .csproj
- Rebuild project
- Clear browser cache

### Models Not Showing
- Ensure DTOs have public properties
- Add XML comments to properties
- Verify models are used in endpoints
- Check UseSerializerDefaults configuration

---

## API Documentation Files

The project includes these documentation files:

1. **API_CONTRACT.md**
   - Complete API contract with all endpoints
   - Request/response examples
   - Error scenarios
   - Data models

2. **QUICK_REFERENCE.md**
   - Quick endpoint summary
   - cURL examples
   - Common patterns
   - Testing examples

3. **Swagger UI**
   - Interactive documentation
   - Live API testing
   - Auto-generated from code

---

## Version Information

| Component | Version |
|---|---|
| .NET | 8.0 |
| Entity Framework Core | 8.0.8 |
| Swashbuckle.AspNetCore | 6.4.0 |
| OpenAPI Specification | 3.0.1 |

---

## Future Enhancements

Planned for v2:
- ✅ JWT Authentication with Bearer tokens
- ✅ API versioning (v1, v2, etc.)
- ✅ Rate limiting
- ✅ Request/Response logging
- ✅ Advanced filtering and pagination
- ✅ GraphQL endpoint
- ✅ WebSocket support for real-time updates

---

## Conclusion

The Store Backend API includes comprehensive Swagger/OpenAPI documentation that makes it easy for developers to:
- Discover available endpoints
- Understand request/response formats
- Test API functionality interactively
- Generate client code
- Integrate with other systems

**Access it now**: `https://localhost:5001/swagger/index.html`

---

**Last Updated**: September 25, 2024  
**Version**: v1.0.0  
**Status**: ✅ Production Ready
