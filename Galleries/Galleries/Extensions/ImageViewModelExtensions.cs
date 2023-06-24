using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.Galleries.Extensions
{
	public static class ImageViewModelExtensions
	{
		private const double THRESHOLD = 0.25;

        private static readonly List<int> SIZES = new List<int>(new int[] { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096 });

        public static void PowerOfTwoResize(this Models.View.ImageViewModel target)
		{
			var currentLargeDimension = System.Math.Max(target.Width, target.Height);

			var wantedLargeDimension = System.Math.Max((int)GetCloseSize(target.Width), (int)GetCloseSize(target.Height));

			if (wantedLargeDimension < currentLargeDimension)
			{
				if ((currentLargeDimension - wantedLargeDimension) > (int)(wantedLargeDimension * THRESHOLD))
				{
					wantedLargeDimension = SIZES.ElementAt(SIZES.IndexOf(wantedLargeDimension) + 1);
				}
			}

			using (var surfaceBitmap = new SkiaSharp.SKBitmap(wantedLargeDimension, wantedLargeDimension))
			{
				using (var canvas = new SkiaSharp.SKCanvas(surfaceBitmap))
				{
					canvas.Clear();

					using (var imageData = SkiaSharp.SKData.CreateCopy(target.Data))
					{
						using (var image = SkiaSharp.SKImage.FromEncodedData(imageData))
						{
							canvas.DrawImage(image, (wantedLargeDimension - target.Width) / 2, (wantedLargeDimension - target.Height) / 2);
						}
					}
				}

				using (var spriteData = surfaceBitmap.Encode(SkiaSharp.SKEncodedImageFormat.Png, 75))
				{
					target.Data = spriteData.ToArray();
				}
			}

			System.Diagnostics.Debug.WriteLine(string.Format("WantedDimension: {0}", wantedLargeDimension));
		}

		private static double GetCloseSize(double size)
		{
			return SIZES.ElementAt(SIZES.Count - SIZES.Select(po2size => po2size - size).Where(item => item > 0).Count());
		}
	}
}

