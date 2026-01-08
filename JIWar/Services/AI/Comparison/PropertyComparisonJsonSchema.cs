namespace Jiwar.Services.AI.Comparison
{
    public static class PropertyComparisonJsonSchema
    {
        public const string Schema = """
{
  "type": "object",
  "properties": {
    "bestPropertyId": { "type": "number" },
    "summary": { "type": "string" },
    "scores": {
      "type": "array",
      "items": {
        "type": "object",
        "properties": {
          "propertyId": { "type": "number" },
          "priceValue": { "type": "number" },
          "location": { "type": "number" },
          "space": { "type": "number" },
          "investmentPotential": { "type": "number" },
          "comfort": { "type": "number" },
          "totalScore": { "type": "number" }
        },
        "required": [
          "propertyId",
          "priceValue",
          "location",
          "space",
          "investmentPotential",
          "comfort",
          "totalScore"
        ]
      }
    }
  },
  "required": ["bestPropertyId", "summary", "scores"]
}
""";
    }
}

