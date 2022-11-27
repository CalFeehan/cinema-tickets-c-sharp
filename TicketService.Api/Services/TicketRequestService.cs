using TicketService.Api.Models;
using TicketService.Api.Models.Enums;
using TicketService.Api.Models.Exceptions;
using TicketService.Api.Models.Extensions;

namespace TicketService.Api.Business;

public class TicketRequestService : ITicketRequestService
{
    private const int _minTickets = 1;
    private const int _maxTickets = 20;

    /// <summary>
    /// Performs validation on a list of TicketTypeRequests.
    /// </summary>
    /// <param name="accountId">User Account Id.</param>
    /// <param name="tickets">List of TicketTypeRequests to validate.</param>
    /// <exception cref="InvalidPurchaseException"></exception>
    public void Validate(long accountId, IEnumerable<TicketTypeRequest> tickets)
    {
        if (accountId <= 0)
        {
            throw new InvalidPurchaseException("Invalid Account Id.", 400);
        }

        var numberOfTickets = tickets.Sum(x => x.NumberOfTickets);
        if (numberOfTickets > _maxTickets)
        {
            throw new InvalidPurchaseException($"You must request at most {_maxTickets} {(_maxTickets == 1 ? "ticket" : "tickets")}.", 400);
        }
        if (numberOfTickets < _minTickets)
        {
            throw new InvalidPurchaseException($"You must request at least {_minTickets} {(_minTickets == 1 ? "ticket" : "tickets")}.", 400);
        }

        // Child or Infant ticket without an accompanying Adult
        if (tickets.SumOfType(TicketType.Adult) < 1) // no Adult tickets
        {
            if (tickets.SumOfType(new List<TicketType> { TicketType.Child, TicketType.Infant }) > 0) // if there are Child/Infant tickets
            {
                throw new InvalidPurchaseException("Child and Infant tickets must be purchased with at least one Adult ticket.", 400);
            }
        }
    }
}
