using Raven.DependencyInjection;

public class AppConfiguration
{
  public RavenExtendedSettings RavenSettings { get; set; }
}

public class RavenExtendedSettings : RavenSettings
{
  /// <summary>
  /// Certificate bytes as UTF8-encoded string
  /// </summary>
  /// <value></value>
  public string CertBytes { get; set; }
}