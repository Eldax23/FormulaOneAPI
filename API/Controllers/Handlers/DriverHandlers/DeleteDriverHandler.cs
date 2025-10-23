using API.Commands.DriverRequests;
using AutoMapper;
using DataService.Repositories;
using Entites;
using MediatR;

namespace API.Controllers.Handlers.DriverHandlers;

public class DeleteDriverHandler(IUnitOfWork unitOfWork , IMapper mapper) : IRequestHandler<DeleteDriverRequestInfo , bool>
{
    public async Task<bool> Handle(DeleteDriverRequestInfo request, CancellationToken cancellationToken)
    {
        Driver? driver = await unitOfWork.Drivers.GetByIdAsync(request.DriverId);
        if (driver == null)
            return false;
        
        await unitOfWork.Drivers.DeleteAsync(request.DriverId);
        await unitOfWork.CompleteAsync();
        return true;
    }
}