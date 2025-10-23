using Entites.Dtos.Requests;
using MediatR;

namespace API.Commands.DriverRequests;

public class UpdateDriverInfoRequest : IRequest<bool>
{
    public UpdateDriverRequest Driver { get; }

    public UpdateDriverInfoRequest(UpdateDriverRequest driver)
    {
        Driver = driver;
    }
}