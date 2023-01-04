using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveState()
    {
        //var allObjects = GameObject.Find("GM").ObjectsOfType<MonoBehaviour>();
        //foreach (var obj in allObjects)
        //{
        //    obj.StopAllCoroutines();
        //}
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveState";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData();

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadState()
    {
        string path = Application.persistentDataPath + "/saveState";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            UnityEngine.Debug.Log("save file not found in " + path);
            return null;
        }
    }
}
