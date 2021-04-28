using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveSystem
{
    public static void SaveNote(Dictionary<string, string> note)
    {
        List<Dictionary<string, string>> noteList = new List<Dictionary<string, string>>();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/note.gaga";
        Debug.Log(Application.persistentDataPath);
        FileStream stream;
        if (File.Exists(path))
        {
            FileStream streamOpen = new FileStream(path, FileMode.Open);
            List<Dictionary<string, string>> OpenedNoteList = formatter.Deserialize(streamOpen) as List<Dictionary<string, string>>;
            noteList.AddRange(OpenedNoteList);
            streamOpen.Close();
            //new FileStream(path, FileMode.Truncate);
        }

        noteList.Add(note);

        stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, noteList);
        stream.Close();
    }
    public static List<Dictionary<string, string>> LoadNote()
    {
        List<Dictionary<string, string>> noteList = new List<Dictionary<string, string>>();
        string path = Application.persistentDataPath + "/note.gaga";
        //File.Delete(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<Dictionary<string, string>> note = formatter.Deserialize(stream) as List<Dictionary<string, string>>;
            stream.Close();
            return note;
           // Debug.Log(note[2]["name"]);
           // Debug.Log(note[2]["desc"]);
        }
        return noteList;
    }


    public static void SaveObjects(Dictionary<string, List<float>> note)
    {
        List<Dictionary<string, List<float>>> noteList = new List<Dictionary<string, List<float>>>();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/objects.gaga";
        FileStream stream;
        if (File.Exists(path))
        {
            FileStream streamOpen = new FileStream(path, FileMode.Open);
            List<Dictionary<string, List<float>>> OpenedNoteList = formatter.Deserialize(streamOpen) as List<Dictionary<string, List<float>>>;
            noteList.AddRange(OpenedNoteList);
            streamOpen.Close();
            //new FileStream(path, FileMode.Truncate);
        }

        noteList.Add(note);

        stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, noteList);
        stream.Close();
    }
    public static List<Dictionary<string, List<float>>> LoadObjects()
    {
        List<Dictionary<string, List<float>>> noteList = new List<Dictionary<string, List<float>>>();
        string path = Application.persistentDataPath + "/objects.gaga";
        //Debug.Log(path);
        //File.Delete(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<Dictionary<string, List<float>>> note = formatter.Deserialize(stream) as List<Dictionary<string, List<float>>>;
            stream.Close();
            return note;
            // Debug.Log(note[2]["name"]);
            // Debug.Log(note[2]["desc"]);
        }
        return noteList;
    }


    public static void SaveCubes(Dictionary<string, List<Dictionary<string, List<float>>>> note)
    {
        List<Dictionary<string, List<Dictionary<string, List<float>>>>> noteList = new List<Dictionary<string, List<Dictionary<string, List<float>>>>>();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/cubes.gaga";
        FileStream stream;
        if (File.Exists(path))
        {
            FileStream streamOpen = new FileStream(path, FileMode.Open);
            List<Dictionary<string, List<Dictionary<string, List<float>>>>> OpenedNoteList = formatter.Deserialize(streamOpen) as List<Dictionary<string, List<Dictionary<string, List<float>>>>>;
            noteList.AddRange(OpenedNoteList);
            streamOpen.Close();
            //new FileStream(path, FileMode.Truncate);
        }

        noteList.Add(note);

        stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, noteList);
        stream.Close();
    }
    public static List<Dictionary<string, List<Dictionary<string, List<float>>>>> LoadCubes()
    {
        List<Dictionary<string, List<Dictionary<string, List<float>>>>> noteList = new List<Dictionary<string, List<Dictionary<string, List<float>>>>>();
        string path = Application.persistentDataPath + "/cubes.gaga";
        //Debug.Log(path);
        //File.Delete(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<Dictionary<string, List<Dictionary<string, List<float>>>>> note = formatter.Deserialize(stream) as List<Dictionary<string, List<Dictionary<string, List<float>>>>>;
            stream.Close();
            return note;
            // Debug.Log(note[2]["name"]);
            // Debug.Log(note[2]["desc"]);
        }
        return noteList;
    }

    public static void SaveCamera(Dictionary<string, List<float>> note)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/camera.gaga";
        FileStream stream;

        stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, note);
        stream.Close();
    }
    public static Dictionary<string, List<float>> LoadCamera()
    {
        Dictionary<string, List<float>> noteList = new Dictionary<string, List<float>>();
        string path = Application.persistentDataPath + "/camera.gaga";
        //File.Delete(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Dictionary<string, List<float>> note = formatter.Deserialize(stream) as Dictionary<string, List<float>>;
            stream.Close();
            return note;
            // Debug.Log(note[2]["name"]);
            // Debug.Log(note[2]["desc"]);
        }
        return noteList;
    }
}
