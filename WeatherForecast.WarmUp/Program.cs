using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace WeatherForecast.WarmUp
{
    class Program
    {
        private static readonly HttpClient _httpClient = new() { BaseAddress = new Uri("http://localhost:80") };

        static void Main(string[] args)
        {
            var appIsUp = CheckIfAppIsUp(5, 5).GetAwaiter().GetResult();

            if (appIsUp)
            {
                WarmUpApp(5).GetAwaiter().GetResult();
                
                Console.WriteLine("The app is warm");
            }
        }
        
        private static async Task<bool> CheckIfAppIsUp(int maxAttempts, int secondsToWaitBetweenRetries)
        {
            var isAppUpRetryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(maxAttempts, _ => TimeSpan.FromSeconds(secondsToWaitBetweenRetries));
    
            var attemptsLeft = maxAttempts;
    
            try
            {
                await isAppUpRetryPolicy.ExecuteAsync(async () =>
                {
                    Console.WriteLine($"Trying to hit health check (attempts left: {attemptsLeft--})");
            
                    await _httpClient.GetAsync("/health");
                });
            }
            catch (Exception)
            {
                Console.WriteLine("The app is not up. Ending check");
                return false;
            }

            Console.WriteLine("The app is up");
    
            return true;
        }

        private static async Task WarmUpApp(int numberOfRequests)
        {
            for (var i = 0; i < numberOfRequests; i++)
            {
                await _httpClient.GetAsync("/WeatherForecast");
            }
        }
    }
}