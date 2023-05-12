using jalaproj.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jalaproj.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly EmployeeDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public EmployeeController(EmployeeDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }






        /*  public async Task<IActionResult> Index()
          {
              var Employee = await _context.Employees.Include(s => s.Country).Include(s => s.City).ToListAsync();

              return View(Employee);
          }*/
        public async Task<IActionResult> Index(string gender)
        {
            IQueryable<Employee> employees = _context.Employees.Include(s => s.Country).Include(s => s.City);

            if (!string.IsNullOrEmpty(gender))
            {
                employees = employees.Where(e => e.Gender == gender);
            }

            var employeeList = await employees.ToListAsync();

            return View(employeeList);
        }



        public IActionResult Create()
        {
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Cities = _context.Cities.ToList();
            return View();
        }

        // POST: StudentRegistration/Create
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Save the model to the database
                _context.Employees.Add(employee);
                _context.SaveChanges();

                // Redirect to the index page
                return RedirectToAction("Index");
            }

            // If the model is not valid, return the view with validation errors
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Cities = _context.Cities.ToList();
            return View(employee);
        }
        public async Task<IActionResult> Edit(int? id) 
        {
            if(id== null)
            {
                return NotFound();

            }
            var employee =  await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Cities = _context.Cities.ToList();
            return View(employee);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Employee emp) {
            if(id != emp.EmployeeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(emp.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Cities = _context.Cities.ToList();
            return View(emp);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) { return NotFound();}
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }












        private bool EmployeeExists(int id)
        {

            {
                return _context.Employees.Any(e => e.EmployeeId == id);
            }
        }

        public IActionResult GetCities(int CountryId)
        {
            var cities = _context.Cities.Where(c => c.CountryId == CountryId).ToList();

            var cityList = cities.Select(c => new SelectListItem
            {
                Value = c.CityId.ToString(),
                Text = c.CityName
            });

            return Json(cityList);
        }
    }
}
