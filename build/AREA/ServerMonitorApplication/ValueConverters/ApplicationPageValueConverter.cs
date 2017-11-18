using System;
using System.Diagnostics;
using System.Globalization;

namespace ServerMonitorApplication
{
    /// <summary>
    /// Convert the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Connection:
                    return new ConnectionPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
