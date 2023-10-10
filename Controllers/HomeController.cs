using Microsoft.AspNetCore.Mvc;
using NBPExchangeRates.Interfaces;

namespace NBPExchangeRates.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProviderService _dataProviderService;
        
        public HomeController(IDataProviderService dataProviderService)
        {
            _dataProviderService = dataProviderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _dataProviderService.GetExchangeRates();
            return View(result);
        }
    }
}