namespace MOROPO.FCBC.Galleries.Converters
{
    public class MediaObjectToViewContentConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            if (value is Models.View.ImageViewModel)
            {
                return new Views.ExibitImageView(value as Models.View.ImageViewModel);
            }

            if (value is SGI.API.Common.Models.VideoDisplayApiModel)
            {
                return new Views.ExibitVideoView(value as SGI.API.Common.Models.VideoDisplayApiModel);
            }

            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
