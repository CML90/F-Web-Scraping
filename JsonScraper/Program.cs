using JsonScraper.AttributeCounter;
using JsonScraper.WebScraper;

//WebScraper.Scraper();
Dictionary<string, Dictionary<string, int>> attributes = AttributeCounter.CountAttributes("../NFTR1.json");
Dictionary<string, int> attributeSums = new Dictionary<string, int>();

//calculate total for each attribute
int totalSum = 0;
foreach((string attribute, Dictionary<string,int> traits) in attributes)
{
    //int sum = 0;
    foreach((string trait, int count) in traits)
    {
        //sum += count;
        totalSum += count;
    }
    //attributeSums[attribute] = sum;
}

int totalNFTs = 804;
Dictionary<string, Dictionary<string,double>> attributePercentages = new Dictionary<string, Dictionary<string, double>>();
//get percentage of each trait
foreach((string attribute, Dictionary<string, int> traits) in attributes)
{
    //if(attribute == "TraitCount") continue;
    Dictionary<string, double> traitPercentages = new Dictionary<string, double>();

    foreach((string trait, int count) in traits)
    {
        traitPercentages[trait] = (double)count/totalNFTs * 100;
    }
    attributePercentages[attribute] = traitPercentages;
}

Dictionary<string, Dictionary<string,double>> attributetotalPercentages = new Dictionary<string, Dictionary<string, double>>();

//get percentage of each trait
// foreach((string attribute, Dictionary<string, int> traits) in attributes)
// {
//     //if(attribute == "TraitCount") continue;
//     Dictionary<string, double> traitPercentages = new Dictionary<string, double>();

//     foreach((string trait, int count) in traits)
//     {   
//         traitPercentages[trait] = (double)count/totalSum * 100;
//     }
//     attributetotalPercentages[attribute] = traitPercentages;
// }

List<(string,double)> traitDivPercents = attributePercentages
    .SelectMany(attribute => attribute.Value)
    .OrderByDescending(trait => trait.Value)
    .Select(trait => (trait.Key, trait.Value))
    .ToList();

// List<(string,double)> traitPercents = attributetotalPercentages
//     .SelectMany(attribute => attribute.Value)
//     .OrderByDescending(trait => trait.Value)
//     .Select(trait => (trait.Key, trait.Value))
//     .ToList();

//arrange traits according to percentage and assign points


foreach((string attribute, Dictionary<string,double> traits) in attributePercentages)
{
    Console.WriteLine($"Attribute Category: {attribute}");
    foreach((string trait, double percentage) in traits)
    {
        Console.WriteLine($"    {trait}: {percentage}");
    }
}

Console.WriteLine("LIST");
// int index = 1;
// double previousPercent = -1.00;
// foreach((string trait ,double percent) in traitPercents)
// {
//     if(percent != previousPercent)
//     {
//         Console.WriteLine($"    {index}: {trait}: {percent}");
//         index++;
//     }
//     else
//     {
//         Console.WriteLine($"    {index-1}: {trait}: {percent}");
//     }
    

//     previousPercent = percent;
// }

Console.WriteLine("LIST2");
int indexy = 1;
double previousPercent2 = -1;
foreach((string trait ,double percent) in traitDivPercents)
{
    if(percent != previousPercent2)
    {
        Console.WriteLine($"    {indexy}: {trait}: {percent}");
        indexy++;
    }   
    else
    {
        Console.WriteLine($"    {indexy-1}: {trait}: {percent}");
    }
    previousPercent2 = percent;
}