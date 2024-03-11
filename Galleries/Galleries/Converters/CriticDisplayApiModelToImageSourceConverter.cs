namespace MOROPO.FCBC.Galleries.Converters
{
    public class CriticDisplayApiModelToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var target = value as SGI.API.Common.Models.CriticDisplayApiModel;

            if (target != null)
            {
                if (target.ImageId != null)
                {
                    return Xamarin.Forms.ImageSource.FromUri(new System.Uri(string.Format("{0}media/images/{1}.{2}", Constants.Networking.SGI_BASE_URL, target.ImageId, target.ImageExtension)));
                }
                else if (target.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Famale)
                {
                    return Providers.ImageSourceProvider.Instance.ProfileFemaleDefault();
                }

                return Providers.ImageSourceProvider.Instance.ProfileMaleDefault();
            }

            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
