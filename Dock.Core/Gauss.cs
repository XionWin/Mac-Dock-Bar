using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dock.Core
{
    public class Gauss: System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Property

        private double sigma = 0d;
        public double Sigma
        {
            get
            {
                return this.sigma;
            }
            set
            {
                this.sigma = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Sigma"));
                }
            }
        }
        private double phase = 0d;
        public double Phase
        {
            get
            {
                return this.phase;
            }
            set
            {
                this.phase = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Phase"));
                }
            }
        }
        private double swing = 0d;
        public double Swing
        {
            get
            {
                return this.swing;
            }
            set
            {
                this.swing = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Swing"));
                }
            }
        }
        private double offset = 0d;
        public double Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Offset"));
                }
            }
        }
        #endregion Property

        #region Public function
        
        public double GaussFunction(double x)
        {
            double y = this.Swing * Math.Exp(-(Math.Pow((x - this.Phase), 2)) / (2 * Math.Pow(this.Sigma, 2))) + this.Offset;
            return y;
        }

        public double IntegrateGauss(double intervalBegin, double intervalEnd)
        {
            double integrateValue = MathNet.Numerics.Integrate.OnClosedInterval(GaussFunctionDelegate, intervalBegin, intervalEnd);
            return integrateValue;
        }
        #endregion Public function

        #region private function 
        private double GaussFunctionDelegate(double x)
        {
            double y = this.Swing * Math.Exp(-(Math.Pow((x - this.Phase), 2)) / (2 * Math.Pow(this.Sigma, 2)));
            return y;
        }
        #endregion private function 
    }
}
