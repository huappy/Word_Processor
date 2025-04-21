using System;

namespace WordProcessorApp.Models
{
    public class DocumentModel
    {
        public string Text { get; set; } = string.Empty;

        public string FontFamily { get; set; } = "Segoe UI";

        public double FontSize { get; set; } = 14;

        public bool IsBold { get; set; } = false;

        public bool IsItalic { get; set; } = false;

        // Optional: Computed property for word count
        public int WordCount => string.IsNullOrWhiteSpace(Text)
            ? 0
            : Text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
