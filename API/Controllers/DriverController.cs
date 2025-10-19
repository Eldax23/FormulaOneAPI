using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Requests;
using Entites.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : BaseController
{
    public DriverController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDrivers()
    {
        IEnumerable<Driver?> drivers = await _unitOfWork.Drivers.GetAllAsync();
        if (drivers.IsNullOrEmpty())
        {
            return NotFound("No Drivers Found");
        }
        IEnumerable<GetDriverResponse> res = _mapper.Map<IEnumerable<GetDriverResponse>>(drivers);
        return Ok(res);
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
        Driver? driver = await _unitOfWork.Drivers.GetByIdAsync(driverId);
        if(driver == null)
            return NotFound("Driver not found");
        
        GetDriverResponse res = _mapper.Map<GetDriverResponse>(driver);
        return Ok(res);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddDriver(CreateDriverRequest driver)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Driver entity = _mapper.Map<Driver>(driver);
        await _unitOfWork.Drivers.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
        return Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDriver(UpdateDriverRequest driver)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Driver? entity = _mapper.Map<Driver>(driver);
        await _unitOfWork.Drivers.UpdateAsync(entity);
        await _unitOfWork.CompleteAsync();
        return NoContent(); 
    }
}