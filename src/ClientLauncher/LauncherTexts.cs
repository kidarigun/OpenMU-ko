// <copyright file="LauncherTexts.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.ClientLauncher;

using System.Globalization;
using System.Resources;

/// <summary>
/// The user-facing texts of the launcher, in the language of the current UI culture.
/// </summary>
/// <remarks>
/// The texts are stored in <c>Properties/LauncherTexts.resx</c> and its culture-specific
/// siblings, e.g. <c>LauncherTexts.ko.resx</c>. Missing translations fall back to English.
/// </remarks>
internal static class LauncherTexts
{
    private static readonly ResourceManager Manager = new("MUnique.OpenMU.ClientLauncher.Properties.LauncherTexts", typeof(LauncherTexts).Assembly);

    /// <summary>
    /// Gets the text with the specified name.
    /// </summary>
    /// <param name="name">The name of the text.</param>
    /// <returns>The text of the current UI culture, or the name when it doesn't exist.</returns>
    public static string Get(string name) => Manager.GetString(name, CultureInfo.CurrentUICulture) ?? name;

    /// <summary>
    /// Gets the text with the specified name and inserts the arguments into it.
    /// </summary>
    /// <param name="name">The name of the text.</param>
    /// <param name="arguments">The arguments which are inserted into the text.</param>
    /// <returns>The formatted text of the current UI culture.</returns>
    public static string Format(string name, params object[] arguments) => string.Format(CultureInfo.CurrentCulture, Get(name), arguments);
}
