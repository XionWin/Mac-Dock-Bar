using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockViewer.Lib
{
    public struct PointF
    {
        public PointF(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public double X;
        public double Y;
    }
}
