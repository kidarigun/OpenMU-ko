// <copyright file="TestCulture.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

// The tests check the neutral (English) texts of the components. Without a fixed culture,
// they would render a translation, e.g. Korean on a machine with Korean regional settings.
// Tests about other languages set their culture themselves.
[assembly: SetCulture("en-US")]
[assembly: SetUICulture("en-US")]
