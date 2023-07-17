using RTU_Client;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{

    private const string API_ENDPOINT = "https://localhost:7012/api/ioentries";
    private const int NO_ANALOG = 10;
    private const int NO_DIGITAL = 10;
    private static List<double[]> limits = new List<double[]>();

    private static List<IOEntry> RTUs = new List<IOEntry>();
    private static Random rand = new Random();

    public static async Task Main()
    {
        initializeLimits();
        initializeRTUs();
        startSendingUpdates();
    }

    private static void startSendingUpdates()
    {
        while (true)
        {
            generateNewValues();
            foreach (var entry in RTUs)
               Console.WriteLine(entry.Value.ToString() + entry.Type.ToString() + entry.LowLimit.ToString());
            sendUpdatesToCore();
            Thread.Sleep(1000);
        }
    }

    private static void generateNewValues()
    {
        foreach (var RTU in RTUs)
        {
            if (RTU.Type == IOEntryType.ANALOG)
            {
                RTU.Value = rand.NextDouble() * (RTU.HighLimit - RTU.LowLimit) + RTU.LowLimit;
            }
            else if (RTU.Type == IOEntryType.DIGITAL)
            {
                RTU.Value = rand.NextDouble()>0.5 ? 1 : 0;
            }
        }
    }

    private static void initializeLimits()
    {
        limits.Add(new double[] { -5, 20 });
        limits.Add(new double[] { -5, 20 });
        limits.Add(new double[] { -5, 20 });
        limits.Add(new double[] { -50, 50 });
        limits.Add(new double[] { -50, 50 });
        limits.Add(new double[] { -50, 50 });
        limits.Add(new double[] { -50, 50 });
        limits.Add(new double[] { 0, 100 });
        limits.Add(new double[] { 0, 100 });
        limits.Add(new double[] { 0, 100 });
    }

    private static void initializeRTUs()
    {
        for (int i = 0; i < NO_DIGITAL; i++)
        {
            RTUs.Add(new IOEntry
            {
                IOAddress = i.ToString(),
                Value = 0,
                LowLimit = 0,
                HighLimit = 0,
                Type = IOEntryType.DIGITAL
            });
        }

        for (int i = 0; i < NO_ANALOG; i++)
        {
            RTUs.Add(new IOEntry
            {
                IOAddress = (i+NO_DIGITAL).ToString(),
                Value = 0,
                LowLimit = limits[i][0],
                HighLimit = limits[i][1],
                Type = IOEntryType.ANALOG
            });
        }
    }

    private static async void sendUpdatesToCore()
    {
        List<IOEntryDTO> dtos = new List<IOEntryDTO>();
        for (int i = 0; i < RTUs.Count; i++)
        {
            dtos.Add(new IOEntryDTO(RTUs[i]));
        }    

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var serializedEntries = JsonSerializer.Serialize(dtos);
                var requestContent = new StringContent(serializedEntries, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(API_ENDPOINT, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
