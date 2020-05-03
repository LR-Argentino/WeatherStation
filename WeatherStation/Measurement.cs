using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherStationNamespace
{
    public class Measurement
    {
        #region Fields
        private double _temp;
        private double _humidity;
        #endregion

        #region Properties
        public double Temperature
        {
            get { return _temp; }
            set { _temp = value; }
        }

        public double Humidity
        {
            get { return _humidity; }
            set { _humidity = value; }
        }

        public bool IsComfortable
        {
            get
            {
                if(_temp >= 21.0 && _temp <= 24.0 && _humidity >= 40.0 && _humidity <= 60.0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region Konstruktor
        public Measurement(double temp, double humidity)
        {
            this.Temperature = temp;
            this.Humidity = humidity;
        }
        #endregion
    }
}
