using System.Windows;

namespace Effect.Lib
{
    public class LineEmitter : Emitter
    {
        #region Properties

        /// <summary>
        /// Dependency Property which maintains the X1 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty X1Property = DependencyProperty.Register(
           "X1", typeof(double), typeof(LineEmitter), new PropertyMetadata(0d));
        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y1 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register(
            "Y1", typeof(double), typeof(LineEmitter), new PropertyMetadata(0d));
        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the X2 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty X2Property = DependencyProperty.Register(
           "X2", typeof(double), typeof(LineEmitter), new PropertyMetadata(0d));
        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y2 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register(
            "Y2", typeof(double), typeof(LineEmitter), new PropertyMetadata(0d));
        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        static LineEmitter()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineEmitter), new FrameworkPropertyMetadata(typeof(LineEmitter)));
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Add a particle to the system
        /// </summary>
        /// <param name="system"></param>
        /// <param name="particle"></param>
        public override void AddParticle(ParticleSystem system, Particle particle)
        {
            base.AddParticle(system, particle);

            // pick a random X between X1 and X2 
            // then get the corresponding y
            double x = ParticleSystem.random.NextDouble(X1, X2);
            particle.Position = new Point(x + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset), 
                LinearEquation(x) + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset)); 
        }

        /// <summary>
        /// Update the particle
        /// </summary>
        /// <param name="particle"></param>
        public override void UpdateParticle(Particle particle)
        {
            base.UpdateParticle(particle);                      

            // Find a new x and corresponding y 
            double x = ParticleSystem.random.NextDouble(X1, X2);
            particle.Position = new Point(x + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset), 
                LinearEquation(x) + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset));
            
        }

        #endregion

        #region Private Methods
         
        /// <summary>
        /// Find a y-coord on a line given an x-coord on the line
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double LinearEquation(double x)
        {
            double m = 0;
            if ((X2 - X1) != 0)
                m = (Y2 - Y1) / (X2 - X1);
            double y = m * x + Y1;
            return y;
        }

        #endregion
    }
}
