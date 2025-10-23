using Entites;
using Entites.Dtos.Responses;
using MediatR;

namespace API.Queries.DriversQueries;

public class GetAllDriversQuery : IRequest<IEnumerable<GetDriverResponse>>
{
    
}