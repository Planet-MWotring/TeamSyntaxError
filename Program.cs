using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;

partial class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Please describe with as much detail as possible the subject of your search: ");
        string? sentence = Console.ReadLine();
        if (sentence != null)
        {
            Console.WriteLine($"Analyzing: {sentence}");

            // Initialize the OpenAI client
            OpenAIClient openAIClient = new OpenAIClient(
                new Uri("https://mrw-ai-dev-42.openai.azure.com/"),
                new AzureKeyCredential(Environment.GetEnvironmentVariable("AZURE_API_KEY")));

            // Use the OpenAI service for text analysis
            var completionResponse = await openAIClient.GetCompletionsAsync(
                "mrw-ai-gpt-35-turbo", // or another model of your choice
                new CompletionsOptions(){
                    Prompts = { sentence },
                } // Adjust maxTokens as needed
            );

            // Process the response
            Console.WriteLine("Analysis result:");
            Console.WriteLine(completionResponse.Value.Choices[0].Text);

            // Further processing based on the analysis result...
        }
    }
}