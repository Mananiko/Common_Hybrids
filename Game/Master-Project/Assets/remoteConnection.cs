using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


public class Comment
{
    public string name { get; set; }
    public string desc { get; set; }
}

public class Data
{
    public List<List<float>> objects { get; set; }
    public List<Dictionary<string, List<float>>> cubes { get; set; }
    public List<string> camera { get; set; }
    public List<Dictionary<string, string>> comments { get; set; }
    public int moves { get; set; }
}

public class loginResp
{
    public int Status { get; set; }
    public string Id { get; set; }
    public string User_id { get; set; }
}

public class contentResp
{
    public string msg { get; set; }
    public int status { get; set; }
    public Data data { get; set; }
}

public class remoteConnection : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField groupName;
    public InputField email;
    public InputField password;

    public Text warningText;
    public GameObject loginPanel;
    
    public Text movesLeftText;
    
    // from gameManager Duplication
    public GameObject[] prefabs;
    public GameObject CameraGameobject;
    // from gameManager Duplication

    // from chat Duplication
    public Text chatText;
    // from chat Duplication

    private string url = GlobalVariables.url;

    public void login()
    {
        if(groupName.text != "" && email.text != "" && password.text != "")
        {

            StartCoroutine(loginSend(url+"login.php?group="+ groupName.text +"&email="+ email.text + "&password=" + password.text));
            
            
        }
        else
        {
            warningText.text = "Please enter all fields !!!";
        }
    }

    public void logOut()
    {
        StartCoroutine(logOutSend());
    }

    IEnumerator loginSend(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<loginResp>(webRequest.downloadHandler.text);
            
            if(response.Status == 1)
            {
                GlobalVariables.userInfo.Add("Group", groupName.text);
                GlobalVariables.userInfo.Add("Email", email.text);
                GlobalVariables.userInfo.Add("Password", password.text);
                GlobalVariables.userInfo.Add("Group_id", response.Id); 
                GlobalVariables.userInfo.Add("User_id", response.User_id);

                StartCoroutine(getContent(url+ "getContent.php?id="+ response.Id+"&user_id="+ response.User_id+"&password=" + password.text));



            }
            else if(response.Status == 2)
            {
                warningText.text = "Can't login. Sombody from your team is currently logged in !!!";
            }
            else
            {
                warningText.text = "Login failed !!!";
            }
            
        }
    }


    IEnumerator getContent(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log("=====================");
            Debug.Log(webRequest.downloadHandler.text);
            Debug.Log("=====================");
            var response = JsonConvert.DeserializeObject<contentResp>(webRequest.downloadHandler.text);
            if (response.status == 1)
            {
                Debug.Log(GlobalVariables.globalMovesLeft);
                GlobalVariables.globalMovesLeft = GlobalVariables.globalMovesLeft - response.data.moves;
                movesLeftText.text = GlobalVariables.globalMovesLeft.ToString();

                List < List<float>> objs = response.data.objects;
                if (objs != null && objs.Count > 0)
                {
                    for (int i = 0; i < objs.Count; i++)
                    {
                        GameObject go = Instantiate(prefabs[(int)objs[i][0]], new Vector3(objs[i][1], objs[i][2], objs[i][3]), Quaternion.identity) as GameObject;
                        go.transform.localScale = new Vector3(objs[i][4], objs[i][5], objs[i][6]);
                        go.tag = "savedPrefab";
                    }
                }


                List<Dictionary<string, List<float>>> cubes = response.data.cubes;
                if (cubes != null && cubes.Count > 0)
                {
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        foreach (KeyValuePair<string, List<float>> entry in cubes[i])
                        {


                            GameObject[] mainObject = GameObject.FindGameObjectsWithTag(entry.Key);
                            if (mainObject.Length > 0)
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
                        GameObject go = Instantiate(prefabs[(int)objs[i][0]], new Vector3(objs[i][1], objs[i][2], objs[i][3]), Quaternion.identity) as GameObject;
                        go.transform.localScale = new Vector3(objs[i][4], objs[i][5], objs[i][6]);
                        go.tag = "savedPrefab";
                    }
                }

                List<string> cameraObj = response.data.camera;

                if (cameraObj != null && cameraObj.Count > 0)
                {
                    CameraGameobject.transform.rotation = Quaternion.Euler(float.Parse(cameraObj[0]), float.Parse(cameraObj[1]), float.Parse(cameraObj[2]));
                    Camera.main.fieldOfView = float.Parse(cameraObj[3]);
                }

                List<Dictionary<string, string>> noteList = response.data.comments;
                if (noteList != null && noteList.Count > 0)
                {
                    string txt = "";
                    for (int i = 0; i < noteList.Count; i++)
                    {
                        txt += noteList[i]["name"] + "\n";
                        txt += noteList[i]["desc"] + "\n\n";
                        //unsavedPrefabs[i].tag = "savedPrefab";
                    }
                    chatText.text = txt;
                }
            }

                
            /*
            GlobalVariables.userInfo.Add("Group", groupName.text);
            GlobalVariables.userInfo.Add("Email", email.text);
            GlobalVariables.userInfo.Add("Password", password.text);
            GlobalVariables.userInfo.Add("Group_id", response.Id);
            GlobalVariables.userInfo.Add("User_id", response.User_id);
            */
            GlobalVariables.isGameStarted = true;

            loginPanel.SetActive(false);

            Debug.Log("!@@@@@@@@@@@@!");
            foreach (KeyValuePair<string, string> entry in GlobalVariables.userInfo)
            {
                Debug.Log(entry.Key);
                Debug.Log(entry.Value);
            }
            Debug.Log("!@@@@@@@@@@@@!");

        }
    }

    IEnumerator logOutSend()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalVariables.userInfo["Group_id"]);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"logout.php", form))
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
