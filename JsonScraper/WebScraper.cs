using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace JsonScraper.WebScraper;

public class WebScraper
{
    async public static Task<Task> Scraper()
    {
        string url = "https://cnft.tools/toolsapi/v3/project/teddybearclub2";
        var client = new HttpClient();

        // Headers (adjust as needed)
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7)");

        // Base payload
        var payloadTemplate = new
        {
            project = "none",
            sort = "asc",
            method = "rarity",
            page = 1,
            priceOnly = "all",
            filters = new { },
            sliders = new
            {
                minPrice = 0,
                maxPrice = 0,
                minRank = 0,
                maxRank = 0
            },
            instantSale = false,
            walletCheck = false,
            stakes = new List<string>(),
            pageSize = 50
        };

        var allData = new List<object>();
        int totalPages = 1;
        int delayInMilliseconds = 1000;

        for (int page = 1; page <= totalPages; page++)
        {
            Console.WriteLine($"Fetching page {page}...");

            // Update the page number in the payload
            var payload = payloadTemplate with { page = page };

            // Serialize the payload to JSON
            var jsonPayload = JsonSerializer.Serialize(payload);

            // Create the HTTP content
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    var jsonResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);

                    // Extract data from the response
                    if (jsonResponse.ContainsKey("attributes"))
                    {
                        var results = (JsonElement)jsonResponse["attributes"];
                        allData.Add(results);
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to fetch page {page}. Status code: {response.StatusCode}");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on page {page}: {ex.Message}");
                break;
            }
            await Task.Delay(delayInMilliseconds);
        }

        // Write all data to a JSON file
        string outputPath = "NFTR2Traits.json";
        await File.WriteAllTextAsync(outputPath, JsonSerializer.Serialize(allData, new JsonSerializerOptions { WriteIndented = true }));

        Console.WriteLine($"Data scraping complete. Saved to {outputPath}");

        return Task.CompletedTask;
    }
}
