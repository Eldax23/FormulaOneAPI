using Entites.Dtos.Responses;
using MediatR;

namespace API.Queries.DriversQueries;

public class GetDriverQuery : IRequest<GetDriverResponse>
{
    public Guid DriverId { get; set; }

    public GetDriverQuery(Guid driverId)
    {
        DriverId =  driverId;
    }
}