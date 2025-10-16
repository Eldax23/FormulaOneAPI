using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Requests;
using Entites.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AchievementController : BaseController
{
    public AchievementController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet("{driverId:guid}")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId)
    {
        IEnumerable<Achievement>? driverAchievements = await _unitOfWork.Achievements.GetAllAsync();
        
        if(driverAchievements == null)
            return NotFound("Achievements Not Found");

        var res = _mapper.Map<DriverAchievementResponse>(driverAchievements);
        
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> AddAchievement([FromBody]CreateDriverAchievementRequest achievemnt)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Achievement res = _mapper.Map<Achievement>(achievemnt);

        await _unitOfWork.Achievements.AddAsync(res);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetDriverAchievements), new { id = res.DriverId }, res);
        
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAchievement(UpdateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        Achievement res = _mapper.Map<Achievement>(achievement);

        await _unitOfWork.Achievements.UpdateAsync(res);
        await _unitOfWork.CompleteAsync();
        
        return NoContent();
    }
}