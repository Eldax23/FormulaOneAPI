using API.Commands.DriverRequests;
using API.Queries.DriversQueries;
using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Requests;
using Entites.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : BaseController
{

    public DriverController(IUnitOfWork unitOfWork, IMapper mapper , IMediator mediator) : base(unitOfWork, mapper , mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDrivers()
    {
        // Specify the query that I have for this endpoint
        var query = new GetAllDriversQuery();
        
        // the MediatR recognized the right handler based on the GetlAllDriversQuery which 
        // we specified in the GetAllDriversHandler , yea i guess MediatR is smart enough to do that
        var result = await _mediator.Send(query);
        return Ok(result);

    }
    
    // Get All Achievements for a driver

    [HttpGet("{driverId:guid}/achievements")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId)
    {
        IEnumerable<Achievement>? driverAchievements = await _unitOfWork.Achievements.GetAchievemntsForDriverAsync(driverId);
        
        if(driverAchievements == null)
            return NotFound("Achievements Not Found");

        IEnumerable<DriverAchievementResponse> res = _mapper.Map<IEnumerable<DriverAchievementResponse>>(driverAchievements);
        return Ok(res);
    }

    [HttpGet("{driverId:guid}")]
    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        var query = new GetDriverQuery(driverId);
        GetDriverResponse? result = await _mediator.Send(query);
        if(result == null)
            return NotFound("Driver Not Found");
        return Ok(result);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddDriver(CreateDriverRequest driver)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        CreateDriverRequestInfo request = new  CreateDriverRequestInfo(driver);
        GetDriverResponse? res = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetDriver), new { driverId = res.DriverId }, res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDriver(UpdateDriverRequest driver)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        UpdateDriverInfoRequest command = new UpdateDriverInfoRequest(driver);
        bool res = await _mediator.Send(command);
        return res ?  NoContent() : BadRequest(); 
    }

    [HttpDelete("{driverId:guid}")]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        DeleteDriverRequestInfo command = new DeleteDriverRequestInfo(driverId);
        bool res = await _mediator.Send(command);
        return res ? NoContent() : BadRequest();
    }
}