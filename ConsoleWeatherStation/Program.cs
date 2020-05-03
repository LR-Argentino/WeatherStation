using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WeatherStationNamespace;

namespace ConsoleWeatherStation
{
    class Program
    {
        /// <summary>
        /// Die Wetterdaten werden aus der Datei Messwerte.csv eingelesen
        /// und ausgewertet
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            
            string[] lines = File.ReadAllLines("Messwerte.csv", Encoding.UTF8);
          
            
            
            WeatherStation station = new WeatherStation();

            
            
            for (int i = 1; i < lines.Length; i++)
            {
                int periode = Convert.ToInt32(lines[i].Split(';')[0]);
                double temp = Convert.ToDouble(lines[i].Split(';')[1]);
                double humi = Convert.ToDouble(lines[i].Split(';')[2]);
                station.SetMeasurementAtPeriod(periode, temp, humi);
            }



            Console.WriteLine("Auswertung der Wetterstation");
            Console.WriteLine();
            Console.WriteLine("Anzahl der gültiger Viertelstundenwerte: {0}", station.Count);
            double temperatur;
            double hum;
            station.GetAverageAllDay(out temperatur, out hum);
            Console.WriteLine("Durchschnittstemperatur: {0}, und Druchschnittsfeuchte: {1}", temperatur, hum );
            Console.Write("Zum Beenden Eingabetaste ...");
            Console.ReadLine();
        }
    }
}
