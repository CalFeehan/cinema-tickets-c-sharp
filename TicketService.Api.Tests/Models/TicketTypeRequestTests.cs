using TicketService.Api.Models.Enums;

namespace TicketService.Api.Tests.Models;

[TestClass]
public class TicketTypeRequestTests
{
    [TestMethod]
    [DataRow(TicketType.Adult, 0, 0)]    // 0 tickets - £0
    [DataRow(TicketType.Adult, 1, 20)]   // 1 adult ticket - £20
    [DataRow(TicketType.Child, 1, 10)]   // 1 child ticket - £10
    [DataRow(TicketType.Infant, 1, 0)]   // 1 infant ticket - £0
    [DataRow(TicketType.Adult, 10, 200)] // 10 adult tickets - £200
    public void CreateTicketTypeRequest_ShouldCorrectlyCalculatePrice(TicketType ticketType, int numberOfTickets, int expectedResult)
    {
        // arrange

        // act
        var ticketTypeRequest = new TicketTypeRequest(ticketType, numberOfTickets);
        var result = ticketTypeRequest.Price;

        // assert
        result.Should().Be(expectedResult);
    }

    [TestMethod]
    [DataRow(TicketType.Adult, 0, 0)]   // 0 tickets - 0 seats
    [DataRow(TicketType.Adult, 1, 1)]   // 1 adult ticket - 1 seat
    [DataRow(TicketType.Child, 1, 1)]   // 1 child ticket - 1 seat
    [DataRow(TicketType.Infant, 1, 0)]  // 1 infant ticket - 0 seats
    [DataRow(TicketType.Adult, 10, 10)] // 10 adult tickets - 10 seats
    public void CreateTicketTypeRequest_ShouldCorrectlyCalculateSeats(TicketType ticketType, int numberOfTickets, int expectedResult)
    {
        // arrange

        // act
        var ticketTypeRequest = new TicketTypeRequest(ticketType, numberOfTickets);
        var result = ticketTypeRequest.Seats;

        // assert
        result.Should().Be(expectedResult);
    }


}
