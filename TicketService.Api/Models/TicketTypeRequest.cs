using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TicketService.Api.Models.Enums;
using TicketService.Api.Models.Exceptions;

namespace TicketService.Api.Models;

public class TicketTypeRequest
{
    private const int _adultTicketPrice = 20; // stored here a TicketType is Enum rather than its own class
    private const int _childTicketPrice = 10; // stored here a TicketType is Enum rather than its own class
    private const int _infantTicketPrice = 0; // stored here a TicketType is Enum rather than its own class

    [Range(0, 20)]
    public int NumberOfTickets { get; init; }

    public TicketType TicketType { get; init; }

    [JsonIgnore]
    public int Price { get; init; }

    [JsonIgnore]
    public int Seats { get; init; }


    public TicketTypeRequest(TicketType ticketType, int numberOfTickets)
    {
        TicketType = ticketType;
        NumberOfTickets = numberOfTickets;
        Price = CalculatePrice();
        Seats = CalculateSeats();
    }


    /// <summary>
    /// Calculates the price of the ticket as an Integer.
    /// Normally decimals used for monetary values but the TicketPaymentService accepts Int.
    /// </summary>
    /// <param name="ticketType">The type of ticket being purchased.</param>
    /// <returns>The price of the ticket as an Integer.</returns>
    /// <exception cref="InvalidPurchaseException"></exception>
    private int CalculatePrice()
    {
        return TicketType switch
        {
            TicketType.Adult => _adultTicketPrice * NumberOfTickets,
            TicketType.Child => _childTicketPrice * NumberOfTickets,
            TicketType.Infant => _infantTicketPrice * NumberOfTickets,
            _ => throw new InvalidPurchaseException("Unknown ticket type.", 400),
        };
    }

    /// <summary>
    /// Calculates the number of seats required for this ticket.
    /// </summary>
    /// <param name="ticketType">The type of ticket being purchased.</param>
    /// <returns>The number of seats required for this ticket.</returns>
    /// <exception cref="InvalidPurchaseException"></exception>
    private int CalculateSeats()
    {
        return TicketType switch
        {
            TicketType.Adult => 1 * NumberOfTickets,
            TicketType.Child => 1 * NumberOfTickets,
            TicketType.Infant => 0,
            _ => throw new InvalidPurchaseException("Unknown ticket type.", 400),
        };
    }
}
