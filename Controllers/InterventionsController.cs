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
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public InterventionsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

        // PUT: api/changet-status-to-in-progress/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("change-status-to-in-progress/{id}")]
        public async Task<ActionResult<Intervention>> PutInProgressStatusIntervention(long id, Intervention intervention)
        {
            //save intervention in a temporary variable
            var tmpIntervention = _context.interventions.Where(e => e.Id == intervention.Id).FirstOrDefault<Intervention>();

            if (id != intervention.Id)
            {
                return BadRequest();
            }
            _context.Entry(tmpIntervention).State = EntityState.Detached;
            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                intervention.Id = tmpIntervention.Id;
                intervention.start_date = DateTime.Now;
                intervention.end_date = tmpIntervention.end_date;
                intervention.result = tmpIntervention.result;
                intervention.report = tmpIntervention.report;
                intervention.status = "InProgress";
                intervention.created_at = tmpIntervention.created_at;
                intervention.updated_at = DateTime.Now;
                intervention.author = tmpIntervention.author;
                intervention.customer_id = tmpIntervention.customer_id;
                intervention.building_id = tmpIntervention.building_id;
                intervention.employee_id = tmpIntervention.employee_id;
                intervention.battery_id = tmpIntervention.battery_id;
                intervention.column_id = tmpIntervention.column_id;
                intervention.elevator_id = tmpIntervention.elevator_id;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return intervention;
        }

        //PUT api/change-status-to-completed/{id}
        [HttpPut("change-status-to-completed/{id}")]
        public async Task<ActionResult<Intervention>> PutCompletedIntervention(long id, Intervention intervention)
        {
            //save intervention in a temporary variable
            var tmpIntervention = _context.interventions.Where(e => e.Id == intervention.Id).FirstOrDefault<Intervention>();

            if (id != intervention.Id)
            {
                return BadRequest();
            }
            _context.Entry(tmpIntervention).State = EntityState.Detached;
            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                intervention.Id = tmpIntervention.Id;
                intervention.start_date = tmpIntervention.start_date;
                intervention.end_date = DateTime.Now;
                intervention.result = tmpIntervention.result;
                intervention.report = tmpIntervention.report;
                intervention.status = "Completed";
                intervention.created_at = tmpIntervention.created_at;
                intervention.updated_at = DateTime.Now;
                intervention.author = tmpIntervention.author;
                intervention.customer_id = tmpIntervention.customer_id;
                intervention.building_id = tmpIntervention.building_id;
                intervention.employee_id = tmpIntervention.employee_id;
                intervention.battery_id = tmpIntervention.battery_id;
                intervention.column_id = tmpIntervention.column_id;
                intervention.elevator_id = tmpIntervention.elevator_id;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return intervention;
        }

        
        
        // get: api/interventions/pending-interventions
        [HttpGet("pending-interventions")]
        public ActionResult<List<Intervention>> GetPendingInterventions()
        {
            List<Intervention> InterventionsAll = _context.interventions.ToList();
            List<Intervention> PendingInterventions = new List<Intervention>();

            foreach (Intervention intervention in InterventionsAll){
                if (intervention.status == "Pending"){
                    System.Console.WriteLine("pending");
                    if (intervention.start_date == null)
                    {
                    PendingInterventions.Add(intervention);
                    }
                }
            }
            
            return PendingInterventions;
        }

        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.Id }, intervention);
        }

       
        private bool CustomerExists(long id)
        {
            return _context.customers.Any(e => e.Id == id);
        }

        private bool InterventionExists(long id)
        {
            return _context.interventions.Any(e => e.Id == id);
        }
    }
}
