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
    [Route("api/buildings")]
    [ApiController]
    public class buildingsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public buildingsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

         // GET: api/buildings/needing-intervention
        [HttpGet("needing-intervention")]

        public ActionResult<List<Building>> GetBuildingsNeedingIntervention()
        {
            var BatteriesAll =  _context.batteries.ToList();
            var BuildingsAll = _context.buildings.ToList();
            var ColumnsAll = _context.columns.ToList();
            var ElevatorsAll = _context.elevators.ToList();
            List<Column> interventionCol = new List<Column>();
            List<Elevator> interventionEle = new List<Elevator>();
            List<Battery> interventionBatt = new List<Battery>() ;
            List<Building> buildingsInIntervention = new List<Building>() ;

            // select elevators that require intervention and add them to a list
            foreach (Elevator ele in ElevatorsAll)
            {
                if (ele.status == "Intervention" || ele.status == "intervention")
                {
                    Int64 counter = 0;
                    foreach (Elevator E in interventionEle)
                    {
                        if (E.column_id == ele.column_id)
                        {
                            counter ++ ;
                        }
                    }
                    if (counter == 0)
                    {
                        interventionEle.Add(ele);
                    }
                }
            }
            // add the columns containing elevators that require intervention to a list
            foreach (Elevator elev in interventionEle)
            {
                foreach(Column col in ColumnsAll)
                {
                    if (col.Id == elev.column_id)
                    {
                        interventionCol.Add(col);
                    }
                }
            }
            // select the columns in intervention and add them to the previous list
            foreach (Column ele in ColumnsAll)
            {
                if (ele.status == "Intervention" || ele.status == "intervention")
                {
                    Int64 counter = 0;
                    foreach (Column E in interventionCol)
                    {
                        if (E.battery_id == ele.battery_id)
                        {
                            counter ++ ;
                        }
                    }
                    if (counter == 0)
                    {
                        interventionCol.Add(ele);
                    }
                }
            }
            // put all batteries containing selected columns into a list
            foreach (Column col in interventionCol)
            {
                foreach(Battery batt in BatteriesAll)
                {
                    if (batt.Id == col.battery_id)
                    {
                        interventionBatt.Add(batt);
                    }
                }
            }
            //add Batteries in intervention to the previous list
            foreach (Battery batt in BatteriesAll)
            {
                if (batt.status == "Intervention" || batt.status == "intervention")
                {
                    // System.Console.WriteLine(batt.building_id);
                    Int64 counter = 0;
                    foreach (Battery B in interventionBatt)
                    {   
                        // System.Console.WriteLine("int");
                        if (B.building_id == batt.building_id)
                        {
                            counter ++ ;
                        }                     

                    }
                    if (counter == 0)
                    {
                        interventionBatt.Add(batt);
                    }
                }
            }

            // select the Buildings containing the batteries of previous list and put them to the list
            foreach (Battery batt in interventionBatt)
            {
                foreach (Building build in BuildingsAll)
                {
                    if (build.Id == long.Parse(batt.building_id))
                    {
                        buildingsInIntervention.Add(build);
                    }
                } 
            }
            //format the list to remove duplicates and sort it
            List<Building> buildingsInInterventionUnique = buildingsInIntervention.Distinct().ToList();
            buildingsInInterventionUnique = buildingsInInterventionUnique.OrderBy( o => o.Id).ToList();

            return buildingsInInterventionUnique;
        }

        // GET: api/buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }
        // Get:api/buildings/for-customer-{id}
        [HttpGet("for-customer-{id}")]
        public ActionResult<List<Building>> GetCustomerBuildings(long id)
        {
            // get a complete list of buildings
            List<Building> buildingsAll = _context.buildings.ToList();
            List<Building> CustomersBuildings = new List<Building>();
            // select relevant buildings
            foreach(Building building in buildingsAll)
            {
                if (building.customer_id == id)
                {   
                    // only add buildings that belong to desired customer
                    CustomersBuildings.Add(building);
                }
            }
            return CustomersBuildings;
             
        }

        
        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
