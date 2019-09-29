using System.Windows;
using System.Windows.Controls;

namespace Effect.Lib
{
    public class PointEmitter : Emitter
    {
        #region Properties

        /// <summary>
        /// Dependency Property which maintains the X-Coord for this Emitter
        /// </summary>
        static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof(double), typeof(PointEmitter), new PropertyMetadata(0d));
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y-Coord for this Emitter
        /// </summary>
        static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof(double), typeof(PointEmitter), new PropertyMetadata(0d));
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// 
        /// </summary>
        static PointEmitter()
        {
            
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PointEmitter), new FrameworkPropertyMetadata(typeof(PointEmitter)));
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Init the width and height to one and set the canvas left and top 
        /// </summary>
        public override void EndInit()
        {
            base.EndInit();

            // Point emitter should always be 1x1
            this.Width = .5;
            this.Height = .5;

            Canvas.SetLeft(this, X);
            Canvas.SetTop(this, Y);
        }

        /// <summary>
        /// Add a particle to the system.
        /// </summary>
        /// <param name="system"></param>
        /// <param name="particle"></param>
        public override void AddParticle(ParticleSystem system, Particle particle) 
        {
            base.AddParticle(system, particle);
                        
            // overwrite the position based on the emitters X and Y position
            particle.Position = new Point(this.X + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset),
                this.Y + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset)); 
       
        }

        /// <summary>
        /// Update the particle
        /// </summary>
        /// <param name="particle"></param>
        public override void UpdateParticle(Particle particle)
        {
            base.UpdateParticle(particle);
            
            // Update the particles position
            particle.Position = new Point(this.X + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset),
                this.Y + ParticleSystem.random.NextDouble(MinPositionOffset, MaxPositionOffset));
         
        }

        #endregion
    }
}
