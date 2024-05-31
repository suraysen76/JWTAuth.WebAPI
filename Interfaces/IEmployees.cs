using JWTAuth.WebAPI.Models;

namespace JWTAuth.WebAPI.Interfaces
{
    public interface IEmployees
    {
        public List<EmployeeModel> GetEmployeeDetails();
        public EmployeeModel GetEmployeeDetails(int id);
        public void AddEmployee(EmployeeModel employee);
        public void UpdateEmployee(EmployeeModel employee);
        public EmployeeModel DeleteEmployee(int id);
        public bool CheckEmployee(int id);
    }
}
