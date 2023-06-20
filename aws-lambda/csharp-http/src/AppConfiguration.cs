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
  /// Certificate public key in plain-text. Used in conjunction with CertPrivateKey.
  /// </summary>
  /// <value></value>
  public string CertPublicKeyFilePath { get; set; }

  /// <summary>
  /// Certificate private key base64-encoded. Used in conjunction with CertPublicKeyFilePath.
  /// </summary>
  /// <value></value>
  public string CertPrivateKey { get; set; }
}