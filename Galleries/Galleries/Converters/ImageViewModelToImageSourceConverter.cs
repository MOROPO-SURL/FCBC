namespace MOROPO.FCBC.Galleries.Converters
{
    public class ImageViewModelToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var concreteValue = value as Models.View.ImageViewModel;

            return Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("{0}media/images/{1}.{2}", Constants.Networking.SGI_BASE_URL, concreteValue.Id, concreteValue.Extension)));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
