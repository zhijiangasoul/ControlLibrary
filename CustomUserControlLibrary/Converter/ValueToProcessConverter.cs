using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomUserControlLibrary.Converter
{
  public  class ValueToProcessConverter : IValueConverter
    {

        private const double Thickness = 10;
        private const double WarnValue = 60;
        private static readonly SolidColorBrush NormalBrush;
        private static readonly SolidColorBrush WarnBrush;
        private static readonly SolidColorBrush BackBrush;
        private static readonly SolidColorBrush TipsBrush;
        private Point centerPoint;
        private double radius;

        static ValueToProcessConverter()
        {
            NormalBrush = new SolidColorBrush(Color.FromRgb(4, 173, 179));
            WarnBrush = new SolidColorBrush(Color.FromRgb(4, 173, 179));
            BackBrush = new SolidColorBrush(Color.FromRgb(52, 59, 68));
            TipsBrush = new SolidColorBrush(Color.FromRgb(199, 46, 82));
        }
      
        /// <summary>
        /// 实现 IValueConverter接口 接收来自组件value值。改变当前进度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double && !string.IsNullOrEmpty((string)parameter))
            {
                double FcurrVal = (double)value;
                double width = double.Parse((string)parameter);
                radius = width / 2;
                centerPoint = new Point(radius, radius);
                return DrawBrush(FcurrVal, 100, radius, radius, Thickness);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据当前值和最大值画出进度条
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private Brush DrawBrush(double FcurrVal, double maxValue, double radiusX, double radiusY, double thickness)
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            DrawingContext drawingContext = drawingGroup.Open();
            DrawingGeometry(drawingContext, FcurrVal, maxValue, radiusX, radiusY, thickness);
            DrawingBrush brush = new DrawingBrush(drawingGroup);
            return brush;

        }
        private void DrawingGeometry(DrawingContext drawingContext, double FcurrVal, double maxValue, double radiusX, double radiusY, double thickness)
        {
            if (FcurrVal != maxValue)
            {
                drawingContext.DrawEllipse(null, new Pen(BackBrush, thickness), centerPoint, radiusX, radiusY);
                SolidColorBrush brush;
                if (FcurrVal < WarnValue)
                {
                    brush = WarnBrush;
                }
                else
                {
                    brush = NormalBrush;
                }
                Geometry geometry = GetGeometry(FcurrVal, maxValue, radiusX + 5, radiusY + 5, thickness);
                drawingContext.DrawGeometry(brush, new Pen(), geometry);
            }
            else
            {
                drawingContext.DrawEllipse(null, new Pen(TipsBrush, thickness), centerPoint, radiusX, radiusY);
            }
            drawingContext.Close();
        }

        /// <summary>
        /// 根据当前值和最大值获取扇形
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private Geometry GetGeometry(double value, double maxValue, double radiusX, double radiusY, double thickness)
        {

            bool isLargeArc = false;
            double percent = value / maxValue;
            double angel = percent * 360D;
            if (angel > 180)
                isLargeArc = true;
            double bigR = radiusX;
            double smallR = radiusX - thickness;
            Point firstpoint = GetPointByAngel(centerPoint, bigR, 0);
            Point secondpoint = GetPointByAngel(centerPoint, bigR, angel);
            Point thirdpoint = GetPointByAngel(centerPoint, smallR, 0);
            Point fourpoint = GetPointByAngel(centerPoint, smallR, angel);
            return DrawingArcGeometry(firstpoint, secondpoint, thirdpoint, fourpoint, bigR, smallR, isLargeArc);
        }

        /// <summary>
        /// 根据角度获取坐标
        /// </summary>
        /// <param name="CenterPoint"></param>
        /// <param name="r"></param>
        /// <param name="angel"></param>
        /// <returns></returns>
        private Point GetPointByAngel(Point CenterPoint, double r, double angel)
        {
            Point p = new Point();
            p.X = Math.Sin(angel * Math.PI / 180) * r + CenterPoint.X;
            p.Y = CenterPoint.Y - Math.Cos(angel * Math.PI / 180) * r;
            return p;

        }

        /// <summary>
        /// 根据4个坐标画出扇形
        /// </summary>
        /// <param name="bigFirstPoint"></param>
        /// <param name="bigSecondPoint"></param>
        /// <param name="smallFirstPoint"></param>
        /// <param name="smallSecondPoint"></param>
        /// <param name="bigRadius"></param>
        /// <param name="smallRadius"></param>
        /// <param name="isLargeArc"></param>
        /// <returns></returns>
        private Geometry DrawingArcGeometry(Point bigFirstPoint, Point bigSecondPoint, Point smallFirstPoint, Point smallSecondPoint, double bigRadius, double smallRadius, bool isLargeArc)
        {

            PathFigure pathFigure = new PathFigure
            {
                IsClosed = true
            };
            pathFigure.StartPoint = bigFirstPoint;
            pathFigure.Segments.Add(
              new ArcSegment
              {
                  Point = bigSecondPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(bigRadius, bigRadius),
                  SweepDirection = SweepDirection.Clockwise

              });
            pathFigure.Segments.Add(new LineSegment
            {
                Point = smallSecondPoint
            });
            pathFigure.Segments.Add(
             new ArcSegment
             {

                 Point = smallFirstPoint,
                 IsLargeArc = isLargeArc,
                 Size = new Size(smallRadius, smallRadius),
                 SweepDirection = SweepDirection.Counterclockwise

             });
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;

        }

    }
}
