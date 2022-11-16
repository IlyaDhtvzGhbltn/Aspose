using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Imaging;
using System.Collections.Generic;


namespace Aspore.Drawing.Extensions
{
	public class BitmapWrapper : IDisposable, IEnumerable<Point>
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		/// <summary>
		/// Original bitmap byte arr
		/// </summary>
		private byte[] data;
		/// <summary>
		/// Gaussed bitmap byte arr
		/// </summary>
		private byte[] outData;
		private int stride;
		private BitmapData bmpData;
		private Bitmap bmp;


		public BitmapWrapper(Bitmap bmp)
		{
			Width = bmp.Width;
			Height = bmp.Height;
			this.bmp = bmp;

			bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			stride = bmpData.Stride;

			data = new byte[stride * Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

			outData = new byte[stride * Height];
		}

		/// <summary>
		/// Returns pixel's color from target bitmap
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Color this[int x, int y]
		{
			get
			{
				int i = GetIndex(x, y);
				return Color.FromArgb(data[i], data[i], data[i], data[i]);
			}
			set
			{
				var i = GetIndex(x, y);
				if (i >= 0)
				{
					outData[i] = value.B;
					outData[i + 1] = value.G;
					outData[i + 2] = value.R;
					outData[i + 3] = value.A;
				};
			}
		}

		public Color this[Point p]
		{
			get { return this[p.X, p.Y]; }
			set { this[p.X, p.Y] = value; }
		}


		/// <summary>
		/// Writes the new color value to the output buffer.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public void SetPixel(Point p, double r, double g, double b)
		{
			r = r < 0 ? 0 : r;
			r = r >= 256 ? 255 : r;
			g = g < 0 ? 0 : g;
			g = g >= 256 ? 255 : g;
			b = b < 0 ? 0 : b;
			b = b >= 256 ? 255 : b;

			this[p.X, p.Y] = Color.FromArgb((int)r, (int)g, (int)b);
		}

		int GetIndex(int x, int y)
		{
			return (x < 0 || x >= Width || y < 0 || y >= Height) ? -1 : x * 4 + y * stride;
		}


		public void Dispose()
		{
			System.Runtime.InteropServices.Marshal.Copy(outData, 0, bmpData.Scan0, outData.Length);
			bmp.UnlockBits(bmpData);
		}

		public IEnumerator<Point> GetEnumerator()
		{
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					yield return new Point(x, y);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
