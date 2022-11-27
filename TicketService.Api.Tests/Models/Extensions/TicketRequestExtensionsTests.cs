using TicketService.Api.Models.Enums;
using TicketService.Api.Models.Extensions;

namespace TicketService.Api.Tests.Models.Extensions;

[TestClass]
public class TicketRequestExtensionsTests
{
    public IEnumerable<TicketTypeRequest> _tickets;

    [TestMethod]
    [DataRow(0, 0, 0, 0)]   // 0 Adult tickets
    [DataRow(1, 0, 0, 1)]   // 1 Adult ticket
    [DataRow(1, 1, 1, 1)]   // 1 Adult tickets
    [DataRow(20, 7, 6, 20)] // 20 Adult tickets
    public void SumOfType_ShouldGiveCorrectSum_GivenOneTypeOfAdult(int adultTickets, int childTickets, int infantTickets, int expectedResult)
    {
        // arrange - not needed as data is clean
        _tickets = new List<TicketTypeRequest>
        {
            new(TicketType.Adult, adultTickets),
            new(TicketType.Child, childTickets),
            new(TicketType.Infant, infantTickets)
        };

        // act
        var result = _tickets.SumOfType(TicketType.Adult);

        // assert
        result.Should().Be(expectedResult);
    }

    [TestMethod]
    [DataRow(0, 0, 0, 0)]    // 0 tickets
    [DataRow(1, 0, 0, 1)]    // 1 ticket
    [DataRow(1, 1, 0, 2)]    // 2 tickets
    [DataRow(1, 1, 1, 3)]    // 3 tickets
    [DataRow(20, 10, 5, 35)] // 20 Adult tickets
    public void SumOfType_ShouldGiveCorrectSum_GivenMultipleTypes(int adultTickets, int childTickets, int infantTickets, int expectedResult)
    {
        // arrange - not needed as data is clean
        _tickets = new List<TicketTypeRequest>
        {
            new(TicketType.Adult, adultTickets),
            new(TicketType.Child, childTickets),
            new(TicketType.Infant, infantTickets)
        };

        // act
        var typesToCheck = new List<TicketType> { TicketType.Adult, TicketType.Child, TicketType.Infant };
        var result = _tickets.SumOfType(typesToCheck);

        // assert
        result.Should().Be(expectedResult);
    }

    #region Setup

    [TestInitialize]
    public void Setup()
    {
        SetupData();
        SetupMocks();
        SetupExpectations();
        SetupServiceUnderTest();
    }

    private void SetupServiceUnderTest()
    {
        // None needed
    }

    private void SetupExpectations()
    {
        // None needed
    }

    private void SetupMocks()
    {
        // None needed
    }

    private void SetupData()
    {
        _tickets = new List<TicketTypeRequest>
        {
            new(TicketType.Adult, 10),
            new(TicketType.Child, 5),
            new(TicketType.Infant, 5)
        };
    }

    #endregion
}