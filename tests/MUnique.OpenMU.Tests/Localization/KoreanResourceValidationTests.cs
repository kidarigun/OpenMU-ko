// <copyright file="KoreanResourceValidationTests.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Tests.Localization;

using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

/// <summary>
/// Defines the Korean translation contract for the string resources under src.
/// </summary>
[TestFixture]
public class KoreanResourceValidationTests
{
    private static readonly Regex BraceTokens = new(@"\{\{|\}\}|\{(?:[0-9]+|[A-Za-z_][A-Za-z0-9_]*)(?:\s*,\s*[+-]?[0-9]+)?(?::[^{}]*)?\}|[{}]", RegexOptions.CultureInvariant);

    // A conversion must not consume a prefix of an English word after an ordinary percentage, such as "% per" or "% less".
    private static readonly Regex PrintfTokens = new(@"%%|%(?:[1-9][0-9]*\$)?[-+ #0']*(?:[0-9]+|\*)?(?:\.(?:[0-9]+|\*))?(?:hh|ll|[hljztL])?[diuoxXfFeEgGaAcspn](?![A-Za-z])", RegexOptions.CultureInvariant);

    private static readonly Regex MarkupTokens = new("</?[A-Za-z][A-Za-z0-9:-]*(?:\\s+[A-Za-z_:][A-Za-z0-9_.:-]*\\s*=\\s*(?:\"[^\"]*\"|'[^']*'))*\\s*/?>|&(?:[A-Za-z][A-Za-z0-9]*|#[0-9]+|#x[0-9A-Fa-f]+);", RegexOptions.CultureInvariant);

    /// <summary>
    /// Tests that Korean siblings exist and contain exactly the neutral string keys.
    /// </summary>
    /// <param name="neutralPath">The repository-relative neutral resource path.</param>
    [TestCaseSource(nameof(NeutralResourcePaths))]
    public void KoreanResourcesMatchNeutralStringKeys(string neutralPath)
    {
        var neutral = ReadStringEntries(LoadResource(neutralPath));
        var korean = ReadKoreanStringEntries(neutralPath);
        var missing = neutral.Keys.Except(korean.Keys, StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray();
        var unexpected = korean.Keys.Except(neutral.Keys, StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray();
        Assert.Multiple(() =>
        {
            Assert.That(missing, Is.Empty, $"{KoreanPath(neutralPath)}: missing string keys: {string.Join(", ", missing)}");
            Assert.That(unexpected, Is.Empty, $"{KoreanPath(neutralPath)}: unexpected string keys: {string.Join(", ", unexpected)}");
        });
    }

    /// <summary>
    /// Tests that translations preserve format placeholders, escaped braces and markup, including repetitions.
    /// </summary>
    /// <param name="neutralPath">The repository-relative neutral resource path.</param>
    [TestCaseSource(nameof(NeutralResourcePaths))]
    public void KoreanResourcesPreservePlaceholders(string neutralPath)
    {
        var neutral = ReadStringEntries(LoadResource(neutralPath));
        var korean = ReadKoreanStringEntries(neutralPath);
        Assert.Multiple(() =>
        {
            foreach (var (key, value) in neutral)
            {
                Assert.That(korean.ContainsKey(key), Is.True, $"{KoreanPath(neutralPath)}: missing string key '{key}'");
                if (korean.TryGetValue(key, out var translation))
                {
                    Assert.That(Tokenize(translation), Is.EquivalentTo(Tokenize(value)), $"{KoreanPath(neutralPath)}: placeholder or markup mismatch at key '{key}'");
                }
            }
        });
    }

    /// <summary>
    /// Tests that Korean string values exist and are not empty or whitespace.
    /// </summary>
    /// <param name="neutralPath">The repository-relative neutral resource path.</param>
    [TestCaseSource(nameof(NeutralResourcePaths))]
    public void KoreanResourcesAreNonEmpty(string neutralPath)
    {
        var korean = ReadKoreanStringEntries(neutralPath);
        var emptyKeys = EmptyValueKeys(korean);
        Assert.That(emptyKeys, Is.Empty, $"{KoreanPath(neutralPath)}: empty or whitespace string values at keys: {string.Join(", ", emptyKeys)}");
    }

    /// <summary>
    /// Tests XML selection without loading or duplicating image and object resources.
    /// </summary>
    [Test]
    public void StringEntrySelectionExcludesTypedObjects()
    {
        var document = XDocument.Parse("""
            <root>
              <data name="Text"><value>  Preserve whitespace  </value><comment>Keep this comment</comment></data>
              <data name="ExplicitString" type="System.String, System.Private.CoreLib"><value>String</value></data>
              <data name="Empty"><value /></data>
              <data name="MissingValue" />
              <data name="Image" type="System.Resources.ResXFileRef, System.Windows.Forms"><value>image.png;System.Drawing.Bitmap</value></data>
              <data name="Number" type="System.Int32, System.Private.CoreLib"><value>42</value></data>
              <data name="Object" mimetype="application/x-microsoft.net.object.binary.base64"><value>AAAA</value></data>
              <metadata name="Metadata"><value>Not a resource</value></metadata>
            </root>
            """);

        var strings = ReadStringEntries(document);
        Assert.Multiple(() =>
        {
            Assert.That(strings.Keys, Is.EquivalentTo(new[] { "Text", "ExplicitString", "Empty", "MissingValue" }));
            Assert.That(strings["Text"], Is.EqualTo("  Preserve whitespace  "));
            Assert.That(strings["Empty"], Is.Empty);
            Assert.That(strings["MissingValue"], Is.Empty);
        });
    }

    /// <summary>
    /// Tests representative token families against hand-written expectations.
    /// </summary>
    /// <param name="value">The resource value.</param>
    /// <param name="expected">The expected tokens, including repetitions.</param>
    [TestCase("{0} {1,-5:N2}", new[] { "{0}", "{1,-5:N2}" })]
    [TestCase("%s %ls %1$02d %.2f %%", new[] { "%s", "%ls", "%1$02d", "%.2f", "%%" })]
    [TestCase("% d % ls", new[] { "% d", "% ls" })]
    [TestCase("Defines the damage increase multiplier for first and third level wings. It's 2 % per wing level.", new string[0])]
    [TestCase("The damage absorb of wings per item level, 2 % less damage per level.", new string[0])]
    [TestCase("Fix max mana/AG % increase jewelry options", new string[0])]
    [TestCase("A double weapon wield grants a 10% increase in physical damage. Only DK, MG, and RF can double wield.", new string[0])]
    [TestCase("[{mapName}] Invasion!", new[] { "{mapName}" })]
    [TestCase("{{0}} {{{1}}}", new[] { "{{", "}}", "{{", "{1}", "}}" })]
    [TestCase("<b title=\"{0}\">%ls</b><br />&nbsp;", new[] { "{0}", "%ls", "<b title=\"{0}\">", "</b>", "<br />", "&nbsp;" })]
    [TestCase("{0} {0} %s %s", new[] { "{0}", "{0}", "%s", "%s" })]
    [TestCase("Plain text 100%", new string[0])]
    [TestCase("<Please select a definition>", new string[0])]
    public void PlaceholderTokenizationRecognizesContracts(string value, string[] expected)
    {
        Assert.That(Tokenize(value), Is.EquivalentTo(expected));
    }

    /// <summary>
    /// Tests that missing, altered and duplicated contract tokens cannot pass comparison.
    /// </summary>
    /// <param name="neutral">The original value.</param>
    /// <param name="translation">A translation with a broken contract.</param>
    [TestCase("{0}", "{1}")]
    [TestCase("[{mapName}] Invasion!", "[{wrongName}] Invasion!")]
    [TestCase("{0} {0}", "{0}")]
    [TestCase("%ls", "%s")]
    [TestCase("%s", "")]
    [TestCase("{{0}}", "{0}")]
    [TestCase("<b>{0}</b>", "<i>{0}</i>")]
    [TestCase("<a href=\"/help\">Help</a>", "<a href=\"/other\">Help</a>")]
    public void PlaceholderComparisonRejectsContractChanges(string neutral, string translation)
    {
        Assert.That(Tokenize(translation), Is.Not.EquivalentTo(Tokenize(neutral)));
    }

    /// <summary>
    /// Tests that empty and whitespace values are reported by their resource key.
    /// </summary>
    /// <param name="value">An invalid translation value.</param>
    [TestCase("")]
    [TestCase(" \t\r\n")]
    public void EmptyValueDetectionNamesTheKey(string value)
    {
        Assert.That(EmptyValueKeys(new Dictionary<string, string> { ["MissingTranslation"] = value, ["Translated"] = "번역" }), Is.EqualTo(new[] { "MissingTranslation" }));
    }

    private static IEnumerable<string> NeutralResourcePaths()
    {
        var root = RepositoryRoot();
        return Directory.EnumerateFiles(Path.Combine(root, "src"), "*.resx", SearchOption.AllDirectories)
            .Where(path => !Path.GetRelativePath(root, path).Split(Path.DirectorySeparatorChar).Any(part => part is "bin" or "obj"))
            .Where(IsNeutralResource)
            .Where(path => ReadStringEntries(XDocument.Load(path, LoadOptions.PreserveWhitespace)).Count > 0)
            .Select(path => Path.GetRelativePath(root, path).Replace('\\', '/'))
            .Order(StringComparer.Ordinal)
            .ToArray();
    }

    private static bool IsNeutralResource(string path)
    {
        var name = Path.GetFileNameWithoutExtension(path);
        var separator = name.LastIndexOf('.');
        if (separator < 0)
        {
            return true;
        }

        try
        {
            return CultureInfo.GetCultureInfo(name[(separator + 1)..]).Equals(CultureInfo.InvariantCulture);
        }
        catch (CultureNotFoundException)
        {
            return true;
        }
    }

    private static Dictionary<string, string> ReadStringEntries(XDocument document)
    {
        var root = document.Root ?? throw new InvalidDataException("Resource XML has no root element.");
        return root.Elements("data")
            .Where(entry => entry.Attribute("mimetype") is null)
            .Where(entry => entry.Attribute("type") is not { } type || type.Value.Split(',')[0].Trim() == "System.String")
            .ToDictionary(entry => (string?)entry.Attribute("name") ?? throw new InvalidDataException("Resource string has no name."), entry => (string?)entry.Element("value") ?? string.Empty, StringComparer.Ordinal);
    }

    private static Dictionary<string, string> ReadKoreanStringEntries(string neutralPath)
    {
        var path = KoreanPath(neutralPath);
        Assert.That(File.Exists(Path.Combine(RepositoryRoot(), path)), Is.True, $"Missing Korean resource file: {path} (neutral: {neutralPath})");
        return ReadStringEntries(LoadResource(path));
    }

    private static XDocument LoadResource(string path) => XDocument.Load(Path.Combine(RepositoryRoot(), path), LoadOptions.PreserveWhitespace);

    private static string KoreanPath(string neutralPath) => Path.ChangeExtension(neutralPath, "ko.resx");

    private static string[] EmptyValueKeys(IReadOnlyDictionary<string, string> entries) => entries
        .Where(entry => string.IsNullOrWhiteSpace(entry.Value))
        .Select(entry => entry.Key)
        .Order(StringComparer.Ordinal)
        .ToArray();

    private static string[] Tokenize(string value) => new[] { BraceTokens, PrintfTokens, MarkupTokens }
        .SelectMany(expression => expression.Matches(value).Select(match => match.Value))
        .ToArray();

    private static string RepositoryRoot()
    {
        for (var directory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AGENTS.md")) && Directory.Exists(Path.Combine(directory.FullName, "src")))
            {
                return directory.FullName;
            }
        }

        throw new DirectoryNotFoundException($"Cannot locate the OpenMU source tree above {TestContext.CurrentContext.TestDirectory}.");
    }
}
