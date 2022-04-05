using GarageVolver.API.Models;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Validators;
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
        /// Enpoint responsible to retrieve truck with corresponding id.
        /// </summary>
        /// <param name="id">Id of the truck.</param>
        /// <returns><c>Truck</c> class object, enveloped in <c>ActionResult</c>.</returns>
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

            return NotFound($"Could not find truck with id:{id}.");
        }

        /// <summary>
        /// Endpoint responsible to create new truck.
        /// </summary>
        /// <param name="newTruck"></param>
        /// <returns>Created <c>Truck</c> class object, enveloped in <c>ActionResult</c>.</returns>
        [HttpGet("CreateTruck")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTruckModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateTruckModel newTruck)
        {
            if (newTruck == null)
                return BadRequest("Could not create truck with invalid input.");

            var createdTruck =
                await _truckService.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck);

            if(createdTruck != null)
            {
                return Ok(createdTruck);
            }

            return Conflict("Could not create truck, internal operation failure.");
        }

    }
}
