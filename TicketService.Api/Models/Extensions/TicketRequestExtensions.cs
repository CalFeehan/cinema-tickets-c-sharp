using TicketService.Api.Models.Enums;

namespace TicketService.Api.Models.Extensions;

public static class TicketRequestExtensions
{
    /// <summary>
    /// Sum the number of tickets for a list of TicketTypeRequest's.
    /// </summary>
    /// <param name="tickets"></param>
    /// <param name="types"></param>
    /// <returns>Number of tickets purchased.</returns>
    public static int SumOfType(this IEnumerable<TicketTypeRequest> tickets, IEnumerable<TicketType> types)
        => tickets.Where(x => types.Contains(x.TicketType)).Sum(x => x.NumberOfTickets);

    /// <summary>
    /// Sum the number of tickets for a single TicketTypeRequest.
    /// </summary>
    /// <param name="tickets"></param>
    /// <param name="types"></param>
    /// <returns>Number of tickets purchased.</returns>
    public static int SumOfType(this IEnumerable<TicketTypeRequest> tickets, TicketType type)
        => tickets.Where(x => x.TicketType == type).Sum(x => x.NumberOfTickets);
}
