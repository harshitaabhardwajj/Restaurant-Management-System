using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using ManagementSystem1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;



        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IActionResult Index()
        {
            var menuItems = _context.MenuItems.ToList();
            return View(menuItems); // Ensure data is passed
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.PasswordHash);

            if (user != null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Invalid credentials!";
            return View();
        }




        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return View();
        }




        public async Task<IActionResult> Menu()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return View(menuItems);
        }

        [HttpGet]
        public IActionResult AddMenuItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _context.MenuItems.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Menu");
            }

            return View(menuItem);
        }

        public IActionResult DeleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Menu"); // ✅ Redirect after deletion
        }






        public IActionResult Employee()
        {
            var employees = _context.Employees.ToList(); // 🔹 Ensure this returns a list, not null
            if (employees == null) // 🚨 Check if employees list is null
            {
                employees = new List<Employee>(); // Assign an empty list to prevent null reference
            }
            return View(employees);
        }


        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Employee");

            }

            return View(employee);
        }

        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee); // 🔥 Delete the employee
                _context.SaveChanges(); // 💾 Save changes to the database
            }
            return RedirectToAction("Employee"); // 🔄 Redirect back to the employee list
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
