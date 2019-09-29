using System;
using System.Windows;
using System.Windows.Data;

namespace Effect.Lib.Forces
{
    public class Spring : Force
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency Property which maintains the constant used to determine the Spring springyness.
        /// </summary>
        public static readonly DependencyProperty SpringConstantProperty = DependencyProperty.Register(
            "SpringConstant", typeof(double), typeof(Spring), new PropertyMetadata(0d));
        public double SpringConstant
        {
            get { return (double)GetValue(SpringConstantProperty); }
            set { SetValue(SpringConstantProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the constant used by the spring to return to rest.
        /// </summary>
        public static readonly DependencyProperty DampingConstantProperty = DependencyProperty.Register(
            "DampingConstant", typeof(double), typeof(Spring), new PropertyMetadata(0d));
        public double DampingConstant
        {
            get { return (double)GetValue(DampingConstantProperty); }
            set { SetValue(DampingConstantProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the length at which this spring is at rest.
        /// </summary>
        public static readonly DependencyProperty RestLengthProperty = DependencyProperty.Register(
            "RestLength", typeof(double), typeof(Spring), new PropertyMetadata(0d));
        public double RestLength
        {
            get { return (double)GetValue(RestLengthProperty); }
            set { SetValue(RestLengthProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains one of the particles that this spring is connected to.
        /// </summary>
        public static readonly DependencyProperty ThisParticleProperty = DependencyProperty.Register(
            "ThisParticle", typeof(Particle), typeof(Spring));
        public Particle ThisParticle
        {
            get { return (Particle)GetValue(ThisParticleProperty); }
            set { SetValue(ThisParticleProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the other particle that this spring is connected to.
        /// </summary>
        public static readonly DependencyProperty ConnectedParticleProperty = DependencyProperty.Register(
            "ConnectedParticle", typeof(Particle), typeof(Spring));
        public Particle ConnectedParticle
        {
            get { return (Particle)GetValue(ConnectedParticleProperty); }
            set { SetValue(ConnectedParticleProperty, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the X1 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty X1Property = DependencyProperty.Register(
            "X1", typeof(double), typeof(Spring), new PropertyMetadata(0d));
        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y1 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register(
            "Y1", typeof(double), typeof(Spring), new PropertyMetadata(0d));
        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the X2 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty X2Property = DependencyProperty.Register(
            "X2", typeof(double), typeof(Spring), new PropertyMetadata(0d));
        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        /// <summary>
        /// Dependency Property which maintains the Y2 position of the MeatballEmitter
        /// </summary>
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register(
            "Y2", typeof(double), typeof(Spring), new PropertyMetadata(0d));
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
        static Spring()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Spring), new FrameworkPropertyMetadata(typeof(Spring)));
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Apply the spring force to ThisParticle and the ConnectedParticle
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        override public Vector ApplyForce(Particle particle)
        {
            // The particle to apply the force to must be one if the two particles which 
            // connects this spring
            if (ThisParticle.Equals(particle) || ConnectedParticle.Equals(particle))
            {
                // if the particle is ThisParticle the the other particle is the ConnectedParticle and vice-versa
                Particle con = ConnectedParticle;
                if (ConnectedParticle.Equals(particle))
                    con = ThisParticle;

                // Calculate the change in position (x and y) for the particle and its connection 
                double deltaX = particle.Position.X - con.Position.X;
                double deltaY = particle.Position.Y - con.Position.Y;

                // Calculate the change in velocity (x and y) for the particle and its connection 
                double deltaVX = particle.Velocity.X - con.Velocity.X;
                double deltaVY = particle.Velocity.Y - con.Velocity.Y;
                
                // Calculate the x and y forces generate on the particle by the spring
                double fx1 = -(SpringConstant * (Math.Abs(deltaX) - RestLength) + DampingConstant * ((deltaVX * deltaX) / Math.Abs(deltaX))) * (deltaX / Math.Abs(deltaX));
                double fy1 = -(SpringConstant * (Math.Abs(deltaY) - RestLength) + DampingConstant * ((deltaVY * deltaY) / Math.Abs(deltaY))) * (deltaY / Math.Abs(deltaY));
                
                // The negative of that force is applied to the connected particle
                double fx2 = -fx1;
                double fy2 = -fy1;

                // Apply the negative force
                Vector cForce = con.Force;
                con.Force = new Vector(cForce.X + fx2, cForce.Y + fy2);

                // Return the spring force to be applied to this particle
                return new Vector(fx1, fy1);
            }
            else
                return new Vector(0, 0);
        }

        /// <summary>
        /// Init the spring by binding the Position of ThisPaticle to the X1 and Y1 positions and 
        /// the Position of ConnectedParticle to the X2 and Y2 positions. This is used if the Spring 
        /// is going to be viewed in the UI.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Binding b = new Binding("Position.X");
            b.Source = ThisParticle;
            this.SetBinding(X1Property, b);

            b = new Binding("Position.Y");
            b.Source = ThisParticle;
            this.SetBinding(Y1Property, b);

            b = new Binding("Position.X");
            b.Source = ConnectedParticle;
            this.SetBinding(X2Property, b);

            b = new Binding("Position.Y");
            b.Source = ConnectedParticle;
            this.SetBinding(Y2Property, b);
        }

        #endregion
    }
}
