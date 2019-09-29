using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace DockViewer.Lib
{
    public interface IAnimation
    {
        void AnimateTo(double x, double y, double sx, double sy, double r, double duration);

        DoubleAnimation MakeAnimation(double to, double duration);
        DoubleAnimation MakeAnimation(double to, double duration, EventHandler endEvent);
        void AnimationCompleted(object sender, EventArgs e);
    }
}
