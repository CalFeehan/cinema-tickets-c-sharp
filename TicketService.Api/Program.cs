using TicketService.Api.Business;
using TicketService.Api.ThirdParty;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ITicketRequestService, TicketRequestService>();
builder.Services.AddSingleton<ISeatReservationService, SeatReservationService>();
builder.Services.AddSingleton<ITicketPaymentService, TicketPaymentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
