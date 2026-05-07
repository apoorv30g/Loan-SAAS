using Microsoft.AspNetCore.Mvc;
using LoanSaas.Backend.Data;
using LoanSaas.Backend.Models;

namespace LoanSaas.Backend.Controllers;

[ApiController]
[Route("upload")]
public class UploadController : ControllerBase
{
    private readonly AppDbContext _db;

    public UploadController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, int clientId)
    {
        using var reader = new StreamReader(file.OpenReadStream());

        while (await reader.ReadLineAsync() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var row = line.Split(',');
            if (row.Length < 4)
            {
                continue;
            }

            _db.Leads.Add(new Lead
            {
                ClientId = clientId,
                Name = row[0],
                Phone = row[1],
                LoanAmount = int.Parse(row[2]),
                DropStage = row[3]
            });
        }

        await _db.SaveChangesAsync();

        return Ok("uploaded");
    }
}