using Backend.EF.Sqlite.DTOs.Employee;
using Backend.EF.Sqlite.Models;

namespace Backend.EF.Sqlite.Helpers.Mappers
{
    public static class EmployeeMapper
    {
        public static List<EmployeeResponse> Map(List<Employee> employees)
        {
            var employeesResponse = GetEmployeesResponse(employees);
            return employeesResponse;
        }

        public static EmployeeResponse Map(Employee employee)
        {
            var employeeResponse = ConvertEmployeeToEmployeeResponse(employee);
            return employeeResponse;
        }

        private static List<EmployeeResponse> GetEmployeesResponse(List<Employee> employees)
        {
            var employeesResponse = new List<EmployeeResponse>();

            employees.ForEach(currentEmployee => {
                var employee = ConvertEmployeeToEmployeeResponse(currentEmployee);
                employeesResponse.Add(employee);
            });

            return employeesResponse;
        }

        private static EmployeeResponse ConvertEmployeeToEmployeeResponse(Employee employee)
        {
            return new EmployeeResponse()
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = 0,
                Salary = employee.Salary,
                CompanyId = employee.CompanyId,
                CompanyName = employee.Company!.Name
            };
        }
    }  
}
