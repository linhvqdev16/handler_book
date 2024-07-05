﻿namespace KOLAffiliate.API.Models.GeminiModel
{
    public class GeminiResponse
    {
        public Candidate[] Candidates { get; set; }
        public PromptFeedback PromptFeedback { get; set; }
    }

    public class PromptFeedback
    {
        public SafetyRating[] SafetyRatings { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
        public SafetyRating[] SafetyRatings { get; set; }
    }

    public class Content
    {
        public Part[] Parts { get; set; }
        public string Role { get; set; }
    }

    public class Part
    {
        // This one interests us the most
        public string Text { get; set; }
    }

    public class SafetyRating
    {
        public string Category { get; set; }
        public string Probability { get; set; }
    }
}
