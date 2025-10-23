using API.Queries.DriversQueries;
using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Responses;
using MediatR;

namespace API.Controllers.Handlers.DriverHandlers;

public class GetDriverHandler : IRequestHandler<GetDriverQuery , GetDriverResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverHandler(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<GetDriverResponse> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        Driver? driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId);
        GetDriverResponse res = _mapper.Map<GetDriverResponse>(driver);
        return driver == null ? null : res;
    }
}