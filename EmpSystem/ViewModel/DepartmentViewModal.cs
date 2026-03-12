using System.ComponentModel.DataAnnotations;
using EmpSystem.Models;

namespace EmpSystem.ViewModel;

public class DepartmentViewModal
{
    public int DepartmentId { get; set; } 
    [Required(ErrorMessage = "Name is required")]
    public string DepartmentName { get; set; }
}