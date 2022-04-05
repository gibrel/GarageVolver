using GarageVolver.API.Models;
using GarageVolver.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GarageVolver.API.Controllers
{
    /// <summary>
    /// Class <c>TruckController</c> handles API requests for truck registration solution.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;

        /// <summary>
        /// Constructor for <c>TruckController</c> that uses <c>TruckService</c>
        /// for record management.
        /// </summary>
        /// <param name="truckService"></param>
        public TruckController(
            ITruckService truckService)
        {
            _truckService = truckService;
        }


        [HttpGet("GetTrucks")]
        public async Task<IActionResult> Get()
        {
            var customers = await _truckService.GetAll<GetTruckModel>();

            if (customers.Any())
            {
                return Ok(customers);
            }

            return NotFound();
        }

    }
}
