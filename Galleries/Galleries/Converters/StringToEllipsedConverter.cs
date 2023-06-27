namespace MOROPO.FCBC.Galleries.Converters
{
    public class StringToEllipsedConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var limit = int.Parse(parameter.ToString());

            if (value.ToString().Length <= limit) return value;

            return string.Format("{0} ...", value.ToString().Substring(0, limit - 4));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

