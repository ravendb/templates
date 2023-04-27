namespace MyCompany.Functions;

public record WelcomeData
{
  public string functionName;
  public string invocationId;
  public string version;
  public string url;
  public string database;
  public bool connected;
  public Exception error;
}

public class WelcomeTemplate
{
  public static async Task<string> RenderHtmlAsync(WelcomeData welcomeData)
  {
    var welcomeTemplateRaw = await System.IO.File.ReadAllTextAsync("welcome.tmpl.html");
    var welcomeTemplate = Scriban.Template.Parse(welcomeTemplateRaw);
    return welcomeTemplate.Render(welcomeData);
  }
}