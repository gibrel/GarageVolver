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
        /// Endpoint responsible to create new truck.
        /// </summary>
        /// <param name="newTruck"><c>CreateTruckModel</c> class object with truck data.</param>
        /// <returns>Created <c>GetTruckModel</c> class object with data of the created truck.</returns>
        [HttpPut("CreateTruck")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTruckModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateTruckModel newTruck)
        {
            if (newTruck == null)
                return BadRequest("Could not create truck with invalid input.");

            var createdTruck =
                await _truckService.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck);

            if (createdTruck != null)
            {
                return Ok(createdTruck);
            }

            return Conflict("Could not create truck, internal operation failure.");
        }

        /// <summary>
        /// Enpoint responsible to retrieve a list of all trucks.
        /// </summary>
        /// <returns>List of <c>GetTruckModel</c> class objects of all found trucks.</returns>
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
        /// <returns><c>GetTruckModel</c> class object of the corresponding id.</returns>
        [HttpGet("GetTruckById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTruckModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid truck id:'{id}'.");

            var truck = await _truckService.GetById<GetTruckModel>(id);

            if (truck != null)
            {
                return Ok(truck);
            }

            return NotFound($"Could not find truck with id:'{id}'.");
        }

        /// <summary>
        /// Enpoint responsible to update truck with new values.
        /// </summary>
        /// <param name="truckToUpdate"><c>UpdateTruckModel</c> class object with truck data to be updated.</param>
        /// <returns>Updated <c>GetTruckModel</c> class object.</returns>
        [HttpPatch("UpdateTruck")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTruckModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromBody] UpdateTruckModel truckToUpdate)
        {
            if (truckToUpdate == null)
                return BadRequest("Could not update truck with invalid input.");

            var updatedTruck =
                await _truckService.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(truckToUpdate);

            if (updatedTruck != null)
            {
                return Ok(updatedTruck);
            }

            return Conflict("Could not update truck, internal operation failure.");
        }

        /// <summary>
        /// Enpoint responsible to delete truck registry by its id.
        /// </summary>
        /// <param name="id">Id of the truck to be purged.</param>
        /// <returns>String message with success or failure to delete register.</returns>
        [HttpDelete("DeleteTruck")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid truck id:'{id}'.");

            var response = await _truckService.Delete(id);

            if (response)
            {
                return Ok($"Sucessfully deleted register of truck with id:'{id}'");
            }

            return NotFound($"Could not find truck with id:'{id}'.");
        }
    }
}
