using Raven.DependencyInjection;

public class AppConfiguration
{
  public RavenExtendedSettings RavenSettings { get; set; }
}

public class RavenExtendedSettings : RavenSettings
{
  /// <summary>
  /// Certificate bytes (.pfx) as UTF8-encoded string.
  /// </summary>
  /// <value></value>
  public string CertBytes { get; set; }

  /// <summary>
  /// Certificate PEM with public/private key in plain-text
  /// </summary>
  /// <value></value>
  public string CertPem { get; set; }
}