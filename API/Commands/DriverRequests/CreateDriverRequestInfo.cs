using Entites.Dtos.Requests;
using Entites.Dtos.Responses;
using MediatR;

namespace API.Commands.DriverRequests;

public class CreateDriverRequestInfo : IRequest<GetDriverResponse>
{
    public CreateDriverRequest DriverRequest { get; }

    public CreateDriverRequestInfo(CreateDriverRequest driverRequest)
    {
        DriverRequest = driverRequest;
    }
}