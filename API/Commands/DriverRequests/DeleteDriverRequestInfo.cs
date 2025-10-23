using MediatR;

namespace API.Commands.DriverRequests;

public class DeleteDriverRequestInfo : IRequest<bool>
{
    public Guid DriverId { get;  }

    public DeleteDriverRequestInfo(Guid driverId)
    {
        DriverId = driverId;
    }
}