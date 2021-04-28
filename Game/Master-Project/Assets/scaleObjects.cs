using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scaleObjects : MonoBehaviour
{
    public InputField x_axis_t;
    public InputField y_axis_t;
    public InputField z_axis_t;
    
    private bool lockInputs = false;
    // Start is called before the first frame update
    void Start()
    {
        x_axis_t.enabled = false;
        y_axis_t.enabled = false;
        z_axis_t.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.isGameStarted)
        {
           // Debug.Log(GlobalVariables.selectedObjectForScaling.tag);
            if (GlobalVariables.selectedObjectForScaling != null && GlobalVariables.selectedObjectForScaling.tag == "unsavedPrefab")
            {

                lockInputs = true;
                x_axis_t.enabled = true;
                x_axis_t.text = GlobalVariables.selectedObjectForScaling.transform.localScale.x.ToString();
                y_axis_t.enabled = true;
                y_axis_t.text = GlobalVariables.selectedObjectForScaling.transform.localScale.y.ToString();
                z_axis_t.enabled = true;
                z_axis_t.text = GlobalVariables.selectedObjectForScaling.transform.localScale.z.ToString();
                lockInputs = false;
            }
            else
            {
                x_axis_t.enabled = false;
                x_axis_t.text = "1";
                y_axis_t.enabled = false;
                y_axis_t.text = "1";
                z_axis_t.enabled = false;
                z_axis_t.text = "1";
            }
        }
        
    }

    public void inScaleInputChange()
    {
        
        if (GlobalVariables.selectedObjectForScaling != null && !lockInputs && GlobalVariables.selectedObjectForScaling != null && GlobalVariables.selectedObjectForScaling.tag == "unsavedPrefab")
        {
           
            GlobalVariables.selectedObjectForScaling.transform.localScale = new Vector3(float.Parse(x_axis_t.text), float.Parse(y_axis_t.text), float.Parse(z_axis_t.text));
        }
    }
}
