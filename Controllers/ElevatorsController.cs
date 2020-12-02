using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using intervention_management.Models;

namespace Intervention_management.Controllers
{
    [Route("api/elevators")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public ElevatorsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            var status = elevator.status;

            Console.WriteLine(elevator.GetType());
            Console.WriteLine(status);


            return  elevator;
        }

        // GET: api/elevators/status/5
        [HttpGet("status/{id}")]
        public async Task<ActionResult<String>> GetElevatorStatus(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator.status;
        }


        // GET: api/Elevators/not-operating
        [HttpGet("not-operating")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetNotOperatingElevators()
        {
            return await _context.elevators.Where( e => e.status != "Active" ).ToListAsync();
        }

        // PUT: api/Elevators/5/status
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutElevatorStatus(long id, Elevator elevator)
        {
            //save elevator in a temporary variable
            var modifiedElevator = _context.elevators.Where(e => e.Id == elevator.Id).FirstOrDefault<Elevator>();
 


            if (id != elevator.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(modifiedElevator).State = EntityState.Detached;
            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                // replace user input with the saved elements except the field to be change by endpoint
                elevator.Id = modifiedElevator.Id;
                elevator.model = modifiedElevator.model;
                elevator.type_of_building = modifiedElevator.type_of_building;
                // // elevator.status = modifiedElevator.status;
                elevator.last_inspection_date = modifiedElevator.last_inspection_date;
                elevator.inspection_certificate = modifiedElevator.inspection_certificate;
                elevator.information = modifiedElevator.information;
                elevator.notes = modifiedElevator.notes;
                elevator.created_at = modifiedElevator.created_at;
                elevator.updated_at = DateTime.Now;
                elevator.column_id = modifiedElevator.column_id;
                elevator.customer_id = modifiedElevator.customer_id;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        // Get:api/elevators/for-column-{id}
        [HttpGet("for-column-{id}")]
        public ActionResult<List<Elevator>> GetColumnElevator(long id)
        {
            // get a complete list of elevators
            List<Elevator> ElevatorsAll = _context.elevators.ToList();
            List<Elevator> ColumnElevators = new List<Elevator>();
            // select relevant elevators
            foreach(Elevator elevator in ElevatorsAll)
            {
                if ((elevator.column_id) == id)
                {   
                    // only add elevators that belong to desired column
                    ColumnElevators.Add(elevator);
                }
            }
            return ColumnElevators;
             
        }
        
        private bool ElevatorExists(long id)
        {
            return _context.elevators.Any(e => e.Id == id);
        }
    }
}
