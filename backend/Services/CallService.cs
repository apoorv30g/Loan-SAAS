namespace LoanSaas.Backend.Services;

using LoanSaas.Backend.Models;

public class CallService
{
    private readonly ExotelService _exotel;

    public CallService(ExotelService exotel)
    {
        _exotel = exotel;
    }

    public async Task TriggerCall(Lead lead)
    {
        await _exotel.MakeCall(lead.Phone, lead.Id);
    }
}
