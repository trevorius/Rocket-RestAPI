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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public EmployeesController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Getemployees()
        {
            return await _context.employees.ToListAsync();
        }
        // Get: api/Employees/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<bool>> GetEmployeeByEmail(string email)
        {
            var employeeList =  await _context.employees.ToListAsync();
            foreach (Employee employee in employeeList)
            {
                if (employee.email == email)
                {
                    return true;
                }
            }
            return NotFound();
        }

    //     // GET: api/Employees/5
    //     [HttpGet("{id}")]
    //     public async Task<ActionResult<Employee>> GetEmployee(long id)
    //     {
    //         var employee = await _context.employees.FindAsync(id);

    //         if (employee == null)
    //         {
    //             return NotFound();
    //         }

    //         return employee;
    //     }

    //     // PUT: api/Employees/5
    //     // To protect from overposting attacks, enable the specific properties you want to bind to, for
    //     // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    //     [HttpPut("{id}")]
    //     public async Task<IActionResult> PutEmployee(long id, Employee employee)
    //     {
    //         if (id != employee.Id)
    //         {
    //             return BadRequest();
    //         }

    //         _context.Entry(employee).State = EntityState.Modified;

    //         try
    //         {
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!EmployeeExists(id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }

    //         return NoContent();
    //     }

    //     // POST: api/Employees
    //     // To protect from overposting attacks, enable the specific properties you want to bind to, for
    //     // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    //     [HttpPost]
    //     public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    //     {
    //         _context.employees.Add(employee);
    //         await _context.SaveChangesAsync();

    //         return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
    //     }

    //     // DELETE: api/Employees/5
    //     [HttpDelete("{id}")]
    //     public async Task<ActionResult<Employee>> DeleteEmployee(long id)
    //     {
    //         var employee = await _context.employees.FindAsync(id);
    //         if (employee == null)
    //         {
    //             return NotFound();
    //         }

    //         _context.employees.Remove(employee);
    //         await _context.SaveChangesAsync();

    //         return employee;
        // }

        private bool EmployeeExists(long id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
