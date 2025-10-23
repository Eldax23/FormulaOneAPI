using API.Commands.DriverRequests;
using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Responses;
using MediatR;

namespace API.Controllers.Handlers.DriverHandlers;

public class AddDriverHandler(IUnitOfWork unitOfWork , IMapper mapper) : IRequestHandler<CreateDriverRequestInfo , GetDriverResponse>
{
    public async Task<GetDriverResponse> Handle(CreateDriverRequestInfo requestInfo, CancellationToken cancellationToken)
    {
        Driver driver = mapper.Map<Driver>(requestInfo.DriverRequest);
        await unitOfWork.Drivers.AddAsync(driver);
        await unitOfWork.CompleteAsync();
        
        return mapper.Map<GetDriverResponse>(driver);
    }
}