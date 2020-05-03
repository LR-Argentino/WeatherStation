using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherStationNamespace
{
    public class WeatherStation
    {

        #region Fields
        private int _count = 0;
        #endregion

        // Wir erstellen ein Array mit dem Typ Measerument 
        // und jeder index ist 15 min so das index 0 = 00:00 Uhr
        // 1 = 00:15
        Measurement[] _measurements = new Measurement[96];


        #region Property
        public int Count
        {
            get { return _count; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Setzt für eine Periode zwischen 0 - 95 den entsprechenden Messwert
        /// </summary>
        /// <param name="periode"></param>
        /// <param name="temp"></param>
        /// <param name="humidity"></param>
        public bool SetMeasurementAtPeriod(int periode, double temp, double humidity)
        {
            if(periode >= 0 && periode <= 95)
            {

                if (_measurements[periode] == null)
                {
                    _count++;
                }

                _measurements[periode] = new Measurement(temp, humidity);
                return true;
            }
            else
            {
                return false;
            }
            
        }


        /// <summary>
        /// Versucht einen Text im Format hh:mm in eine Periode umzuwandeln
        /// </summary>
        /// <param name="timeString">Text in einem hh:mm Format</param>
        /// <returns></returns>
        public static int ParseTimeString(string timeString)
        {
            if (timeString.Contains(":"))
            {
                // die stunden * 4 rechnen um den index zu bekommen
                int hours = Convert.ToInt32($"{timeString[0]}{timeString[1]}");
                int minutes = Convert.ToInt32($"{timeString[3]}{timeString[4]}");
                double measureToPeriod;

                if (hours >= 00 && hours <= 23 && minutes % 15 == 0 && minutes < 60 && minutes >= 0)
                {
                    if(minutes == 30)
                    {
                        measureToPeriod = Convert.ToDouble($"{hours},5") * 4.0;
                        
                    }else if(minutes == 45)
                    {
                        measureToPeriod = Convert.ToDouble($"{hours},75") * 4.0;
                    }else if(minutes == 15)
                    {
                        measureToPeriod = Convert.ToDouble($"{hours},25") * 4.0;
                    }
                    else
                    {
                        measureToPeriod = Convert.ToDouble($"{hours},{minutes}") * 4.0;
                        
                    }
                    return Convert.ToInt32(measureToPeriod);
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Setzt für eine Zeit den entsprechenden Messwert
        /// </summary>
        public bool SetMeasurementAtTime(string time, double temp, double humidity)
        {
            if(ParseTimeString(time) >= 0)
            {
                SetMeasurementAtPeriod(ParseTimeString(time), temp, humidity);
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Gibt die Anzahl der gespeicherten Werte in dem übergebenen Intervall zurück
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int CountInInterval(string startTime, string endTime)
        {
            int startIndex = ParseTimeString(startTime);
            int endIndex = ParseTimeString(endTime);
            int howMany = 0;


            if (startIndex != -1 && endIndex != -1)
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (_measurements[i] != null)
                    {
                        howMany++;
                    }
                }


                return howMany;
            }
            else
            {
                return -1;
            }
            
        }


        /// <summary>
        /// Berechnet den Mittelwert der Temperatur und der
        /// Luftfeuchte für den ganzen Tag
        /// </summary>
        public void GetAverageAllDay(out double temp, out double hum)
        {

            
            int avgCounter = 0;
            int inc = 0;
            Measurement[] shortMeasure = new Measurement[_count];
            for (int i = 0; i < _measurements.Length; i++)
            {
                if(_measurements[i] != null)
                {
                    avgCounter += inc;
                    shortMeasure[inc] = _measurements[i];
                    inc++;
                }
            }

            temp = shortMeasure[avgCounter / _count].Temperature;
            hum = shortMeasure[avgCounter / _count].Humidity;
        }
        #endregion
    }
}
