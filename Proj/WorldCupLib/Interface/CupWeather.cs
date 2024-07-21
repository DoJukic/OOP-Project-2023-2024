namespace WorldCupLib
{
    public class CupWeather
    {
        public readonly long humidity;
        public readonly long tempCelsius;
        public readonly long windSpeed;
        public readonly String description;

        public CupWeather(long? humidity, long? tempCelsius, long? windSpeed, string description)
        {
            this.humidity = humidity ?? 0;
            this.tempCelsius = tempCelsius ?? 0;
            this.windSpeed = windSpeed ?? 0;
            this.description = description;
        }
    }
}
