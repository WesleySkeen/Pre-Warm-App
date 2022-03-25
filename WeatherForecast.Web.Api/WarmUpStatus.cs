namespace WeatherForecast.Web.Api
{
    public static class WarmUpStatus
    {
        public static string WarmUpSuccessFileName => "warmup.success";
        public static bool IsWarmedUp { get; private set; }

        public static void SetIsWarmedUpStatus(bool isWarmedUp)
        {
            IsWarmedUp = isWarmedUp;
        }
    }
}