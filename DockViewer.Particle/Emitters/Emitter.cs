using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Effect.Lib
{
    abstract public class Emitter : Control
    {
        #region DependencyProperties

        /// <summary>
        /// Dependency Property which maintains the maximum number of paritcles an emitter can have.
        /// </summary>
        static readonly DependencyProperty MaxParticlesProperty = DependencyProperty.Register(
            "MaxParticles", typeof(int), typeof(Emitter), new PropertyMetadata(0));
        public int MaxParticles 
        {
            get { return (int)GetValue(MaxParticlesProperty); }
            set { SetValue(MaxParticlesProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum number of paritcles an emitter can have.
        /// </summary>
        static readonly DependencyProperty MinParticlesProperty = DependencyProperty.Register(
            "MinParticles", typeof(int), typeof(Emitter), new PropertyMetadata(0));
        public int MinParticles
        {
            get { return (int)GetValue(MinParticlesProperty); }
            set { SetValue(MinParticlesProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum velocity of a particle in the x-direction.
        /// </summary>
        static readonly DependencyProperty MaxHorizontalVelocityProperty = DependencyProperty.Register(
            "MaxHorizontalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));
        public double MaxHorizontalVelocity
        {
            get { return (double)GetValue(MaxHorizontalVelocityProperty); }
            set { SetValue(MaxHorizontalVelocityProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum velocity of a particle in the x-direction.
        /// </summary>
        static readonly DependencyProperty MinHorizontalVelocityProperty = DependencyProperty.Register(
            "MinHorizontalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));
        public double MinHorizontalVelocity
        {
            get { return (double)GetValue(MinHorizontalVelocityProperty); }
            set { SetValue(MinHorizontalVelocityProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum velocity of a particle in the x-direction.
        /// </summary>
        static readonly DependencyProperty MaxVerticalVelocityProperty = DependencyProperty.Register(
            "MaxVerticalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));
        public double MaxVerticalVelocity
        {
            get { return (double)GetValue(MaxVerticalVelocityProperty); }
            set { SetValue(MaxVerticalVelocityProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum velocity of a particle in the y-direction.
        /// </summary>
        static readonly DependencyProperty MinVerticalVelocityProperty = DependencyProperty.Register(
            "MinVerticalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));
        public double MinVerticalVelocity
        {
            get { return (double)GetValue(MinVerticalVelocityProperty); }
            set { SetValue(MinVerticalVelocityProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum lifespan of a particle.
        /// </summary>
        static readonly DependencyProperty MaxLifeSpanProperty = DependencyProperty.Register(
            "MaxLifeSpan", typeof(double), typeof(Emitter), new PropertyMetadata(double.MaxValue));
        public double MaxLifeSpan
        {
            get { return (double)GetValue(MaxLifeSpanProperty); }
            set { SetValue(MaxLifeSpanProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum lifespan of a particle.
        /// </summary>
        static readonly DependencyProperty MinLifeSpanProperty = DependencyProperty.Register(
            "MinLifeSpan", typeof(double), typeof(Emitter), new PropertyMetadata(double.MaxValue));
        public double MinLifeSpan
        {
            get { return (double)GetValue(MinLifeSpanProperty); }
            set { SetValue(MinLifeSpanProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum mass of a particle.
        /// </summary>
        static readonly DependencyProperty MaxMassProperty = DependencyProperty.Register(
            "MaxMass", typeof(double), typeof(Emitter), new PropertyMetadata(2.0d));
        public double MaxMass
        {
            get { return (double)GetValue(MaxMassProperty); }
            set { SetValue(MaxMassProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum mass of a particle.
        /// </summary>
        static readonly DependencyProperty MinMassProperty = DependencyProperty.Register(
            "MinMass", typeof(double), typeof(Emitter), new PropertyMetadata(1.0d));
        public double MinMass
        {
            get { return (double)GetValue(MinMassProperty); }
            set { SetValue(MinMassProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum width of a particle.
        /// </summary>
        static readonly DependencyProperty MaxParticleWidthProperty = DependencyProperty.Register(
            "MaxParticleWidth", typeof(double), typeof(Emitter), new PropertyMetadata(8.0d));
        public double MaxParticleWidth
        {
            get { return (double)GetValue(MaxParticleWidthProperty); }
            set { SetValue(MaxParticleWidthProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum width of a particle.
        /// </summary>
        static readonly DependencyProperty MinParticleWidthProperty = DependencyProperty.Register(
            "MinParticleWidth", typeof(double), typeof(Emitter), new PropertyMetadata(2.0d));
        public double MinParticleWidth
        {
            get { return (double)GetValue(MinParticleWidthProperty); }
            set { SetValue(MinParticleWidthProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum height of a particle.
        /// </summary>
        static readonly DependencyProperty MaxParticleHeightProperty = DependencyProperty.Register(
            "MaxParticleHeight", typeof(double), typeof(Emitter), new PropertyMetadata(10.0d));
        public double MaxParticleHeight
        {
            get { return (double)GetValue(MaxParticleHeightProperty); }
            set { SetValue(MaxParticleHeightProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum height of a particle.
        /// </summary>
        static readonly DependencyProperty MinParticleHeightProperty = DependencyProperty.Register(
            "MinParticleHeight", typeof(double), typeof(Emitter), new PropertyMetadata(4.0d));
        public double MinParticleHeight
        {
            get { return (double)GetValue(MinParticleHeightProperty); }
            set { SetValue(MinParticleHeightProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the maximum offset for the position of a particle.
        /// </summary>
        static readonly DependencyProperty MaxPositionOffsetProperty = DependencyProperty.Register(
            "MaxPositionOffset", typeof(double), typeof(Emitter), new PropertyMetadata(0.01d));
        public double MaxPositionOffset
        {
            get { return (double)GetValue(MaxPositionOffsetProperty); }
            set { SetValue(MaxPositionOffsetProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the minimum offset for the position of a particle.
        /// </summary>
        static readonly DependencyProperty MinPositionOffsetProperty = DependencyProperty.Register(
            "MinPositionOffset", typeof(double), typeof(Emitter), new PropertyMetadata(-0.01d));
        public double MinPositionOffset
        {
            get { return (double)GetValue(MinPositionOffsetProperty); }
            set { SetValue(MinPositionOffsetProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the starting opacity of a particle.
        /// </summary>
        static readonly DependencyProperty StartOpacityProperty = DependencyProperty.Register(
            "StartOpacity", typeof(double), typeof(Emitter), new PropertyMetadata(0.3));
        public double StartOpacity
        {
            get { return (double)GetValue(StartOpacityProperty); }
            set { SetValue(StartOpacityProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the opacity of a particle at end-state.
        /// </summary>
        static readonly DependencyProperty EndOpacityProperty = DependencyProperty.Register(
            "EndOpacity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));
        public double EndOpacity
        {
            get { return (double)GetValue(EndOpacityProperty); }
            set { SetValue(EndOpacityProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the ColorKeyFrameCollection of a particle, Used by the
        /// particle to determine the background color over time.
        /// </summary>
        static readonly DependencyProperty ColorKeyFramesProperty = DependencyProperty.Register(
            "ColorKeyFrames", typeof(ColorKeyFrameCollection), typeof(Emitter),
            new PropertyMetadata(new ColorKeyFrameCollection()));
        public ColorKeyFrameCollection ColorKeyFrames
        {
            get { return (ColorKeyFrameCollection)GetValue(ColorKeyFramesProperty); }
            set { SetValue(ColorKeyFramesProperty, value); }
        }

        #endregion


        public ParticleSystem ParticleSystem
        {
            get;
            set;
        }

        #region Virtual Methods

        /// <summary>
        /// Generates the particles for an emitter, called once at creation by the Particle System.
        /// </summary>
        /// <param name="system"></param>
        virtual public void GenerateParticles(ParticleSystem system)
        {
            this.ParticleSystem = system;

            // Init the particles for the emitter
            for (int i = 0; i < MaxParticles; i++)
            {
                Particle mParticle = new Particle();
                UpdateParticle(mParticle);
                this.AddParticle(system, mParticle);
                system.Particles.Add(mParticle); 
            }
        }

        /// <summary>
        /// Method which is used to perform additional processing when adding a particle to the system for the
        /// first time.
        /// </summary>
        /// <param name="system"></param>
        /// <param name="particle"></param>
        virtual public void AddParticle(ParticleSystem system, Particle particle) {}
        
        /// <summary>
        /// Updates the particle by reinitializing its parameters. Used when a particle is first created and 
        /// when a particle is resurrected.
        /// </summary>
        /// <param name="particle"></param>
        virtual public void UpdateParticle(Particle particle) 
        {
            particle.Owner = this;
            particle.Mass = ParticleSystem.random.NextDouble(MinMass, MaxMass);
            particle.StartOpacity = ParticleSystem.random.NextDouble(this.StartOpacity / 2, this.StartOpacity);
            particle.EndOpacity = this.EndOpacity;
            particle.Force = new Vector(0, 0);
            particle.Velocity = new Vector(
                ParticleSystem.random.NextDouble(MinHorizontalVelocity, MaxHorizontalVelocity),
                ParticleSystem.random.NextDouble(MinVerticalVelocity, MaxVerticalVelocity));
            particle.LifeSpan = ParticleSystem.random.NextDouble(MinLifeSpan, MaxLifeSpan);
            particle.BackgroundColors = this.ColorKeyFrames;
        }

        #endregion
    }
}
