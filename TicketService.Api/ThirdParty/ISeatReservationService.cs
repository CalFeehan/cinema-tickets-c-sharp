namespace TicketService.Api.ThirdParty;

public interface ISeatReservationService
{
    public void ReserveSeat(long accountId, int totalSeatsToAllocate);
}