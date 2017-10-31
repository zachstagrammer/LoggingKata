using System.Collections;
using System.Collections.Generic;
using log4net;
using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
    public class TacoParser
    {
        public TacoParser()
        {

        }

        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Name { get; private set; }
        public Point Location { get; private set; }

        public ITrackable Parse(string row)
        {
            var cells = row.Split(',');

            if (cells.Length < 3)
            {
                Logger.Error("Must have at least 3 elements to parse into ITrackable");
                return null;
            }

            double lon = 0;
            double lat = 0;

            try
            {
                Logger.Debug("Attempting to parse Longitude");
                lon = double.Parse(cells[0]);

                Logger.Debug("Attempting to parse Latitude");
                lat = double.Parse(cells[1]);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to parse the location", e);
                return null;
            }

            var tacoBell = new TacoBell()
            {
                Name = cells[2],
                Location = new Point { Longitude = lon, Latitude = lat }
            };

            Logger.Info("Created a new TacoBell");

            return tacoBell;
        }
    }
}