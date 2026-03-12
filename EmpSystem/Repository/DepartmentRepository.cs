using EmpSystem.Data;
using EmpSystem.Models;
using EmpSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmpSystem.Repository;

public class DepartmentRepository: IDepartmentRepository
{
    private readonly AppDbContext _dbContext;
    public DepartmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Department> GetAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DepartmentViewModal>> GetAllAsync()
    {
        List<Department> departments = await _dbContext.Departments.ToListAsync();
        List<DepartmentViewModal> departmentViewModals = new List<DepartmentViewModal>();
        
        foreach (var department in departments)
        {
            var departmentModel = new DepartmentViewModal
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
            };
            departmentViewModals.Add(departmentModel);
        }

        return departmentViewModals;
    }

    public async Task AddDepartment(DepartmentViewModal department)
    {
        var newDepartment = new Department()
        {
            DepartmentName = department.DepartmentName
        };
        await _dbContext.Departments.AddAsync(newDepartment);
        await _dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Department department)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int Id)
    {
        throw new NotImplementedException();
    }
}