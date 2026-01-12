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
          "categoryScores": {
            "type": "object",
            "properties": {
              "priceValue": { "$ref": "#/definitions/score" },
              "location": { "$ref": "#/definitions/score" },
              "spaceAndLayout": { "$ref": "#/definitions/score" },
              "features": { "$ref": "#/definitions/score" },
              "investmentPotential": { "$ref": "#/definitions/score" }
            },
            "required": ["priceValue","location","spaceAndLayout","features","investmentPotential"]
          },
          "totalScore": { "type": "number" },
          "overallReason": { "type": "string" }
        },
        "required": ["propertyId","categoryScores","totalScore","overallReason"]
      }
    }
  },
  "required": ["bestPropertyId","summary","scores"],
  "definitions": {
    "score": {
      "type": "object",
      "properties": {
        "score": { "type": "number" },
        "description": { "type": "string" }
      },
      "required": ["score","description"]
    }
  }
}
""";
    }
}

