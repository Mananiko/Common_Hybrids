using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chat : MonoBehaviour
{
    public Button chatOpenCloseButton;
    public Text chatOpenCloseText;
    public ScrollRect scrl;
    public ScrollRect scrlInfo;
    public Text chatText;
    // Start is called before the first frame update
    void Start()
    {
        /*
        List<Dictionary<string, string>> noteList = saveSystem.LoadNote();
        if(noteList.Count > 0)
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
        */
    }

    public void closeButtonClick()
    {
        if (GlobalVariables.isGameStarted)
        {
            if (scrl.gameObject.active)
            {
                scrl.gameObject.SetActive(false);
                chatOpenCloseText.text = "<<";
            }
            else
            {
                scrl.gameObject.SetActive(true);
                chatOpenCloseText.text = ">>";
            }
        }
        
    }

    public void closeButtonClickInfo()
    {
        if (GlobalVariables.isGameStarted)
        {
            if (scrlInfo.gameObject.active)
            {
                scrlInfo.gameObject.SetActive(false);
            }
            else
            {
                scrlInfo.gameObject.SetActive(true);
            }
        }

    }

}
