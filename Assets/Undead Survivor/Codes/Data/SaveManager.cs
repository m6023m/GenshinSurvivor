using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager
{
    private static ConcurrentQueue<SaveData> saveQueue = new ConcurrentQueue<SaveData>();
    private static string SavePath => Application.persistentDataPath + "/saves/";
    private static string saveFileName = "saveData";

    public static void Save(SaveData saveData)
    {
        saveQueue.Enqueue(saveData);
        ProcessSaveQueue();
    }

    private static async void ProcessSaveQueue()
    {
        if (saveQueue.TryDequeue(out SaveData saveData))
        {
            await SaveAsync(saveData);
        }
    }

    private static async Task SaveAsync(SaveData saveData)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonConvert.SerializeObject(saveData);
        string saveFilePath = SavePath + saveFileName + ".json";
        await WriteFileAsync(saveFilePath, saveJson);
        Debug.Log("Save Success: " + saveFilePath);
    }

    private static Task WriteFileAsync(string path, string content)
    {
        return Task.Run(() => File.WriteAllText(path, content));
    }

    public static SaveData Load()
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No such saveFile exists");
            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(saveFile);
        return saveData;
    }
}
