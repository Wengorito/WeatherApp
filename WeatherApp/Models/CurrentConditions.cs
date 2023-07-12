using System;

namespace WeatherApp.Models
{
    public class Unit
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public int UnitType { get; set; }
    }

    public class Temperature
    {
        public Unit Metric { get; set; }
        public Unit Imperial { get; set; }
    }

    public class CurrentConditions
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }


}
