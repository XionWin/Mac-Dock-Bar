using System.Windows;
using System.Collections.Generic;

namespace Effect.Lib
{
    /// <summary>
    /// A class which creates and maintains a number of meatballs based on the MaxParticle property of an emitter. 
    /// </summary>
    public class MeatballEmitter : Emitter
    {
        #region Properties

        /// <summary>
        /// Dependency Property which maintains the X1 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty X1Property = DependencyProperty.Register(
           "X1", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(0d));
        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y1 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register(
            "Y1", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(0d));
        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the X2 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty X2Property = DependencyProperty.Register(
           "X2", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(0d));
        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y2 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register(
            "Y2", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(0d));
        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum Spring Constant (ks) for the mass-spring system used
        /// to create the Meatball
        /// </summary>
        public static readonly DependencyProperty MaxSpringConstantProperty = DependencyProperty.Register(
           "MaxSpringConstant", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(1.0d));
        public double MaxSpringConstant
        {
            get { return (double)GetValue(MaxSpringConstantProperty); }
            set { SetValue(MaxSpringConstantProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum Spring Constant (ks) for the mass-spring system used
        /// to create the Meatball
        /// </summary>
        public static readonly DependencyProperty MinSpringConstantProperty = DependencyProperty.Register(
           "MinSpringConstant", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(1.0d));
        public double MinSpringConstant
        {
            get { return (double)GetValue(MinSpringConstantProperty); }
            set { SetValue(MinSpringConstantProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum Dampening Constant (kd) for the mass-spring system used
        /// to create the Meatball
        /// </summary>
        public static readonly DependencyProperty MaxDampeningConstantProperty = DependencyProperty.Register(
           "MaxDampeningConstant", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(0.3d));
        public double MaxDampeningConstant
        {
            get { return (double)GetValue(MaxDampeningConstantProperty); }
            set { SetValue(MaxDampeningConstantProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum Dampening Constant (kd) for the mass-spring system used
        /// to create the Meatball
        /// </summary>
        public static readonly DependencyProperty MinDampeningConstantProperty = DependencyProperty.Register(
           "MinDampeningConstant", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(0.1d));
        public double MinDampeningConstant
        {
            get { return (double)GetValue(MinDampeningConstantProperty); }
            set { SetValue(MinDampeningConstantProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum Rest Length (r) for the mass-spring system used
        /// to create the Meatball
        /// </summary>
        public static readonly DependencyProperty MaxRestLengthProperty = DependencyProperty.Register(
           "MaxRestLengthConstant", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(1.0d));
        public double MaxRestLength
        {
            get { return (double)GetValue(MaxRestLengthProperty); }
            set { SetValue(MaxRestLengthProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum Rest Length (r) for the mass-spring system used
        /// to create the Meatball
        /// </summary>
        public static readonly DependencyProperty MinRestLengthProperty = DependencyProperty.Register(
           "MinRestLengthConstant", typeof(double), typeof(MeatballEmitter), new PropertyMetadata(1.0d));
        public double MinRestLength
        {
            get { return (double)GetValue(MinRestLengthProperty); }
            set { SetValue(MinRestLengthProperty, value); }
        }
        
        #endregion

        #region Constructor
        
        /// <summary>
        /// 
        /// </summary>
        static MeatballEmitter()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MeatballEmitter), new FrameworkPropertyMetadata(typeof(MeatballEmitter)));
        }
        
        #endregion

        #region Private Properties

        // A dictionary for quick reference to the meatball which contains a particle
        private Dictionary<Particle, Meatball> mMeatballs = new Dictionary<Particle, Meatball>();

        #endregion

        #region Overrides

        /// <summary>
        /// Generate the particles by calling AddPartcle for a number of meatbals equal to 
        /// the MaxParticles divided by three. 
        /// </summary>
        /// <param name="system"></param>
        public override void GenerateParticles(ParticleSystem system)
        {
            //base.GenerateParticles(system);
            
            int totalConnections = MaxParticles;
            if (Meatball.NumberOfConnections > 0)
                totalConnections = MaxParticles / Meatball.NumberOfConnections;

            // Init the particles for the emitter
            Particle p = null;
            for (int i = 0; i < totalConnections; i++)
            {
                AddParticle(system, p);
            }
        }

        /// <summary>
        /// Create a meatball which adds three particles with springs to the system. 
        /// </summary>
        /// <param name="system"></param>
        /// <param name="particle"></param>
        public override void AddParticle(ParticleSystem system, Particle particle)
        {
            //base.AddParticle(system, particle); // ignore the base

            double x = ParticleSystem.random.NextDouble(X1, X2);
            Meatball meatball = new Meatball(system, this, x, LinearEquation(x),
                MinSpringConstant, MaxSpringConstant, MinDampeningConstant, MaxDampeningConstant,
                MinRestLength, MaxRestLength, true);
            // add each particle to the dictionary and its associated meatball.
            foreach (Particle p in meatball.Particles)
            {
                mMeatballs.Add(p, meatball);
            }
        }

        /// <summary>
        /// Update the particle. Ignore the base and use the meatball update. 
        /// </summary>
        /// <param name="particle"></param>
        public override void UpdateParticle(Particle particle)
        {
            //base.UpdateParticle(particle);
                        
            double x = ParticleSystem.random.NextDouble(X1, X2);
            // Get the meatball this particle is a part of.
            ((Meatball)mMeatballs[particle]).Update(this, x, LinearEquation(x));                       
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
