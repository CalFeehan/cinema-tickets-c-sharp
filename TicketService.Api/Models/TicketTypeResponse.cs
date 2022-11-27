using TicketService.Api.Models.Enums;

namespace TicketService.Api.Models;

public class TicketTypeResponse
{
    public int NumberOfTickets { get; init; }

    public TicketType TicketType { get; init; }

    public int Price { get; init; }

    public int Seats { get; init; }


    public TicketTypeResponse(TicketTypeRequest ticketTypeRequest)
    {
        TicketType = ticketTypeRequest.TicketType;
        NumberOfTickets = ticketTypeRequest.NumberOfTickets;
        Price = ticketTypeRequest.Price;
        Seats = ticketTypeRequest.Seats;
    }
}
