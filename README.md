# Week-2-Cohorts-Assigment

The API developed in the first week must be used. ✔️

The API must be a RESTful API ✔️

The project must adhere to SOLID principles. ✔️

Fake services must be developed, Dependency Injection must be used. ✔️

Service
```
Api/Service/GraphicCardShowService
```
Dependency Injection has been applied in Controller
```
Api/Controllers/GraphicCardApiController.cs
```

Example:
```
public class GraphicCardApiController : ControllerBase
{
    private readonly IGraphicCardShowService _graphicCardService;

    public GraphicCardApiController(IGraphicCardShowService graphicCardService)
    {
        _graphicCardService = graphicCardService;
    }
```

Developed an extension to be used in your API. ✔️
```
Api/Extension/ServiceCollectionExtension.cs
```

Swagger must be implemented in the project. ✔️

A global logging middleware must be implemented. (at a very basic level, like logging when entering an action) ✔️
```
Api/Middlewares/CustomMiddleware.cs
```


