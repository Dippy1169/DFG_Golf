using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtHole : MonoBehaviour
{

    // private Quaternion cam_Look;
    private float where_To_Look;
    public Vector3 hole_Pos_To_Look;
    public float degrees;
    public CinemachineFreeLook freeLookCam;
    public GameObject Hole_Trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        //Debug.Log("On Trigger Exit");
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "golfBall")
        {
            // Get the game object of hole Trigger. 
            // Since this is in our hole we can use it to look at
            GameObject hole_Num = transform.parent.gameObject;
            //Debug.Log(hole_Num.name);
            //GameObject hole_Obj = Hole_Trigger;
            //Debug.Log(hole_Obj.name);
            //Debug.Log(hole_Obj.transform.position);
            //GameObject hole_Obj = GameObject.Find("HoleTrigger").gameObject;
            // Get the angle delta on the position this will tell our cmaera where to look
            //where_To_Look = Vector3.Angle(col.transform.position, Hole_Trigger.transform.position);
            //Debug.Log("col pos: " + col.transform.position);
            //Debug.Log("hole pos: " + hole_Obj.transform.position);
            hole_Pos_To_Look = transform.TransformPoint(Hole_Trigger.GetComponent<BoxCollider>().center);
            //Debug.Log("Transform Pos: " + Hole_Trigger.transform.position);
            //Debug.Log("hole pos: " + transform.TransformPoint(Hole_Trigger.GetComponent<BoxCollider>().center));
            //float x_Delta = hole_Obj.transform.position.x - col.transform.position.x;
            //float y_Delta = hole_Obj.transform.position.y - col.transform.position.y;
            float x_Delta = col.transform.position.x - hole_Pos_To_Look.x;
            float z_Delta = col.transform.position.z - hole_Pos_To_Look.z;
            if (z_Delta > 0)
            {
                degrees = Mathf.Atan(z_Delta / x_Delta) * (180 / Mathf.PI);
            }
            else
            {
                degrees = Mathf.Atan(x_Delta / z_Delta) * (180 / Mathf.PI);
            }
            //Debug.Log("Delta X: " + x_Delta);
            //Debug.Log("Delta z: " + z_Delta);
            //Debug.Log("Initial Look at " + degrees);
            //degrees = (90 - Mathf.Abs(degrees)) + 90;
            if (z_Delta > 0)
            {
                if (x_Delta < 0)
                {
                    degrees = 90 + (degrees * -1);
                }
                else if (x_Delta > 0)
                {
                    degrees = -90 + (degrees * -1);
                }

            }
            //if (x_Delta > 0)
            //{
            //    degrees = degrees * -1;
            //}
            //if (z_Delta < 0)
            //{
            //    degrees = degrees + 90;
            //}
            //Debug.Log("Angle we should look at " + degrees);
            //Debug.Log("Where we are looking " + freeLookCam.m_XAxis.Value);
            // Set the camera angle we just calculated
            freeLookCam.m_XAxis.Value = degrees;
        }
    }
}
