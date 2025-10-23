using API.Queries.DriversQueries;
using AutoMapper;
using DataService.Repositories;
using Entites;
using Entites.Dtos.Responses;
using MediatR;

namespace API.Controllers.Handlers.DriverHandlers;

// the handler handles all the query , response , logic
public class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery , IEnumerable<GetDriverResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllDriversHandler(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetDriverResponse>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Driver> drivers = await _unitOfWork.Drivers.GetAllAsync();
        IEnumerable<GetDriverResponse> res = _mapper.Map<IEnumerable<GetDriverResponse>>(drivers);
        return res;
    }
}