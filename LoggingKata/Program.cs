using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Geolocation;

namespace LoggingKata
{
    class Program
    {
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            Logger.Info("Log initialized");

            var csvPath = Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv";

            if (csvPath.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified in our path variable");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Reading Lines");

            var rows = File.ReadAllLines(csvPath);

            switch (rows.Length)
            {
                case 0:
                    Logger.Error("Our csv file is missing or empty of content");
                    break;
                case 1:
                    Logger.Warn("Can't compare. There is only one element");
                    break;
            }

            Console.WriteLine("Parsing Tacos");

            var parser = new TacoParser();
            Logger.Info("Initialized our Parser");

            var locations = rows.Select(row => parser.Parse(row))
                .OrderBy(loc => loc.Location.Longitude)
                .ThenBy(loc => loc.Location.Latitude)
                .ToArray();

            Console.WriteLine("Parsed locations");

            ITrackable a = null;
            ITrackable b = null;
            double distance = 0;

            foreach (var locA in locations)
            {
                var origin = new Coordinate
                {
                    Latitude = locA.Location.Latitude,
                    Longitude = locA.Location.Longitude
                };

                foreach (var locB in locations)
                {
                    var dest = new Coordinate
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Longitude
                    };

                    var nDist = GeoCalculator.GetDistance(origin, dest);

                    if (nDist > distance)
                    {
                        a = locA;
                        b = locB;
                        distance = nDist;
                    }
                }
            }

            Console.WriteLine("Foreach loops are finished");

            if (a == null || b == null)
            {
                Logger.Error("Could not find furthest locations");
                Console.WriteLine("Couldn't find the locations furthest apart");
                Console.ReadLine();
            }

            Console.WriteLine($"The 2 TacoBells that are furthest apart are: {a.Name} and: {b.Name}. These 2 locations are {distance} miles apart");
            Console.ReadLine();
        }
    }
}