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

        /// <summary>
        /// Enpoint responsible to retrieve a list of all trucks.
        /// </summary>
        /// <returns>List of <c>Truck</c> class objects, enveloped in <c>ActionResult</c>.</returns>
        [HttpGet("GetTrucks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTruckModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var trucks = await _truckService.GetAll<GetTruckModel>();

            if (trucks.Any())
            {
                return Ok(trucks);
            }

            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of <c>Truck</c> class objects, enveloped in <c>ActionResult</c>.</returns>
        [HttpGet("GetTruckById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTruckModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var truck = await _truckService.GetById<GetTruckModel>(id);

            if (truck != null)
            {
                return Ok(truck);
            }

            return NotFound($"Could not find truck with id:{id}");
        }

    }
}
