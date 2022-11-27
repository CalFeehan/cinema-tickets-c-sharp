using TicketService.Api.Business;
using TicketService.Api.Models.Enums;
using TicketService.Api.Models.Exceptions;

namespace TicketService.Api.Tests.Business;

[TestClass]
public class TicketRequestServiceTests
{
    public TicketRequestService _sut;
    public IEnumerable<TicketTypeRequest> _tickets;
    public long _accountId;

    [TestMethod]
    [DataRow(1, 0, 0)]  // 1 ticket
    [DataRow(20, 0, 0)] // 20 tickets
    [DataRow(7, 7, 6)]  // 20 tickets mixture
    [DataRow(1, 1, 0)]  // Child with accompanying Adult
    [DataRow(1, 0, 1)]  // Infant with accompanying Adult
    [DataRow(1, 1, 1)]  // Child and Infant with accompanying Adult
    public void Validate_ShouldNotThrow_GivenCorrectData(int adultTickets, int childTickets, int infantTickets)
    {
        // arrange - not needed as data is clean
        _tickets = new List<TicketTypeRequest>
        {
            new(TicketType.Adult, adultTickets),
            new(TicketType.Child, childTickets),
            new(TicketType.Infant, infantTickets)
        };

        // act
        _sut.Validate(_accountId, _tickets);

        // assert - not needed as throw will fail the test
    }

    [TestMethod]
    [DataRow(0, 1, 0, 0)]  // Invalid Account Id
    [DataRow(1, 0, 0, 0)]  // 0 tickets
    [DataRow(1, 21, 0, 0)] // 21 tickets
    [DataRow(1, 7, 7, 7)]  // 21 tickets mixture
    [DataRow(1, 0, 1, 0)]  // Child with no accompanying Adult
    [DataRow(1, 0, 0, 1)]  // Infant with no accompanying Adult
    [DataRow(1, 0, 1, 1)]  // Child and Infant with no accompanying Adult
    public void Validate_ShouldThrow_GivenIncorrectData(long accountId, int adultTickets, int childTickets, int infantTickets)
    {
        // arrange
        _accountId = accountId;
        _tickets = new List<TicketTypeRequest>
        {
            new(TicketType.Adult, adultTickets),
            new(TicketType.Child, childTickets),
            new(TicketType.Infant, infantTickets)
        };

        // act
        var result = Assert.ThrowsException<InvalidPurchaseException>(() => _sut.Validate(_accountId, _tickets));

        // assert - if it reaches this section, then no errors were thrown.
        result.Should().BeOfType<InvalidPurchaseException>();
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
        _sut = new TicketRequestService();
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
        _accountId = 1;
        _tickets = new List<TicketTypeRequest>
        {
            new(TicketType.Adult, 10),
            new(TicketType.Child, 5),
            new(TicketType.Infant, 5)
        };
    }

    #endregion
}