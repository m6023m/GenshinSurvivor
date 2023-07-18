using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager
{
    private static string SavePath => Application.persistentDataPath + "/saves/";
    private static string saveFileName = "saveData";

    public static void Save(SaveData saveData)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonConvert.SerializeObject(saveData);

        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("Save Success: ".AddString(saveFilePath));
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