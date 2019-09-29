using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DockViewer.Lib
{
    public class TestViewer : Control
    {
        private Dock.Core.Gauss gauss = new Dock.Core.Gauss();
        public TestViewer()
        {
            this.SnapsToDevicePixels = true;

            gauss.Sigma = 128;
            gauss.Swing = 72;
            gauss.Offset = 88;

            this.Loaded += (o, e) =>
            {
                this.ReSetView();
            };
            this.SizeChanged += (o, e) =>
            {
                this.ReSetView();
            };
        }

        static TestViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TestViewer), new FrameworkPropertyMetadata(typeof(TestViewer)));
        }



        #region Graphic Helper



        private PointF[] points = null;
        public PointF[] Points
        {
            get
            {
                return this.points;
            }
            set
            {
                this.points = value;
                this.InvalidateVisual();
            }
        }


        private PointF[] points2 = null;
        public PointF[] Points2
        {
            get
            {
                return this.points2;
            }
            set
            {
                this.points2 = value;
            }
        }

        private double ps2Max = 0;

        public void RenderView(Point p)
        {
            this.gauss.Phase = p.X;
            Queue<PointF> ps = new Queue<PointF>();
            Queue<PointF> ps2 = new Queue<PointF>();

            var maxY = this.gauss.GaussFunction(p.X);
            DateTime sl = DateTime.Now;
            for (double i = 0; i <= this.ActualWidth; i += 1)
            {
                ps.Enqueue(new PointF(i, this.gauss.GaussFunction(i)));
            }

            {
                ps2Max = this.gauss.IntegrateGauss(this.gauss.Phase, int.MaxValue);
                double y = this.gauss.IntegrateGauss(this.gauss.Phase, this.ActualWidth);
                ps2.Enqueue(new PointF(this.gauss.Phase, y));
                y = this.gauss.IntegrateGauss(0, this.gauss.Phase);
                ps2.Enqueue(new PointF(this.gauss.Phase, 0 - y));
            }
            DateTime nl = DateTime.Now;

            this.Points = ps.ToArray();
            this.Points2 = ps2.ToArray();

        }
        public void ReSetView()
        {
            Queue<PointF> ps = new Queue<PointF>();

            for (double i = 0; i <= this.ActualWidth; i += 1)
            {
                ps.Enqueue(new PointF(i, this.gauss.Offset));
            }

            this.Points = ps.ToArray();
            this.Points2 = null;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            

            drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, this.ActualHeight - this.gauss.Offset, this.ActualWidth, 0.5));
            if (this.Points == null)
            {
                return;
            }
            foreach (var p in this.Points)
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(p.X, this.ActualHeight - p.Y - 1, 1, 1));
            }

            if (this.Points2 == null)
            {
                return;
            }


            for (int i = 0; i < 2; i++)
            {
                PointF p = this.Points2[i];
                double offset = (p.Y / ps2Max) * this.gauss.Swing + this.gauss.Offset;
                drawingContext.DrawRectangle(i == 0 ? Brushes.White : Brushes.White, null, new Rect(p.X, this.ActualHeight - 3 - offset, 6, 6));
            }
            
        }


        #endregion Graphic Helper
    }
}
