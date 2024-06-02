
using JWTAuth.WebAPI.Interfaces;
using JWTAuth.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.WebApi.Repository
{
    public class EmployeeRepository : IEmployees
    {
        readonly DatabaseContext _dbContext = new();

        public EmployeeRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EmployeeModel> GetEmployeeDetails()
        {
            try
            {
                return _dbContext.Employees.ToList();
            }
            catch
            {
                throw;
            }
        }

        public EmployeeModel GetEmployeeDetails(int id)
        {
            try
            {
                var set = _dbContext.Employees;
                EmployeeModel? employee = _dbContext.Employees.Where(ee=>ee.Id==id).FirstOrDefault();
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddEmployee(EmployeeModel employee)
        {
            try
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public EmployeeModel DeleteEmployee(int id)
        {
            try
            {
                EmployeeModel? employee = _dbContext.Employees.Where(ee=>ee.Id==id).FirstOrDefault();

                if (employee != null)
                {
                    _dbContext.Employees.Remove(employee);
                    _dbContext.SaveChanges();
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckEmployee(int id)
        {
            return _dbContext.Employees.Any(e => e.Id == id);
        }
    }
}