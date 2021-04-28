using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class duplicateCube : MonoBehaviour
{
    public Camera cam;
    public gameManager gm;
    //public Button addNewObjButton;
    //public Transform CubeTransform;

    Transform t_SelectedObject;
    Transform t_AttachToObject;
    float f_CubeSize = 1f;

    //private List<string> newCubesParents = new List<string>();
    //private List<GameObject> newCubesAsGO = new List<GameObject>();

    void Start()
    {
        //Debug.Log("===============");
        //Debug.Log(GameObject.Find("gameScript").GetComponent<gameManager>().getMovesCount());
       // Debug.Log("===============");
        gm = GameObject.Find("gameScript").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {


        /* If we press right mouse button 
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit; */
        // Debug.Log(Input.mousePosition);
        // Debug.Log(cam.ScreenPointToRay(Input.mousePosition));
        /* Draw ray from mouse position to check if we hit anything with certain layer */
        //Debug.Log(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit));
        /*
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.tag == "cube")
            {
                Destroy(hit.transform.gameObject);
            }

             If we hit the same object then we de-select it */

        /*if (t_SelectedObject != null && t_SelectedObject == hit.transform)
            t_SelectedObject = null;
        else
            t_SelectedObject = hit.transform;

    }
}
*/

        if (GlobalVariables.isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit;
                //Debug.Log("DOWN");
                /* Draw ray from mouse position to check if we hit anything with certain layer */
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    GlobalVariables.selectedObjectForScaling = hit.collider.gameObject;
                    if (hit.collider.gameObject.tag == "cube" && gm != null)
                    {
                        if (gm.getMovesCount() > 0)
                        {
                            t_AttachToObject = hit.transform;
                            //Debug.Log(hit.point);
                            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            GameObject cube = GameObject.Instantiate(hit.transform.gameObject);
                            cube.gameObject.transform.parent = hit.collider.gameObject.transform.parent;//gameObject.transform.parent;
                            cube.transform.localScale = hit.collider.gameObject.transform.localScale;//gameObject.transform.localScale;
                                                                                                     // go.transform.position = transform.position;
                            
                           

                            t_SelectedObject = cube.transform;
                            if (t_SelectedObject != null)
                            {
                                /* Check if we do not hit the same object that we have selected */
                                if (t_SelectedObject != hit.transform)
                                {
                                    /* Assign position with offset so the cube center won't be at click point
                                     * Keep in mind that if you change your cube size you should change f_CubeSize as well */
                                    //t_SelectedObject.position = hit.point + hit.normal.normalized * f_CubeSize;
                                    Debug.Log(hit.collider.gameObject.transform.parent.GetComponent<positionDetection>().orientation.ToString());
                                    if (hit.collider.gameObject.transform.parent.GetComponent<positionDetection>().orientation.ToString() == "All" || hit.collider.gameObject.transform.parent.GetComponent<positionDetection>().orientation.ToString().IndexOf(FindPermissionPosition(hit.point)) != -1)
                                    {
                                        /* If you need to Rotate it accordingly to object that you attach */
                                        t_SelectedObject.rotation = hit.transform.rotation;
                                        /* Function if you want attach cube to center of hitted object side */
                                        t_SelectedObject.position = FindSideCenter(hit.point);

                                        GlobalVariables.newCubesParents.Add(cube.gameObject.transform.parent.tag);
                                        GlobalVariables.newCubesAsGO.Add(cube);
                                        //Debug.Log("===============");
                                        //Debug.Log(GameObject.Find("gameScript").GetComponent<gameManager>().getMovesCount());
                                        Debug.Log("==============="); Debug.Log("START");
                                        gm.addNewObject();
                                        Debug.Log("===============");

                                        //addNewObjButton.onClick.Invoke();
                                    }


                                }
                            }
                        }
                        else
                        {
                            gm.addNewObject();
                        }

                    }


                }
            }
        }
        



    }

    /* Function to find right side center and return it position to attach
     * If you don't need to attach it to center, then just remove this function */

    string FindPermissionPosition(Vector3 hitPosition)
    {
        System.String[] sidesS = { "Right", "Left", "Forward", "Backward", "Top", "Bottom" };

        Vector3[] sides = { t_AttachToObject.right, -t_AttachToObject.right, t_AttachToObject.forward, -t_AttachToObject.forward, t_AttachToObject.up, -t_AttachToObject.up };

        float minDistance = Vector3.Distance(t_AttachToObject.position + sides[0] * f_CubeSize, hitPosition);
        int sideIndex = 0;

        for (int i = 1; i < 6; i++)
        {
            float curDistance = Vector3.Distance(t_AttachToObject.position + sides[i] * f_CubeSize, hitPosition);

            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                sideIndex = i;
            }
        }
        return sidesS[sideIndex];
    }


    Vector3 FindSideCenter(Vector3 hitPosition)
    {

        Vector3[] sides = { t_AttachToObject.right, -t_AttachToObject.right, t_AttachToObject.forward, -t_AttachToObject.forward, t_AttachToObject.up, -t_AttachToObject.up };

        float minDistance = Vector3.Distance(t_AttachToObject.position + sides[0] * f_CubeSize, hitPosition);
        int sideIndex = 0;

        for (int i = 1; i < 6; i++)
        {
            float curDistance = Vector3.Distance(t_AttachToObject.position + sides[i] * f_CubeSize, hitPosition);

            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                sideIndex = i;
            }
        }
        
        return t_AttachToObject.position + sides[sideIndex] * f_CubeSize;
    }
}
