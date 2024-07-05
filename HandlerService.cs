using KOLAffiliate.API.Models.GeminiModel;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HanderBook
{
    public class HandlerService : IHandlerService
    {
        private readonly JsonSerializerSettings _serializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public async Task<string> SummarizeBook(IFormFile file)
        {
            if (file == null)
            {
                return string.Empty;
            }
            //FileStream fileStream = new FileStream(file.FileName, FileMode.Open, FileAccess.Read);
            //PdfLoadedDocument loadedDocument = new PdfLoadedDocument(fileStream);
            string extractedText = string.Empty;
            string extractedTextSummarize = string.Empty;
            //int index = 0;
            //foreach (PdfLoadedPage loadedPage in loadedDocument.Pages)
            //{
            //    extractedText += loadedPage.ExtractText();
            //    if (index == 50 || index == loadedDocument.Pages.Count)
            //    {
            //        var responseText = await SummarizeTextWithAI(extractedText);
            //        extractedTextSummarize += "\n" + responseText;
            //        extractedText = string.Empty;
            //        index = 0;
            //    }
            //    index++;
            //}
            //loadedDocument.Close(true);
            using (var reader = new StreamReader(file.FileName))
            {
                extractedText = reader.ReadToEnd();
            }
            string prompt = "As an expert writer with more than a decade of experience you can help me summarize detail content of this paragraph. You are allowed to rephrase given the summary means the same as the original text:" + extractedText;
            var responseText = await CallGeminiAPI(prompt);
            extractedTextSummarize += "\n" + responseText;
            File.WriteAllText("data.txt", extractedTextSummarize);
            return extractedTextSummarize;
        }


        public async Task<string> TransferSummarizeContentToShortStory(IFormFile file)
        {
            if (file == null)
            {
                return string.Empty;
            }
            string extractedText = string.Empty;
            using (var reader = new StreamReader(file.FileName))
            {
                extractedText = reader.ReadToEnd();
            }
            string prompt = "As an expert writer with more than a decade of experience you can help me write short story base on some suggestion then and you can use Vietnamese language to write: " + extractedText;
            var responseText = await CallGeminiAPI(prompt);
            File.WriteAllText("dataShortStory.txt", responseText);
            return responseText;
        }

        public async Task<string> TransferTextToVideo(IFormFile file)
        {
            if(file == null)
            {
                Console.WriteLine("Test transfer text to video using AI");
                return string.Empty;
            }

            return string.Empty;
        }

        private async Task<string?> CallGeminiAPI(string prompt)
        {
            GeminiRequest requestBody = new GeminiRequest();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1/models/gemini-pro:generateContent");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                requestBody = GeminiRequestFactory.CreateRequest(prompt);
                var content = new StringContent(JsonConvert.SerializeObject(requestBody, Formatting.None, _serializerSettings), Encoding.UTF8, "application/json");
                var httpResonse = await httpClient.PostAsync("?key=AIzaSyDB7OMHk6n3NbyeKlX_Hb1UtnB6reBjz9Q", content);
                httpResonse.EnsureSuccessStatusCode();
                var responseBody = await httpResonse.Content.ReadAsStringAsync();
                var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(responseBody);
                return geminiResponse?.Candidates[0].Content.Parts[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
