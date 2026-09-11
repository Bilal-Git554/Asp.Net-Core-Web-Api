using Library_Management.IRepository;
using Library_Management.Report;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _IStock;
        public StockController(IStockRepository IStock)
        {
            _IStock = IStock;
        }//Dependency Injection For The Stock Report

        [HttpGet]
        public async Task<ActionResult<Whole_Report>> Report()
        {
            var Read_ = await _IStock.Report();
            if (Read_ == null)
            {
                return NotFound("No Stock Available!");
            }
            return Ok(Read_);
        }//Stock Reading Report

    }
}
