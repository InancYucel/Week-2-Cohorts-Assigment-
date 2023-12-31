using WonderFulGraphicCards_API.Data;
using WonderFulGraphicCards_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WonderFulGraphicCards_API.Interface;

namespace WonderFulGraphicCards_API.Controllers;

[Route("api/GraphicCardApi")] //[Route("api/[controller]")]
[ApiController]
public class GraphicCardApiController : ControllerBase
{
    private readonly IGraphicCardShowService _graphicCardService;

    public GraphicCardApiController(IGraphicCardShowService graphicCardService)
    {
        _graphicCardService = graphicCardService;
    }
    
    [HttpGet] //Endpoint
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<GraphicCardDto>> GetGraphicCards() //multiple records
    {
        return Ok(GraphicCardStore.GraphicsCardList);
    }
    
    [HttpGet("{id:int}", Name="GetGraphicCard")] //Endpoint //to know which resource was created
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GraphicCardDto> GetGraphicCard(int id) //one record
    {
        if (id == 0) //bad request
        {
            return BadRequest(); //Return 400 = undocumented
        }

        var graphicCard = _graphicCardService.FindGraphicCardWithId(id); //Dependency Injection Used
        if (graphicCard == null)
        {
            return NotFound(); //Return 404
        }
        return Ok(graphicCard);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<GraphicCardDto> CreateGraphicCard([FromBody]GraphicCardDto graphicCardDto)
    {
        /*if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }*/
        if (GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Name.ToLower() == graphicCardDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Graphic Card already exists");
            return BadRequest(ModelState);
        }
        if (graphicCardDto == null)
        {
            return BadRequest(graphicCardDto);
        }

        if (graphicCardDto.Id > 0) //When creating a graphicCard the id must be 0
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        graphicCardDto.Id = GraphicCardStore.GraphicsCardList.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1; //I want last Id + 1
        GraphicCardStore.GraphicsCardList.Add(graphicCardDto);

        return CreatedAtRoute("GetGraphicCard", new { id = graphicCardDto.Id }, graphicCardDto); //"GetGraphicCard" to know which resource was created
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteGraphicCard")]
    public IActionResult DeleteGraphicCard(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        //We fetch the matching record
        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return NotFound();
        }

        GraphicCardStore.GraphicsCardList.Remove(graphicCard);
        return NoContent(); //The transaction is successful but we do not give any feedback
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id:int}", Name = "UpdateGraphicCard")]
    public IActionResult UpdateGraphicCard(int id, [FromBody] GraphicCardDto graphicCardDto)
    {
        if (graphicCardDto == null || id != graphicCardDto.Id) //
        {
            return BadRequest(); //If the searched ID does not exist
        }
        //We fetch the matching record
        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard != null)
        {
            graphicCard.Name = graphicCardDto.Name;
            graphicCard.CudaCores = graphicCardDto.CudaCores;
            graphicCard.CoreClockSpeed = graphicCardDto.CoreClockSpeed;
            return NoContent(); //The transaction is successful but we do not give any feedback
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialGraphicCard")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePartialGraphicCard(int id, JsonPatchDocument<GraphicCardDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }
        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return BadRequest();
        }
        patchDto.ApplyTo(graphicCard, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent(); //The transaction is successful but we do not give any feedback
    }
    
    [HttpGet("GetIdByQuery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GraphicCardDto> GetGraphicCardWithIdFromQuery([FromQuery] int id)
    {
        if (id == 0) //bad request
        {
            return BadRequest(); //Return 400 = undocumented
        }

        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return NotFound(); //Return 404
        }
        return Ok(graphicCard);
    }
    
    [HttpGet("GetIdByRoute/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GraphicCardDto> GetGraphicCardWithIdFromRoute(int id)
    {
        if (id == 0) //bad request
        {
            return BadRequest(); //Return 400 = undocumented
        }

        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return NotFound(); //Return 404
        }
        return Ok(graphicCard);
    }
    
    [HttpPost("PostGraphicCardByQuery")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<GraphicCardDto> CreateGraphicCardByQuery([FromQuery] string name, int coreClockSpeed, int cudaCores)
    {
        if (GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Graphic Card already exists");
            return BadRequest(ModelState);
        }
        
        var graphicCardDto = new GraphicCardDto
        {
            Id = 0,
            Name = name,
            CoreClockSpeed = coreClockSpeed,
            CudaCores = cudaCores
        };
        
        if (graphicCardDto == null)
        {
            return BadRequest(graphicCardDto);
        }

        if (graphicCardDto.Id > 0) //When creating a graphicCard the id must be 0
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        graphicCardDto.Id = GraphicCardStore.GraphicsCardList.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1; //I want last Id + 1
        GraphicCardStore.GraphicsCardList.Add(graphicCardDto);
        return CreatedAtRoute("GetGraphicCard", new { id = graphicCardDto.Id }, graphicCardDto); //"GetGraphicCard" to know which resource was created
    }
    
    [HttpPost("PostGraphicCardByRouteParameter/{name}/{coreClockSpeed}/{cudaCores}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<GraphicCardDto> CreateGraphicCardByRouteParameter(string name, int coreClockSpeed, int cudaCores)
    {
        if (GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Graphic Card already exists");
            return BadRequest(ModelState);
        }
        
        var graphicCardDto = new GraphicCardDto
        {
            Id = 0,
            Name = name,
            CoreClockSpeed = coreClockSpeed,
            CudaCores = cudaCores
        };
        
        if (graphicCardDto == null)
        {
            return BadRequest(graphicCardDto);
        }

        if (graphicCardDto.Id > 0) //When creating a graphicCard the id must be 0
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        graphicCardDto.Id = GraphicCardStore.GraphicsCardList.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1; //I want last Id + 1
        GraphicCardStore.GraphicsCardList.Add(graphicCardDto);

        return CreatedAtRoute("GetGraphicCard", new { id = graphicCardDto.Id }, graphicCardDto); //"GetGraphicCard" to know which resource was created
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("DeleteGetGraphicCardByIdWithQuery")]
    public IActionResult DeleteGetGraphicCardByIdWithQuery([FromQuery] int id)
    {
        if (id == 0) //There is no data with id 0
        {
            return BadRequest();
        }

        var graphicCardDto = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCardDto == null)
        {
            return NotFound();
        }

        GraphicCardStore.GraphicsCardList.Remove(graphicCardDto);
        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("DeleteGraphicCardByIdWithRouteParameter/{id}")]
    public IActionResult DeleteGraphicCardByIdWithRouteParameter(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return NotFound();
        }

        GraphicCardStore.GraphicsCardList.Remove(graphicCard);
        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("UpdateGraphicCardByIdWithQuery")]
    public IActionResult UpdateGraphicCardByIdWithQuery([FromQuery] int id, string name, int coreClockSpeed, int cudaCores)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return NotFound();
        }
        
        graphicCard.Name = name;
        graphicCard.CudaCores = coreClockSpeed;
        graphicCard.CoreClockSpeed = cudaCores;

        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("UpdateGraphicCardByIdWithRouteParameter/{id}/{name}/{coreClockSpeed}/{cudaCores}")]
    public IActionResult UpdateGraphicCardByIdWithRouteParameter(int id, string name, int coreClockSpeed, int cudaCores)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var graphicCard = GraphicCardStore.GraphicsCardList.FirstOrDefault(x => x.Id == id);
        if (graphicCard == null)
        {
            return NotFound();
        }
        
        graphicCard.Name = name;
        graphicCard.CudaCores = coreClockSpeed;
        graphicCard.CoreClockSpeed = cudaCores;

        return NoContent();
    }
    
    [HttpGet("List/")]
    public ActionResult<GraphicCardDto> List([FromQuery] string name)
    {
        //We bring all the data
        var graphicCard = GraphicCardStore.GraphicsCardList.FindAll(x => x.Name.Contains(name));
        if (graphicCard == null)
        {
            return NotFound(); //Return 404
        }
        return Ok(graphicCard);
    }
    
    [HttpGet("Sorting/")]
    public ActionResult<GraphicCardDto> Sorting([FromQuery] string sortBy)
    {
        var graphicCard = (IOrderedEnumerable<GraphicCardDto>?)null;
        switch (sortBy) //We sort from smallest to largest according to the string entered in the query.
        {
            case "Name":
                graphicCard = GraphicCardStore.GraphicsCardList.OrderBy(p => p.Name);
                break;
                
            case "CudaCores":
                graphicCard = GraphicCardStore.GraphicsCardList.OrderBy(p => p.CudaCores);    
                break;
            
            case "CoreClockSpeed":
                graphicCard = GraphicCardStore.GraphicsCardList.OrderBy(p => p.CoreClockSpeed);
                break;
        }

        if (graphicCard == null)
        {
            return NotFound(); //Return 404
        }
        return Ok(graphicCard);
    }
}
