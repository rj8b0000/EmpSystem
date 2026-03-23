namespace EmpSystem.Services.Interfaces;

public interface IEmailTemplateService
{
    Task<string> GetEmployeeWelcomeTemplateAsync(string firstName);
}