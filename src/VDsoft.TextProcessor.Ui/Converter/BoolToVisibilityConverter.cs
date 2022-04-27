﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VDsoft.TextProcessor.Ui.Converter
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class BoolToVisibilityConverter : IValueConverter
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value switch
            {
                true => Visibility.Visible,
                false => Visibility.Hidden,
            };


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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (Visibility)value switch
            {
                Visibility.Collapsed => false,
                Visibility.Hidden => false,
                Visibility.Visible => true,
                _ => false
            };
    }
}
