using API.Commands.DriverRequests;
using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Requests;
using MediatR;

namespace API.Controllers.Handlers.DriverHandlers;

public class UpdateDriverHandler(IUnitOfWork unitOfWork , IMapper mapper) : IRequestHandler<UpdateDriverInfoRequest , bool>
{
    public async Task<bool> Handle(UpdateDriverInfoRequest request, CancellationToken cancellationToken)
    {
        Driver? entity = mapper.Map<Driver>(request.Driver);
        await unitOfWork.Drivers.UpdateAsync(entity);
        await unitOfWork.CompleteAsync();
        return true;
        
        
    }
}