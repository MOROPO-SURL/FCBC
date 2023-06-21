namespace MOROPO.FCBC.Galleries.Converters
{
    public class ExibitDetailPageHeightToImageViewerHeightConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Xamarin.Forms.Application.Current.MainPage.Width < Xamarin.Forms.Application.Current.MainPage.Height)
            {
                return value;
            }

            var concreteValue = (double)value;

            var aspectHeight = concreteValue * Xamarin.Forms.Application.Current.MainPage.Height / Xamarin.Forms.Application.Current.MainPage.Width;

            return aspectHeight - aspectHeight * 0.1;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
