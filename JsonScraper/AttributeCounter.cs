using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using JsonScraper.Data;
using Microsoft.VisualBasic;

namespace JsonScraper.AttributeCounter;

public class AttributeCounter
{
    public static Dictionary<string, Dictionary<string, int>> CountAttributes(string jsonFilePath)
    {
        // Read the JSON file
        string jsonString = File.ReadAllText(jsonFilePath);

        // Deserialize into a list of dictionaries (NFT data)
        List<NFTData> nftList = JsonSerializer.Deserialize<List<NFTData>>(jsonString)!;
        Console.WriteLine(nftList[0]);

        // Initialize a dictionary to store counts
        var attributeCounts = new Dictionary<string, Dictionary<string, int>>();

        Type nftDataType = typeof(NFTData);

        Console.WriteLine("Is class: " + nftDataType.IsClass);
        Console.WriteLine("Namespace: " + nftDataType.Namespace);

        var properties = nftDataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in properties)
        {
            Console.WriteLine(prop.Name + " (" + prop.PropertyType + ")");
        }

        var NFTattributes = typeof(NFTData)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(string));

        // Iterate over NFTs and count attributes
        foreach (var attribute in NFTattributes)
        {
            string propertyName = attribute.Name;
            foreach (NFTData nft in nftList)
            {
                string value = attribute.GetValue(nft) as string;
                if (value == null) continue;

                if (!attributeCounts.ContainsKey(propertyName))
                {
                    attributeCounts[propertyName] = new Dictionary<string, int>();
                }

                if (!attributeCounts[propertyName].ContainsKey(value))
                {
                    attributeCounts[propertyName][value] = 0;
                }

                attributeCounts[propertyName][value]++;
            }
        }
        return attributeCounts;
    }

    public static void SerializeAttributes(Dictionary<string, Dictionary<string, int>> attributeCounts)
    {
        // Serialize and save the result to a JSON file
        string resultJson = JsonSerializer.Serialize(attributeCounts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("NFT1Traits.json", resultJson);

        Console.WriteLine("Attribute counts saved to NFT1Traits.json");
    }
        
    
}