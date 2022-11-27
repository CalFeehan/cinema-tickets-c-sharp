using TicketService.Api.Models;

namespace TicketService.Api.Business;

public interface ITicketRequestService
{
    public void Validate(long accountId, IEnumerable<TicketTypeRequest> tickets);
}