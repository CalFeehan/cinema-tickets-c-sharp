using Microsoft.AspNetCore.Mvc;
using TicketService.Api.Business;
using TicketService.Api.Models;
using TicketService.Api.ThirdParty;

namespace TicketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketRequestController : ControllerBase
    {
        private readonly ITicketRequestService _ticketRequestService;
        private readonly ISeatReservationService _seatReservationService;
        private readonly ITicketPaymentService _ticketPaymentService;

        public TicketRequestController(
            ITicketRequestService ticketRequestService,
            ISeatReservationService seatReservationService,
            ITicketPaymentService ticketPaymentService
            )
        {
            _ticketRequestService = ticketRequestService;
            _seatReservationService = seatReservationService;
            _ticketPaymentService = ticketPaymentService;
        }

        /// <summary>
        /// Given a valid purchase request, this will handle calls for payments to be made and seats to be reserved.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="ticketTypeRequest"></param>
        /// <returns>A populated list of tickets purchased.</returns>
        // POST api/<TicketRequestController>/purchasetickets/{accountId}
        [HttpPost("purchasetickets/{accountId}")]
        [ProducesResponseType(typeof(IEnumerable<TicketTypeResponse>), 201)]
        [ProducesResponseType(typeof(IEnumerable<TicketTypeResponse>), 400)]
        public async Task<IActionResult> Purchase(long accountId, [FromBody] IEnumerable<TicketTypeRequest> ticketTypeRequest)
        {
            _ticketRequestService.Validate(accountId, ticketTypeRequest);

            _ticketPaymentService.MakePayment(accountId, ticketTypeRequest.Sum(x => x.Price));
            _seatReservationService.ReserveSeat(accountId, ticketTypeRequest.Sum(x => x.Seats));

            return Created(string.Empty, ticketTypeRequest.Select(x => new TicketTypeResponse(x)));
        }
    }
}
