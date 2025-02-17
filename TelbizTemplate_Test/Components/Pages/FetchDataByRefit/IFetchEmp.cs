using Refit;
using TelbizTemplate_Test.Components.Models;

namespace TelbizTemplate_Test.Components.Pages.FetchDataByRefit
{
    public interface IFetchEmp
    {
        [Get("/users")]
        Task<List<Employee>> GetEmployee();

        [Get("/users/{id}")]
        Task<Employee> GetEmployeeById(int id);

        //[Post("/users/{id}")]
        //Task<Employee> CreateEmployee([Body]Employee employee);


        //[Put("/users/{id}")]
        //Task UpdateEmployee(int id, [Body]Employee employee);

        //[Delete("/users/{id}")]
        //Task DeleteEmployeeById(int id);



    }
}
