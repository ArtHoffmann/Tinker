using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tinkerforge;
using uPLibrary.Networking.M2Mqtt;
using Tinker.model;
using System.Web.Script.Serialization;
using Client;

namespace Tinker
{
    public class Start
    {
        private const string HOST = "localhost";
        private const int PORT = 4223;
        private const string SECRET_CO2 = "D25";
        private const string SECRET_LCD = "vyx";
        private const string SECRET_BAROMETER = "qLu";
        private const string SECRET_AMBIENTLIGHT = "maR";
        private const string SECRET_TEMPERATURE = "rnd";
        public CLient c = new CLient();

        //private readonly IOT_ProjectEntities _context = new IOT_ProjectEntities();

        public Start()
        {

         
            doSmth();
           
        }
      
        public void doSmth()
        {
            //Create IP Connection
           IPConnection ipcon = new IPConnection();
            // Connect to brickd
             ipcon.Connect(HOST, PORT);
            
            var BrickletLCD = new BrickletLCD20x4(SECRET_LCD, ipcon);
            var BrickletAmbientLight = new BrickletAmbientLight(SECRET_AMBIENTLIGHT, ipcon);
            var BrickletCO2 = new BrickletCO2(SECRET_CO2, ipcon);
            var BrickletBarometer = new BrickletBarometer(SECRET_BAROMETER, ipcon);
            var BrickletTemperature = new BrickletTemperature(SECRET_TEMPERATURE, ipcon);

            int i = 100;

            var data = new Bricklet_Model();
            var tb = new Thingsboard();

            
            while (true)
            {

                var co2value = BrickletCO2.GetCO2Concentration(); // unit ppm - parts per million
                var lightvalue = BrickletAmbientLight.GetAnalogValue(); // lcd 
                var barometervalue = String.Format("{0:n}", BrickletBarometer.GetAirPressure()); // in mbar
                // var barometervalue = bV / 1000;
                
                double tempvalue = (Math.Round((double)BrickletTemperature.GetTemperature() * 100) / 10000) - 5; // in °c


                tb.LightValue = lightvalue;
                tb.Barometer = Convert.ToDouble(barometervalue) / 1000;
                tb.Temperature = tempvalue;
                tb.CO2 = co2value;
                //tb.LightValue = 100;
                //tb.Barometer = 99;
                //tb.Temperature = 98;
                //tb.CO2 = 97;
                c.PublishMessage(tb);

                /*
                var temperatur = new Temperatur();
                _context.Temperatur.Add(temperatur);
                _context.Entry(temperatur).CurrentValues.SetValues(new Temperatur { Datum = new DateTime(), Einheit = "Grad", Wert = CO2Value, idTemperatur = i++ });
                _context.SaveChanges();
                */
                BrickletLCD.SetConfig(true, true);
                BrickletLCD.BacklightOn();
                Console.WriteLine(
                    " LightValue: " + lightvalue + " BarometerValue " +
                    barometervalue + "mbar " + " TempValue " + tempvalue.ToString() + "°C " + " CO2-Value: " + co2value);
                BrickletLCD.WriteLine(0, 0, "CO2 Value: " + co2value.ToString());
                BrickletLCD.WriteLine(1, 0, "LightValue: " + lightvalue.ToString());
                BrickletLCD.WriteLine(2, 0, "BarometerValue:" + barometervalue.ToString());
                BrickletLCD.WriteLine(3, 0, "Temperature " + tempvalue.ToString());

                Thread.Sleep(500);

            }

            ipcon.Disconnect();
        }
     
    }
 

}


