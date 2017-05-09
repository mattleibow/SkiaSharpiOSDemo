using System;
using CoreGraphics;
using SkiaSharp;
using SkiaSharp.Views.iOS;

namespace CustomSkiaSharpViews
{
	public class SkiaSharpView : SKCanvasView
	{
		private static readonly SKColor XamarinDarkBlue = 0xFF2C3E50;

		public SkiaSharpView()
		{
			Initialize();
		}

		public SkiaSharpView(CGRect frame)
			: base(frame)
		{
			Initialize();
		}

		private void Initialize()
		{
			Opaque = false;
		}

		public override void DrawInSurface(SKSurface surface, SKImageInfo info)
		{
			base.DrawInSurface(surface, info);

			// get some shortcuts
			var canvas = surface.Canvas;
			var canvasSize = info.Size;

			// do some math to make the Xamagon fit
			var xamagonBounds = new SKRect(41.6587026f, 56f, 144.34135f, 147f);
			var totalSize = new SKSize(xamagonBounds.Right + xamagonBounds.Left, xamagonBounds.Bottom + xamagonBounds.Top);
			var xamagonCenter = new SKPoint(totalSize.Width / 2f, totalSize.Height / 2f);

			// clear the screen
			canvas.Clear(SKColors.Transparent);

			// scale the screen, as the xamagon is not big enough
			var scale = canvasSize.Width > canvasSize.Height
				? canvasSize.Height / totalSize.Height
				: canvasSize.Width / totalSize.Width;
			canvas.Translate(
				(canvasSize.Width - (totalSize.Width * scale)) / 2f,
				(canvasSize.Height - (totalSize.Height * scale)) / 2f);
			canvas.Scale(scale);

			// draw a colorful Xamagon
			var colors = new[] { SKColors.Cyan, SKColors.Magenta, SKColors.Yellow, SKColors.Cyan };
			using (var paint = new SKPaint())
			using (var shader = SKShader.CreateSweepGradient(xamagonCenter, colors, null))
			using (var path = new SKPath())
			{
				paint.IsAntialias = true;
				paint.Shader = shader;

				path.MoveTo(71.4311121f, 56f);
				path.CubicTo(68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
				path.LineTo(43.0238921f, 97.5342563f);
				path.CubicTo(41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
				path.LineTo(64.5928855f, 143.034271f);
				path.CubicTo(65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
				path.LineTo(114.568946f, 147f);
				path.CubicTo(117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
				path.LineTo(142.976161f, 105.465744f);
				path.CubicTo(144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
				path.LineTo(121.407172f, 59.965729f);
				path.CubicTo(120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
				path.LineTo(71.4311121f, 56f);
				path.Close();

				canvas.DrawPath(path, paint);
			}

			// reset the matrix so we can draw something else
			canvas.ResetMatrix();

			// draw some text at the bottom
			using (var paint = new SKPaint())
			using (var typeface = SKTypeface.FromFamilyName("Courier"))
			{
				paint.IsAntialias = true;
				paint.Typeface = typeface;
				paint.TextSize = 80;
				paint.TextAlign = SKTextAlign.Center;

				canvas.DrawText("#MSBuild", canvasSize.Width / 2f, canvasSize.Height - 50, paint);
			}
		}
	}
}
