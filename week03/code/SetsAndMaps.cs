using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    /// 
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    /// ["am & ma", "if & fi"]
    /// 
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    /// 
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var set = new HashSet<string>(words);
        var result = new List<string>();

        foreach (var word in words)
        {
            // Skip same-letter words like "aa"
            if (word[0] == word[1]) continue;

            string reversed = $"{word[1]}{word[0]}";

            if (set.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                set.Remove(word);
                set.Remove(reversed); // avoid duplicates
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  
    /// The summary should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that have earned that degree.
    /// The degree information is in the 4th column of the file.  
    /// There is no header row in the file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            string degree = fields[3].Trim(); // 4th column

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.
    /// An anagram is when the same letters in a word are re-organized into a new word.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize (remove spaces, lowercase)
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length) return false;

        var counts = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (counts.ContainsKey(c))
                counts[c]++;
            else
                counts[c] = 1;
        }

        foreach (char c in word2)
        {
            if (!counts.ContainsKey(c)) return false;

            counts[c]--;

            if (counts[c] < 0) return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON data from the USGS consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// It returns a list of all earthquake locations ('place') and magnitudes ('mag').
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var jsonStream = client.GetStreamAsync(uri).Result;
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            var place = feature.Properties.Place;
            var mag = feature.Properties.Mag;
            results.Add($"{place} - Mag {mag}");
        }
        return results.ToArray();
    }

    // Classes for JSON mapping
    public class FeatureCollection
    {
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public Properties Properties { get; set; }
    }

    public class Properties
    {
        public string Place { get; set; }
        public double? Mag { get; set; }
    }
}