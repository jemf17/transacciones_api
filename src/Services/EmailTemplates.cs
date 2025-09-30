using System.IO;
using DBModels;
namespace ApiTrans.Email {
public class TemplateService
{
    private readonly IWebHostEnvironment _env;

    public TemplateService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string GetTemplate(string templateName, Dictionary<string, object> parameters)
    {
        var templatePath = Path.Combine(_env.ContentRootPath, "Templates", $"{templateName}.html");
        
        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Template {templateName} not found");
        }

        var templateContent = File.ReadAllText(templatePath);

        foreach (var param in parameters)
        {
            var stringValue = Convert.ToString(param.Value) ?? string.Empty;
            templateContent = templateContent.Replace($"{{{{{param.Key}}}}}", stringValue);
        }

        return templateContent;
    }
    }
}