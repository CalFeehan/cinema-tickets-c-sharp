namespace TicketService.Api.ThirdParty;

public interface ITicketPaymentService
{
    public void MakePayment(long accountId, int totalAmountToPay);
}