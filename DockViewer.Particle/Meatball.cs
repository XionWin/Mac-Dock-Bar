using Effect.Lib.Forces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Effect.Lib
{
    public class Meatball
    {
        #region Properties

        /// <summary>
        /// Static properties which contains the number of connections for a meatball
        /// </summary>
        static readonly public int NumberOfConnections = 3;

        /// <summary>
        /// The array of particles which make up a meatball
        /// </summary>
        private Particle[] mParticles = new Particle[3];
        public Particle[] Particles
        {
            get { return mParticles; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <param name="emitter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="minks">Minimum Spring Constant</param>
        /// <param name="maxks">Maximum Spring Constant</param>
        /// <param name="minkd">Minimum Dampening Constant</param>
        /// <param name="maxkd">Maximum Dampening Constant</param>
        /// <param name="minrest">Minimum Rest Length</param>
        /// <param name="maxrest">Maximum Rest Length</param>
        public Meatball(ParticleSystem system, Emitter emitter, double x, double y,
            double minks, double maxks, double minkd, double maxkd, double minrest,
            double maxrest, bool anchored)
        {
            // create thre particles
            mParticles[0] = new Particle();
            mParticles[0].IsAnchor = anchored; // anchors the first particle if so desired
            mParticles[1] = new Particle();
            mParticles[2] = new Particle();

            // update the particles
            Update(emitter, x, y);

            // Create the spring between particles 0 and 1
            Spring s = new Spring();
            s.ThisParticle = mParticles[0];
            s.ConnectedParticle = mParticles[1];
            s.SpringConstant = ParticleSystem.random.NextDouble(minks, maxks); //1.0, 4.0);
            s.DampingConstant = ParticleSystem.random.NextDouble(minkd, maxkd); //0.10, 0.30);
            s.RestLength = ParticleSystem.random.NextDouble(minrest, maxrest); //1.0, 6.0);
            mParticles[0].Connections.Add(s);
            mParticles[1].Connections.Add(s);
            //system.Forces.Add(s);

            // Create the spring between particles 0 and 2
            s = new Spring();
            s.ThisParticle = mParticles[0];
            s.ConnectedParticle = mParticles[2];
            s.SpringConstant = ParticleSystem.random.NextDouble(minks, maxks); //1.0, 4.0);
            s.DampingConstant = ParticleSystem.random.NextDouble(minkd, maxkd); //0.10, 0.30);
            s.RestLength = ParticleSystem.random.NextDouble(minrest, maxrest); //1.0, 6.0);
            mParticles[0].Connections.Add(s);
            mParticles[2].Connections.Add(s);
            //system.Forces.Add(s);

            // Create the spring between particles 1 and 2
            s = new Spring();
            s.ThisParticle = mParticles[1];
            s.ConnectedParticle = mParticles[2];
            s.SpringConstant = ParticleSystem.random.NextDouble(minks, maxks); //1.0, 4.0);
            s.DampingConstant = ParticleSystem.random.NextDouble(minkd, maxkd); //0.10, 0.30);
            s.RestLength = ParticleSystem.random.NextDouble(minrest, maxrest); //1.0, 6.0);
            mParticles[1].Connections.Add(s);
            mParticles[2].Connections.Add(s);
            //system.Forces.Add(s);

            // Add the particles to the system
            system.Particles.Add(mParticles[0]);
            system.Particles.Add(mParticles[1]);
            system.Particles.Add(mParticles[2]);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update a meatball
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Update(Emitter emitter, double x, double y)
        {
            // All particles should have the same lifespan, so only calculate it once.
            double life = ParticleSystem.random.NextDouble(emitter.MinLifeSpan, emitter.MaxLifeSpan);

            // inti every particle in the particle list
            foreach (Particle particle in mParticles)
            {
                particle.Owner = emitter;
                particle.Mass = (float)ParticleSystem.random.NextDouble(emitter.MinMass, emitter.MaxMass);
                particle.StartOpacity = emitter.StartOpacity;
                particle.EndOpacity = emitter.EndOpacity;
                particle.Force = new Vector(0, 0);
                particle.Velocity = new Vector(
                    ParticleSystem.random.NextDouble(emitter.MinHorizontalVelocity, emitter.MaxHorizontalVelocity),
                    ParticleSystem.random.NextDouble(emitter.MinVerticalVelocity, emitter.MaxVerticalVelocity));
                particle.LifeSpan = life;
                particle.Position = new Point(x + ParticleSystem.random.NextDouble(emitter.MinPositionOffset, emitter.MaxPositionOffset),
                                              y + ParticleSystem.random.NextDouble(emitter.MinPositionOffset, emitter.MaxPositionOffset));
                particle.BackgroundColors = emitter.ColorKeyFrames;
            }
        }

        #endregion
    }
}
