// <copyright company="VDSoft" file="InversBoolConverter.cs">
//    Copyright (C) VDSoft. All rights reserved. Confidential.
// </copyright>

using System.Globalization;
using System.Windows.Data;

namespace VDsoft.TextProcessor.Ui.Converter;

/// <summary>
/// Inverts a boolean value.
/// </summary>
/// <seealso cref="IValueConverter" />
public class InverstBoolConverter : IValueConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is bool boolValue ? !boolValue : (object)false;

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
    /// </returns>
    /// <exception cref="NotImplementedException">Backwards conversion is not supported.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException("Backwards conversion is not supported.");
    }
}
