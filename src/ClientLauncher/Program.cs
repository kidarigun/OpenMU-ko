// <copyright file="Program.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.ClientLauncher;

using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Windows.Forms;
using System.Xml.Serialization;

/// <summary>
/// The static main program.
/// </summary>
[SupportedOSPlatform("windows")]
internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    internal static void Main()
    {
        ApplyConfiguredUiCulture();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }

    /// <summary>
    /// Uses the culture of <see cref="LauncherSettings.UiCulture"/> for the texts, when the launcher configuration sets one.
    /// </summary>
    private static void ApplyConfiguredUiCulture()
    {
        try
        {
            if (!File.Exists(MainForm.ConfigFileName))
            {
                return;
            }

            using var file = new StreamReader(MainForm.ConfigFileName);
            if (new XmlSerializer(typeof(LauncherSettings)).Deserialize(file) is LauncherSettings { UiCulture: { Length: > 0 } cultureName })
            {
                var culture = CultureInfo.GetCultureInfo(cultureName);
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
        }
        catch (Exception ex) when (ex is IOException or InvalidOperationException or CultureNotFoundException or UnauthorizedAccessException)
        {
            // The texts stay in the display language of the operating system.
        }
    }
}