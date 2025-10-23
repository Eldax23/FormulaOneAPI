using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Requests;
using Entites.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AchievementController : BaseController
{
    public AchievementController(IUnitOfWork unitOfWork, IMapper mapper , IMediator mediator) : base(unitOfWork, mapper , mediator)
    {
    }


    [HttpGet("{achievementId:guid}")]
    public async Task<IActionResult> GetAchievement(Guid achievementId)
    {
        Achievement? entity = await _unitOfWork.Achievements.GetAchievementById(achievementId);
        if (entity == null)
            return NotFound("No Achievement Found");
        
        DriverAchievementResponse response = _mapper.Map<DriverAchievementResponse>(entity);
        return Ok(response);
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
        return CreatedAtAction(nameof(GetAchievement), new { achievementId = res.DriverId }, res);
        
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