using Microsoft.AspNetCore.Mvc;
using TicketService.Api.Business;
using TicketService.Api.Controllers;
using TicketService.Api.Models.Enums;
using TicketService.Api.ThirdParty;

namespace TicketService.Api.Tests.Controller;

[TestClass]
public class TicketRequestControllerTests
{
    private TicketRequestController _sut;

    private Mock<ITicketRequestService> _ticketRequestServiceMock;
    private Mock<ISeatReservationService> _seatReservationServiceMock;
    private Mock<ITicketPaymentService> _ticketPaymentServiceMock;

    [TestMethod]
    public async Task Purchase_ReturnsValidResponse()
    {
        // Arrange
        var request = new List<TicketTypeRequest>
        {
            new TicketTypeRequest(TicketType.Adult, 20),
            new TicketTypeRequest(TicketType.Child, 10),
            new TicketTypeRequest(TicketType.Infant, 5)
        };

        // Act
        var objectResult = await _sut.Purchase(1, request) as ObjectResult;
        var statusCodeResult = objectResult?.StatusCode;
        var results = (objectResult?.Value as IEnumerable<TicketTypeResponse>)?.ToList();

        // Assert
        statusCodeResult.Should().Be(201);
        results.Should().NotBeNull();
        results!.Should().BeOfType<List<TicketTypeResponse>>();
        results!.Count.Should().Be(3);
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
        _sut = new TicketRequestController(
            _ticketRequestServiceMock.Object,
            _seatReservationServiceMock.Object,
            _ticketPaymentServiceMock.Object
            );
    }

    private void SetupExpectations()
    {
        // None needed
    }

    private void SetupMocks()
    {
        _ticketRequestServiceMock = new Mock<ITicketRequestService>();
        _seatReservationServiceMock = new Mock<ISeatReservationService>();
        _ticketPaymentServiceMock = new Mock<ITicketPaymentService>();
    }

    private void SetupData()
    {
        // None needed
    }

    #endregion
}
