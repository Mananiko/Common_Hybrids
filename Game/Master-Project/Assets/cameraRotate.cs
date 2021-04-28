using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotate : MonoBehaviour
{
    public float speed;
    //public Camera cam;

    private float minFov = 1f;
    private float maxFov = 150f;
    private float sensitivity = 10f;

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.isGameStarted)
        {
            //.transform.position = new Vector3(0, 20, 0); && Camera.main.fieldOfView < maxFov
            Debug.Log(Camera.main.fieldOfView);
            if (Input.GetAxis("Mouse ScrollWheel") != 0.0f )
            {
                float R = Input.GetAxis("Mouse ScrollWheel") * 15;                                   //The radius from current camera
                float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
                float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right
                PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
                PosY = PosY / 180 * Mathf.PI;                                       //^
                float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
                float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
                float Y = R * Mathf.Cos(PosX);                                      //^
                float CamX = Camera.main.transform.position.x;                      //Get current camera postition for the offset
                float CamY = Camera.main.transform.position.y;                      //^
                float CamZ = Camera.main.transform.position.z;                      //^
                Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
                /*
                float fov = Camera.main.fieldOfView;
                fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
                fov = Mathf.Clamp(fov, minFov, maxFov);
                Camera.main.fieldOfView = fov;
                //cam.transform.position = (cam.transform.position - gameObject.transform.position).normalized * Input.GetAxis("Mouse ScrollWheel");
                //cam.transform.position = new Vector3(0, 0, cam.transform.position.z + Input.GetAxis("Mouse ScrollWheel"));
                */
            }
            //cam.transform.position = new Vector3(0, 0, cam.transform.position.z + Input.GetAxis("Mouse ScrollWheel"));
            Debug.Log(transform.eulerAngles.x + speed * Time.deltaTime);

            if (Input.GetKey("up") && transform.eulerAngles.x + speed * Time.deltaTime < 85)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + speed * Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
            }

            if (Input.GetKey("down") && transform.eulerAngles.x + speed * Time.deltaTime > 5)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - speed * Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
            }

            if (Input.GetKey("right"))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x , transform.eulerAngles.y - speed * Time.deltaTime, transform.eulerAngles.z);
               // transform.Rotate(0, speed * Time.deltaTime, 0);
            }

            if (Input.GetKey("left"))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speed * Time.deltaTime, transform.eulerAngles.z);
            }
        }
        

    }
}
