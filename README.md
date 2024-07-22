# ASP.NET Core API Restful with React

## Project Overview

This project is a RESTful API developed using ASP.NET Core, designed to showcase best practices in building scalable web APIs. It features a MySQL database, utilizes ASP.NET Versioning, Evolve for database migrations, and Serilog for logging. This API is fully equipped with Swagger for API documentation, supports XML format, HATEOAS, CORS, and JWT for authentication. Enhancements such as GitHub Actions integration, and multi-database compatibility are currently in progress.

### Key Features:

- **Database Integration**: MySQL v5 with Evolve for migration management.
- **Error Logging**: Integrated Serilog for sophisticated runtime logging.
- **API Documentation**: Swagger UI for API exploration and testing at `http://localhost:44300/swagger/index.html`.
- **Versioning**: Endpoint version management to support backward compatibility.
- **Security**: Implements JWT-based authentication to secure endpoints.
- **Advanced Query Capabilities**: Supports query parameters and paged search for effective data fetching.
- **File Handling**: Handling file uploads and downloads.
- **HATEOAS**: Implements HATEOAS pattern using the HATEOAS-NuGet package created by Everton David.


## Technologies

- **ASP.NET Core**: For creating server-side logic
- **Entity Framework**: ORM used for database operations
- **MySQL**: Database management
- **ASP.NET Versioning.Mvc**: For API version control
- **Serilog**: For logging
- **Swagger**: API documentation and exploration tool
- **React.js**: Frontend framework
- **HATEOAS-NuGet**: For implementing HATEOAS pattern

## Architecture

This project follows a layered architecture:

- **Client**: Frontend consuming the API
- **Controller**: Handles incoming HTTP requests and responses
- **Business**: Business logic implementation
- **Repository**: Data access layer using generic repositories
- **Model**: Data models and entities

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- .NET 8 SDK
- MySQL Server
- Node.js and npm (for React.js development)

### How to Run the Project

1. **Clone the repository:**
   ```bash
   git clone https://github.com/evertondavid/RESTful-With-AspNet-8-and-React.git

2. **Start the application:**
   Copy code: dotnet run

   Open your browser and navigate to http://localhost:44300/swagger/index.html to view the application.

3. **Use HATEOAS.library (by Everton David):**
> ## How to use

>### 1 - Import HATEOAS-NuGet to your projetct
#### Import with command line
```bash
Install-Package HATEOAS -Version 8.0.302.6
```

#### Import with nuget package manager

![Nuget Package Mannager](https://github.com/evertondavid/HATEOAS-NuGet/blob/main/image/nuget_package_mannager.png?raw=true)

>### 2 - Implements *ISupportsHyperMedia* in your exposed object.

```csharp
namespace RESTFulSampleServer.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
```

>### 3 - Implements your enricher with *ContentResponseEnricher<T>*.

```csharp
namespace RESTFulSampleServer.HyperMedia.Enricher
{
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/book";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return Task.CompletedTask;
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (this)
            {
                var url = new { controller = path, id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            };
        }
    }
}
```

>### 4 - Add annotation *[TypeFilter(typeof(HyperMediaFilter))]* to your controller methods.

```csharp
namespace RESTFulSampleServer.Controllers
{

    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;

        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
		
		// Adds HyperMedia filter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
		
		// Adds HyperMedia filter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
		
		// Adds HyperMedia filter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
		
		// Adds HyperMedia filter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}

```

>### 5 - Add *HyperMediaFilterOptions* to your Program.cs.

```csharp
	var filterOptions = new HyperMediaFilterOptions();
	filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
	filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
```

>### 6 - Add a *MapControllerRoute* to your route like was defined in your enricher.

```csharp
	app.MapControllers();
	* app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}"); *

	app.Run();
```
>### 7 - Enjoy

#### Response as JSON

```json
[
    {
        "id": 1,
        "title": "Working effectively with legacy code",
        "author": "Michael C. Feathers",
        "price": 49.00,
        "launchDate": "2017-11-29T13:50:05.878",
        "links": [
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/1",
                "type": "application/json",
                "action": "GET"
            },
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/1",
                "type": "application/json",
                "action": "POST"
            },
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/1",
                "type": "application/json",
                "action": "PUT"
            },
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/1",
                "type": "int",
                "action": "DELETE"
            }
        ]
    },
    {
        "id": 2,
        "title": "Design Patterns",
        "author": "Ralph Johnson, Erich Gamma, John Vlissides e Richard Helm",
        "price": 45.00,
        "launchDate": "2017-11-29T15:15:13.636",
        "links": [
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/2",
                "type": "application/json",
                "action": "GET"
            },
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/2",
                "type": "application/json",
                "action": "POST"
            },
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/2",
                "type": "application/json",
                "action": "PUT"
            },
            {
                "rel": "self",
                "href": "https://localhost:44300/api/book/v1/2",
                "type": "int",
                "action": "DELETE"
            }
        ]
    }
]
```

#### Response as XML

```xml
<ArrayOfBookVO xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <BookVO>
        <Id>1</Id>
        <Title>Working effectively with legacy code</Title>
        <Author>Michael C. Feathers</Author>
        <Price>49.00</Price>
        <LaunchDate>2017-11-29T13:50:05.878</LaunchDate>
        <Links>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/1</Href>
                <Type>application/json</Type>
                <Action>GET</Action>
            </HyperMediaLink>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/1</Href>
                <Type>application/json</Type>
                <Action>POST</Action>
            </HyperMediaLink>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/1</Href>
                <Type>application/json</Type>
                <Action>PUT</Action>
            </HyperMediaLink>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/1</Href>
                <Type>int</Type>
                <Action>DELETE</Action>
            </HyperMediaLink>
        </Links>
    </BookVO>
    <BookVO>
        <Id>2</Id>
        <Title>Design Patterns</Title>
        <Author>Ralph Johnson, Erich Gamma, John Vlissides e Richard Helm</Author>
        <Price>45.00</Price>
        <LaunchDate>2017-11-29T15:15:13.636</LaunchDate>
        <Links>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/2</Href>
                <Type>application/json</Type>
                <Action>GET</Action>
            </HyperMediaLink>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/2</Href>
                <Type>application/json</Type>
                <Action>POST</Action>
            </HyperMediaLink>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/2</Href>
                <Type>application/json</Type>
                <Action>PUT</Action>
            </HyperMediaLink>
            <HyperMediaLink>
                <Rel>self</Rel>
                <Href>https://localhost:44300/api/book/v1/2</Href>
                <Type>int</Type>
                <Action>DELETE</Action>
            </HyperMediaLink>
        </Links>
    </BookVO>
</ArrayOfBookVO>
```

>### Suggestions are welcome. Feel free to sugest improvements.

### Contributing
   Contributions are welcome! Please fork this repository and submit pull requests with any improvements.

### License
   This project is licensed under the MIT License - see the LICENSE file for details.

