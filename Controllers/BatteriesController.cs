using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using intervention_management.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Intervention_management.Controllers
{
    [Produces("application/json")]
    [Route("api/Batteries")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public BatteriesController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET: api/batteries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBattery(long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

        // GET: api/batteries/status/5
        [HttpGet("status/{id}")]
        public async Task<ActionResult<String>> GetBatteryStatus(long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery.status;
        }
        
        // GET: api/Elevators/not-operating
        [HttpGet("not-operating")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetNotOperatingElevators()
        {
            return await _context.elevators.Where( e => e.status != "Active" ).ToListAsync();
        }

        
        // PUT: api/Batteries/5/status
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutBatteryStatus(long id, Battery battery)
        {   
            
            var originalBattery = _context.batteries.Where(e => e.Id == battery.Id).FirstOrDefault<Battery>();

            if (id != battery.Id)
            {
                return BadRequest();
            }

            _context.Entry(originalBattery).State = EntityState.Detached;

            _context.Entry(battery).State = EntityState.Modified;

            try
            {

                battery.Id = originalBattery.Id;
                battery.building_id = originalBattery.building_id;
                battery.type_of_building = originalBattery.type_of_building;
                // battery.status = originalBattery.status;
                battery.employee_id = originalBattery.employee_id ;
                battery.commissioning_date = originalBattery.commissioning_date;
                battery.last_inspection_date = originalBattery.last_inspection_date;
                battery.operations_certificate = originalBattery.operations_certificate;
                battery.information = originalBattery.information;
                battery.notes = originalBattery.notes;
                battery.created_at = originalBattery.created_at;
                battery.updated_at = DateTime.Now;
                battery.customer_id = originalBattery.customer_id;


                

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Get:api/batteries/for-building-{id}
        [HttpGet("for-building-{id}")]
        public ActionResult<List<Battery>> GetBuildingBatterie(long id)
        {
            // get a complete list of batteries
            List<Battery> batteriesAll = _context.batteries.ToList();
            List<Battery> buildingBatteries = new List<Battery>();
            // select relevant batteries
            foreach(Battery battery in batteriesAll)
            {
                if (Int32.Parse(battery.building_id) == id)
                {   
                    // only add batteries that belong to desired building
                    buildingBatteries.Add(battery);
                }
            }
            return buildingBatteries;
             
        }

        

        private bool BatteryExists(long id)
        {
            return _context.batteries.Any(e => e.Id == id);
        }
    }
}
