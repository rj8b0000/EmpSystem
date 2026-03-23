using EmpSystem.Controllers;
using EmpSystem.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EmpSystem.Services.Implementations;

public class CompanySettingsService : ICompanySettingsService
{
    private readonly CompanySettings _settings;

    public CompanySettingsService(IOptions<CompanySettings> settings)
    {
        _settings = settings.Value;
    }

    public CompanySettings GetSettings()
    {
        return _settings;
    }
}