using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;
using Newtonsoft.Json;

[System.Serializable]

public class gameManager : MonoBehaviour
{
    public int movesLeft = 6;
    public int movesCount = 0;
    public Text movesLeftText;

    public GameObject addNotePopup;
    public InputField noteName;
    public InputField noteDesc;
    
    public Text chatText;

    public GameObject CameraGameobject;

    public GameObject popup;
    public Text popupText;

    public GameObject[] prefabs;
    private List<int> unsavedPrefabs = new List<int>();
    private List<GameObject> unsavedPrefabsAsGO = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.globalMovesLeft = movesLeft;

        /*

        List<Dictionary<string, List<float>>> objs = saveSystem.LoadObjects();
        if(objs.Count > 0)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                GameObject go = Instantiate(prefabs[(int)objs[i]["data"][0]], new Vector3(objs[i]["data"][1], objs[i]["data"][2], objs[i]["data"][3]), Quaternion.identity) as GameObject;
                go.transform.localScale = new Vector3(objs[i]["data"][4], objs[i]["data"][5], objs[i]["data"][6]);
                go.tag = "savedPrefab";
            }
        }


        List<Dictionary<string, List<Dictionary<string, List<float>>>>> cubes = saveSystem.LoadCubes();
        if (cubes.Count > 0)
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                if(cubes[i]["data"].Count > 0)
                {
                    for (int j = 0; j < cubes[i]["data"].Count; j++)
                    {
                        //Dictionary<string, List<float>> d = cubes[i]["data"][j];
                        foreach (KeyValuePair<string, List<float>> entry in cubes[i]["data"][j])
                        {


                            GameObject[] mainObject = GameObject.FindGameObjectsWithTag(entry.Key);
                            if(mainObject.Length > 0)
                            {
                                //mainObject[0].transform.FindChild()
                                Transform t = mainObject[0].transform;
                                GameObject child = null;
                                for (int ch_i = 0; ch_i < t.childCount; ch_i++)
                                {
                                    if (t.GetChild(ch_i).gameObject.tag == "cube")
                                    {
                                        child = t.GetChild(ch_i).gameObject;
                                        break;
                                    }

                                }

                                if (child != null)
                                {
                                    GameObject ccc = GameObject.Instantiate(child);
                                    ccc.gameObject.transform.parent = child.gameObject.transform.parent;//gameObject.transform.parent;
                                    ccc.transform.position = new Vector3(entry.Value[0], entry.Value[1], entry.Value[2]);
                                    ccc.transform.localScale = new Vector3(entry.Value[3], entry.Value[4], entry.Value[5]);//gameObject.transform.localScale;
                                }


                            }
                            //GameObject.FindGameObjectsWithTag(entry.Key).ta
                            // do something with entry.Value or entry.Key
                        }
                        
                    }
                }
                GameObject go = Instantiate(prefabs[(int)objs[i]["data"][0]], new Vector3(objs[i]["data"][1], objs[i]["data"][2], objs[i]["data"][3]), Quaternion.identity) as GameObject;
                go.transform.localScale = new Vector3(objs[i]["data"][4], objs[i]["data"][5], objs[i]["data"][6]);
                go.tag = "savedPrefab";
            }
        }

        Dictionary<string, List<float>> cameraObj = saveSystem.LoadCamera();
       
        if (cameraObj.Count > 0)
        {
            CameraGameobject.transform.rotation = Quaternion.Euler(cameraObj["data"][0], cameraObj["data"][1], cameraObj["data"][2]);
            Camera.main.fieldOfView = cameraObj["data"][3];
        }
          

        */
        movesLeftText.text = GlobalVariables.globalMovesLeft.ToString();
    }

    public void addNewObject()
    {
        if(GlobalVariables.globalMovesLeft > 0)
        {
            GlobalVariables.globalMovesLeft--;
            movesCount++;

            Debug.Log(prefabs.Length);
            
            int index = Random.Range(0, prefabs.Length);
            
            List<Vector3> vcList = new List<Vector3>();

            vcList.Add(new Vector3(0, 24, 0));
            vcList.Add(new Vector3(0, 44, 0));
            vcList.Add(new Vector3(23, 24, 0));
            vcList.Add(new Vector3(0, 24, 77));
            vcList.Add(new Vector3(23, 24, 0));
            vcList.Add(new Vector3(0, 24, 98));

            


            //GameObject newpref = Instantiate(prefabs[index], vcList[Random.Range(0, 5)], Quaternion.identity);
            GameObject newpref = Instantiate(prefabs[index], new Vector3(Random.Range(-49, -22), Random.Range(0, 30), Random.Range(75, 50)), Quaternion.identity);
            unsavedPrefabs.Add(index);
            unsavedPrefabsAsGO.Add(newpref);

            movesLeftText.text = GlobalVariables.globalMovesLeft.ToString();

        }
        else
        {
            Debug.Log("Its Over");
            popup.SetActive(true);
            popupText.text = "Object limit is over !!!";
            //    EditorUtility.DisplayDialog("Alert", "Object limit is over !!!", "Ok");

            //Debug.Log("Its Over");
        }

    }
    
    public void resetCamera()
    {

        CameraGameobject.transform.rotation = Quaternion.Euler(0, 0, 0);
        Camera.main.fieldOfView = 60;
    }

    public void saveObjects()
    {
        
        if (unsavedPrefabs.Count == 0)
        {
            popup.SetActive(true);
            popupText.text = "You did not add new object !!!";
            //    EditorUtility.DisplayDialog("Alert", "You did not add new object !!!", "Ok");
        }
        else if (GlobalVariables.newCubesParents.Count == 0)
        {
            popup.SetActive(true);
            popupText.text = "You did not add new object !!!";
        //    EditorUtility.DisplayDialog("Alert", "You did not add new object !!!", "Ok");
        }
        else
        {
            if (
                true
                //EditorUtility.DisplayDialog("Save Objects ?","If you save objects you can not change them again !!!", "Save", "Do Not Save")
                )
            {

                List<List<float>> parentObj = new List<List<float>>();

                for (int i = 0; i < unsavedPrefabs.Count; i++)
                {
                    unsavedPrefabsAsGO[i].tag = "savedPrefab";
                    Dictionary<string, List<float>> obj = new Dictionary<string, List<float>>();
                    List<float> lobj = new List<float>();
                    lobj.Add(unsavedPrefabs[i]);
                    lobj.Add(unsavedPrefabsAsGO[i].transform.position.x);
                    lobj.Add(unsavedPrefabsAsGO[i].transform.position.y);
                    lobj.Add(unsavedPrefabsAsGO[i].transform.position.z);
                    lobj.Add(unsavedPrefabsAsGO[i].transform.localScale.x);
                    lobj.Add(unsavedPrefabsAsGO[i].transform.localScale.y);
                    lobj.Add(unsavedPrefabsAsGO[i].transform.localScale.z);
                    parentObj.Add(lobj);
                    obj.Add("data", lobj);
                    saveSystem.SaveObjects(obj);


                }
                
                StartCoroutine(objectsSend(parentObj));

                List<Dictionary<string, List<float>>> parent_dlobj = new List<Dictionary<string, List<float>>>();

                for (int i = 0; i < GlobalVariables.newCubesParents.Count; i++)
                {
                    
                    Dictionary<string, List<Dictionary<string, List<float>>>> obj = new Dictionary<string, List<Dictionary<string, List<float>>>>();
                    List<float> lobj = new List<float>();
                    Dictionary<string, List<float>> dobj = new Dictionary<string, List<float>>();
                    List <Dictionary<string, List<float>>> dlobj = new List<Dictionary<string, List<float>>> ();

                    lobj.Add(GlobalVariables.newCubesAsGO[i].transform.position.x);
                    lobj.Add(GlobalVariables.newCubesAsGO[i].transform.position.y);
                    lobj.Add(GlobalVariables.newCubesAsGO[i].transform.position.z);
                    lobj.Add(GlobalVariables.newCubesAsGO[i].transform.localScale.x);
                    lobj.Add(GlobalVariables.newCubesAsGO[i].transform.localScale.y);
                    lobj.Add(GlobalVariables.newCubesAsGO[i].transform.localScale.z);

                    dobj.Add(GlobalVariables.newCubesParents[i], lobj);
                    dlobj.Add(dobj);
                    parent_dlobj.Add(dobj);
                    obj.Add("data", dlobj);
                    saveSystem.SaveCubes(obj);
                    
                }

                StartCoroutine(CubesSend(parent_dlobj));

                StartCoroutine(movesCountSend());

                GlobalVariables.newCubesParents.Clear();
                GlobalVariables.newCubesAsGO.Clear();


                addNotePopup.SetActive(true);
                GlobalVariables.isGameStarted = false;
                Debug.Log("save");
            }
        }
        
    }

    public void cencelNote()
    {
        noteName.text = "";
        noteDesc.text = "";
        addNotePopup.SetActive(false);
        GlobalVariables.isGameStarted = true;
    }

    public void saveNote()
    {
       
        Dictionary<string, string> note = new Dictionary<string, string>();
        note.Add("name", noteName.text);
        note.Add("desc", noteDesc.text);
        saveSystem.SaveNote(note);

        StartCoroutine(noteSend(noteName.text, noteDesc.text));

        string txt = chatText.text;
        txt += noteName.text + "\n";
        txt += noteDesc.text + "\n\n";
        chatText.text = txt;

        noteName.text = "";
        noteDesc.text = "";
        addNotePopup.SetActive(false);
        GlobalVariables.isGameStarted = true;
    }

    public int getMovesCount()
    {
        return GlobalVariables.globalMovesLeft;
    }

    public void closePopup()
    {
        popup.SetActive(false);
        popupText.text = "";
    }

    public void afterApplicationQuit()
    {
        StartCoroutine(cameraSend(CameraGameobject.transform.rotation.eulerAngles.x,
            CameraGameobject.transform.rotation.eulerAngles.y,
            CameraGameobject.transform.rotation.eulerAngles.z,
            Camera.main.fieldOfView));

        Dictionary<string, List<float>> obj = new Dictionary<string, List<float>>();
        List<float> fobj = new List<float>();
        fobj.Add(CameraGameobject.transform.rotation.eulerAngles.x);
        fobj.Add(CameraGameobject.transform.rotation.eulerAngles.y);
        fobj.Add(CameraGameobject.transform.rotation.eulerAngles.z);
        fobj.Add(Camera.main.fieldOfView);
        obj.Add("data", fobj);
        saveSystem.SaveCamera(obj);
        Debug.Log("OnApplicationQuit");
        
    }

    void OnApplicationQuit()
    {


        StartCoroutine(cameraSend(CameraGameobject.transform.rotation.eulerAngles.x,
            CameraGameobject.transform.rotation.eulerAngles.y,
            CameraGameobject.transform.rotation.eulerAngles.z,
            Camera.main.fieldOfView));

        Dictionary<string, List<float>> obj = new Dictionary<string, List<float>>();
        List<float> fobj = new List<float>();
        fobj.Add(CameraGameobject.transform.rotation.eulerAngles.x);
        fobj.Add(CameraGameobject.transform.rotation.eulerAngles.y);
        fobj.Add(CameraGameobject.transform.rotation.eulerAngles.z);
        fobj.Add(Camera.main.fieldOfView);
        obj.Add("data", fobj);
        saveSystem.SaveCamera(obj);
        Debug.Log("OnApplicationQuit");
    }

    IEnumerator logOutSend()
    {
        WWWForm form = new WWWForm();

        form.AddField("id", GlobalVariables.userInfo["Group_id"]);

        using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariables.url + "logout.php", form))
        {
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Application.Quit();
            }
        }
    }

    IEnumerator cameraSend(float x, float y, float z, float cfow)
    {
        WWWForm form = new WWWForm();
       /* Debug.Log("@@@@@@@@@@@@");
        foreach (KeyValuePair<string, string> entry in GlobalVariables.userInfo)
        {
            Debug.Log(entry.Key);
            Debug.Log(entry.Value);
        }
        Debug.Log(x.ToString());
        Debug.Log("@@@@@@@@@@@@");
        */
        form.AddField("id", GlobalVariables.userInfo["Group_id"]);
        form.AddField("password", GlobalVariables.userInfo["Password"]);
        form.AddField("x", x.ToString());
        form.AddField("y", y.ToString());
        form.AddField("z", z.ToString());
        form.AddField("cfow", cfow.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariables.url + "saveCamera.php", form))
        {
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StartCoroutine(logOutSend());
            }
        }
    }

    IEnumerator noteSend(string name, string desc)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalVariables.userInfo["Group_id"]);
        form.AddField("password", GlobalVariables.userInfo["Password"]);
        form.AddField("name", name);
        form.AddField("desc", desc);

        using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariables.url + "saveNote.php", form))
        {
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
               
            }
        }
    }

    IEnumerator objectsSend(List<List<float>> obj)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalVariables.userInfo["Group_id"]);
        form.AddField("password", GlobalVariables.userInfo["Password"]);
        form.AddField("obj", JsonConvert.SerializeObject(obj));
       
        Debug.Log(GlobalVariables.url + "saveObjects.php");
        using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariables.url + "saveObjects.php", form))
        {
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");

            }
        }
    }

    IEnumerator CubesSend(List<Dictionary<string, List<float>>> obj)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalVariables.userInfo["Group_id"]);
        form.AddField("password", GlobalVariables.userInfo["Password"]);
        form.AddField("obj", JsonConvert.SerializeObject(obj));

        using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariables.url + "saveCubes.php", form))
        {
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");

            }
        }
    }

    IEnumerator movesCountSend()
    {
        WWWForm form = new WWWForm();
    
        form.AddField("id", GlobalVariables.userInfo["Group_id"]);
        form.AddField("password", GlobalVariables.userInfo["Password"]);
        form.AddField("user_id", GlobalVariables.userInfo["User_id"]);
        form.AddField("moves", movesCount);

        using (UnityWebRequest www = UnityWebRequest.Post(GlobalVariables.url + "saveMoves.php", form))
        {
            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
