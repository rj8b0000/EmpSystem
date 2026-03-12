using EmpSystem.Models;
using EmpSystem.ViewModel;

namespace EmpSystem.Repository;

public interface IDepartmentRepository
{
    Task<Department> GetAsync(int Id);
    Task<List<DepartmentViewModal>> GetAllAsync();
    Task AddDepartment(DepartmentViewModal department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(int Id);
    
}