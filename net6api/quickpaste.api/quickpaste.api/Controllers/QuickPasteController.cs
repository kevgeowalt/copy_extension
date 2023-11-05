using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quickpaste.api.Interfaces;

namespace quickpaste.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuickPasteController : ControllerBase
    {
        private readonly IDataService dataService;
        public QuickPasteController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetRecordsAsync(int n = 5)
        {
            var records = await dataService.RetrieveAsync(n);
            return Ok(records);
        }
    }
}
