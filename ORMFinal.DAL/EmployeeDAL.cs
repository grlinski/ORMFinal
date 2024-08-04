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
        //{
        //    return _context.Employees.ToList();
        //}
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



        public void AddEmployee(Employee newEmployee)
        {
            _logger.LogInformation("AddEmployee method in DAL called.");

            try
            {
                _context.Employees.Add(newEmployee);
                _context.SaveChanges();
                _logger.LogInformation("Employee added to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding employee to database.");
                throw; // Optionally rethrow the exception or handle it accordingly
            }
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
    }
}
