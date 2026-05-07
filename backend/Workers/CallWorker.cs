namespace LoanSaas.Backend.Workers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LoanSaas.Backend.Data;
using LoanSaas.Backend.Services;

public class CallWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scope;

    public CallWorker(IServiceScopeFactory scope)
    {
        _scope = scope;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scope.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var callService = scope.ServiceProvider.GetRequiredService<CallService>();

            var leads = db.Leads
                .Where(x => x.Status == "pending" && x.Attempts < 3)
                .Take(5)
                .ToList();

            foreach (var lead in leads)
            {
                lead.Attempts++;
                await callService.TriggerCall(lead);
            }

            await db.SaveChangesAsync(stoppingToken);

            await Task.Delay(5000, stoppingToken);
        }
    }
}