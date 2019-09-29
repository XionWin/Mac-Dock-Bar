using System.Windows;
using System.Windows.Controls;

namespace Effect.Lib.Forces
{
    /// <summary>
    /// Abstract class which defines the methods of a Force object.
    /// </summary>
    abstract public class Force : Control
    {
        #region

        /// <summary>
        /// Used by the Particle System to apply a force vector to a particle.
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        virtual public Vector ApplyForce(Particle particle) { return new Vector(); }
        
        #endregion
    }
}
