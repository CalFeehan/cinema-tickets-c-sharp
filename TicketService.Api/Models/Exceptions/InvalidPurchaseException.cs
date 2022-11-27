namespace TicketService.Api.Models.Exceptions;

public class InvalidPurchaseException : BadHttpRequestException
{
    // hidden stack trace to show clear error messages,
    // OnActionExecuting could be used for error handling.
    public override string StackTrace => "";

    public InvalidPurchaseException(string message, int statusCode)
        : base(message, statusCode) { }
}
