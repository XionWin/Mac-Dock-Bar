using System.Windows;

namespace Effect.Lib
{
    public class CustomerEmitter : Emitter
    {
        public CustomerEmitter()
        {
        }

        

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        static CustomerEmitter()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomerEmitter), new FrameworkPropertyMetadata(typeof(CustomerEmitter)));
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
            double x = ParticleSystem.random.NextDouble(0, this.ParticleSystem.ActualWidth);
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
            double x = ParticleSystem.random.NextDouble(0, this.ParticleSystem.ActualWidth);
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
        /// 
        private static System.Random _r = new System.Random();
        private double LinearEquation(double x)
        {
            double y = System.Math.Abs(this.ParticleSystem.ActualHeight - 0) * _r.NextDouble() + 0;
            return y;
        }

        #endregion
    }
}
