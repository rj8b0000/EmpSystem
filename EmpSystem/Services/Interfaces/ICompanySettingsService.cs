using EmpSystem.Controllers;

namespace EmpSystem.Services.Interfaces;

public interface ICompanySettingsService
{
    CompanySettings GetSettings();
}