using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ORMFinal.Models;

namespace ORMFinal.DAL
{
    public class EmployeeDAL
    {
        private readonly ORMFinalContext _context;
        private readonly ILogger<EmployeeDAL> _logger;

        public EmployeeDAL(ORMFinalContext context, ILogger<EmployeeDAL> logger)
        {
            _context = context;

            _logger = logger;
        }

        //public List<Employee> GetEmployees()

        public List<Employee> GetEmployees()
        {
            return _context.Employees.Include(e => e.Exhibit).ToList();
        }


        public Employee GetEmployeeById(int id)
        {
            return _context.Employees
                .Include(e => e.Exhibit)
                .FirstOrDefault(e => e.EmployeeId == id);
        }



        // create
        public void AddEmployee(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }





        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }


        // Validation
        public bool HasEmployeesForExhibit(int exhibitId)
        {
            return _context.Employees.Any(e => e.ExhibitId == exhibitId);
        }


    }
}
