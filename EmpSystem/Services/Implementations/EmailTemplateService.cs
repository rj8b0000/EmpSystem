using EmpSystem.Services.Interfaces;

namespace EmpSystem.Services.Implementations;

public class EmailTemplateService: IEmailTemplateService
{
    private readonly IWebHostEnvironment _env;
    private readonly ICompanySettingsService _companySettingsService;
    private IEmailTemplateService _emailTemplateServiceImplementation;

    public EmailTemplateService(
        IWebHostEnvironment env,
        ICompanySettingsService companySettingsService
    )
    {
        _env = env;
        _companySettingsService = companySettingsService;
    }

    public async Task<string> GetEmployeeWelcomeTemplateAsync(string firstName)
    {
        var path = Path.Combine(
            _env.ContentRootPath,
            "EmailTemplate",
            "EmployeeWelcomeTemplate.html"
        );
        var template = await File.ReadAllTextAsync(path);
        var company = _companySettingsService.GetSettings();
        
        template = template.Replace("{{FirstName}}", firstName);
        template = template.Replace("{{CompanyName}}", company.CompanyName);
        template = template.Replace("{{SupportEmail}}", company.SupportEmail);
        template = template.Replace("{{HRPhone}}", company.Phone);
        
        return template;
    }
}