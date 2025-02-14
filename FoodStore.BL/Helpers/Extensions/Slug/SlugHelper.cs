using System.Text.RegularExpressions;

namespace FoodStore.BL.Helpers.Extensions.Slug;

public static class SlugHelper
{
    public static string CreateSlug(string input)
    {
        input = input.ToLowerInvariant();
        input = RemoveTurkishCharacters(input);
        input = Regex.Replace(input, @"[^a-z0-9\s-]", "");
        input = Regex.Replace(input, @"\s+", " ").Trim();
        input = input.Replace(" ", "-");
        return input;
    }

    private static string RemoveTurkishCharacters(string input)
    {
        string[] turkishChars = { "ç", "ı", "ğ", "ü", "ş", "ö" };
        string[] englishChars = { "c", "i", "g", "u", "s", "o" };

        for (int i = 0; i < turkishChars.Length; i++)
        {
            input = input.Replace(turkishChars[i], englishChars[i]);
        }

        return input;
    }
}