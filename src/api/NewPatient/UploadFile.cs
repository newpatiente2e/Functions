using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Contoso.Healthcare.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Contoso.Healthcare.Api.NewPatient;

public static class UploadFile
{
    [FunctionName(nameof(UploadFile))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "new-patient/upload-file")] HttpRequest req,
        [CosmosDB(
                databaseName: "patientDb",
                containerName: "patientContainer",
                Connection = "COSMOS_DB")]IAsyncCollector<FormRecognizerResponse> formResponse,
        ILogger log)
    {
        var formData = await req.ReadFormAsync();
        var file = formData.Files["file"];

        if (file == null)
        {
            return new BadRequestObjectResult("File not found");
        }

        string patientId = Guid.NewGuid().ToString();
        var outputs = await ExtractFormInfo(file);

        await formResponse.AddAsync(new FormRecognizerResponse(FormRecognizerResponse.GetId(patientId), outputs));

        return new OkObjectResult(new { PatientId = patientId });
    }

    private static async Task<Dictionary<string, (string, float?)>> ExtractFormInfo(IFormFile file)
    {
        var endpoint = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_ENDPOINT");
        var apiKey = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_API_KEY");
        var modelId = Environment.GetEnvironmentVariable("FORM_RECOGNIZER_MODEL_ID");

        var client = new DocumentAnalysisClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        var options = new AnalyzeDocumentOptions { IncludeFieldElements = true };
        var operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, modelId, stream, options);
        var result = operation.Value;

        var outputs = new Dictionary<string, (string, float?)>();

        foreach (var field in result.Fields)
        {
            var fieldName = field.Key;
            var fieldValue = field.Value;

            if (fieldValue.ValueType == DocumentFieldType.String)
            {
                outputs[fieldName] = (fieldValue.AsString(), fieldValue.Confidence);
            }
        }

        return outputs;
    }
}
