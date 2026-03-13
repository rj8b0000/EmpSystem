using EmpSystem.Models;
using EmpSystem.ViewModel;

namespace EmpSystem.Repository;

public interface IDepartmentRepository
{
    Task<DepartmentViewModal> GetByIdAsync(int Id);
    Task<List<DepartmentViewModal>> GetAllAsync();
    Task AddDepartment(DepartmentViewModal department);
    Task UpdateAsync(DepartmentViewModal department);
    Task DeleteAsync(int Id);
    
}