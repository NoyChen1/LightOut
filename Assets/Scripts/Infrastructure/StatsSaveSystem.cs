using UnityEngine;
using System.IO;

public static class StatsSaveSystem
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, "stats.json");

    public static void SaveStats(StatsData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(FilePath, json);
    }

    public static StatsData LoadStats()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<StatsData>(json);
        }
        else
        {
            return new StatsData();
        }
    }

    public static void ResetStats()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }
}
