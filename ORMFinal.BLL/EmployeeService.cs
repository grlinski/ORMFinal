using Microsoft.Extensions.Logging;
using ORMFinal.DAL;
using ORMFinal.Models;

namespace ORMFinal.BLL
{
    public class EmployeeService
    {
        private readonly EmployeeDAL _employeeDAL;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(EmployeeDAL employeeDAL, ILogger<EmployeeService> logger)
        {
            _employeeDAL = employeeDAL;
            _logger = logger;
        }

        public List<Employee> GetEmployees()
        {
            return _employeeDAL.GetEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeDAL.GetEmployeeById(id);
        }




        public void DeleteEmployee(int id)
        {
            _employeeDAL.DeleteEmployee(id);
        }


        public void AddEmployee(Employee newEmployee)
        {
            _logger.LogInformation("AddEmployee method called.");

            // Log details of the employee being added
            _logger.LogInformation($"Adding Employee: Position={newEmployee.Position}, DateStarted={newEmployee.DateStarted}, DateEnded={newEmployee.DateEnded}, ExhibitId={newEmployee.ExhibitId}");

            _employeeDAL.AddEmployee(newEmployee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeDAL.UpdateEmployee(employee);
        }



        // 
        public bool HasEmployeesForExhibit(int exhibitId)
        {
            return _employeeDAL.HasEmployeesForExhibit(exhibitId);
        }


    }
}
