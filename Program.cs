using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Please describe with as much detail as possible the subject of your search: ");
        string? sentence = Console.ReadLine();
        if (sentence != null)
        {
            Console.WriteLine($"Analyzing: {sentence}");

            var client = new TextAnalyticsClient(new Uri("https://mrw-ai-dev-42.openai.azure.com/"), new AzureKeyCredential("72fe1b0d6d8e4ff5ab04619acd50a8ec"));

            var response = await client.RecognizeEntitiesAsync(sentence);

            var dossier = new Dictionary<string, string>();

            Console.WriteLine("Recognized entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"Text: {entity.Text}, Category: {entity.Category}, SubCategory: {entity.SubCategory}");

                Console.Write("Is this information accurate? (yes/no) ");
                string? confirmation = Console.ReadLine();
                if (confirmation?.ToLower() == "no")
                {
                    Console.Write("Please enter the correct information: ");
                    string? correction = Console.ReadLine();
                    dossier[entity.Category] = correction;
                }
                else
                {
                    dossier[entity.Category] = entity.Text;
                }
            }

            Console.WriteLine("Dossier:");
            foreach (var entry in dossier)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
        else
        {
            Console.WriteLine("No sentence provided.");
        }
    }
}