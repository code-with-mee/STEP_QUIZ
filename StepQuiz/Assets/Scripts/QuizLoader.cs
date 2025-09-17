using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class QuizLoader
{
    public static List<QuizItem> LoadFromResources(string resourcePathNoExt)
    {
        TextAsset json = Resources.Load<TextAsset>(resourcePathNoExt);
        if (json == null)
        {
            Debug.LogError($"Quiz JSON not found at Resources/{resourcePathNoExt}.json");
            return new List<QuizItem>();
        }
        return ParseArrayJson(json.text);
    }

    public static List<QuizItem> LoadFromStreamingAssets(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogError("Quiz JSON not found: " + path);
            return new List<QuizItem>();
        }
        string json = File.ReadAllText(path);
        return ParseArrayJson(json);
    }

    private static List<QuizItem> ParseArrayJson(string arrayJson)
    {
        string wrapped = "{\"items\":" + arrayJson + "}";
        var set = JsonUtility.FromJson<QuizSet>(wrapped);
        return set?.items ?? new List<QuizItem>();
    }
}
