using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class ballInHole : MonoBehaviour
{
    public int hole_Number;
    public string goto_Hole_Name;
    public CinemachineFreeLook freeLookCam;
    public float x_Cam_Value;
    public float y_Cam_Value;

    // These 2 prolly need to be in a main script for the hole itself. and have the trigger on the base object instead of its own object.

    void OnTriggerEnter (Collider col)
    {
        Debug.Log("Collider Detected");
        if (col.gameObject.tag == "golfBall")
        {
            Debug.Log("Golf Ball In the HOLE!");
            // Wait 2 seconds before transporting the object
            //teleportBall();
            GameObject current_Hole = gameObject.transform.parent.gameObject;
            Debug.Log("current_Hole = " + current_Hole);
            GameObject current_Bounds = current_Hole.transform.Find("Bounds").gameObject;
            GameObject next_Hole = GameObject.Find(goto_Hole_Name);
            GameObject start_Mat_Child = next_Hole.transform.Find("StartMat").gameObject;
            Debug.Log(start_Mat_Child);
            Transform next_Mat = next_Hole.transform.Find("StartMat");
            Vector3 move_Pos = start_Mat_Child.transform.position;
            Vector3 mat_Size = start_Mat_Child.transform.localScale;
            Debug.Log("mat_Size = " + mat_Size);
            // Vector3 move_Pos = next_Mat.position;
            // Vector3 mat_Size = next_Hole.transform.Find("StartMat").
            move_Pos.y = move_Pos.y; // + 0.002f;
            Debug.Log(move_Pos);
            // System.Threading.Thread.Sleep(2000);
            // Disable the bounds
            current_Bounds.GetComponent<Collider>().enabled = false;
            col.attachedRigidbody.velocity = Vector3.zero;
            col.transform.position = move_Pos;
            next_Hole.transform.Find("Bounds").GetComponent<Collider>().enabled = true;
            Debug.Log("Cam x axis: " + freeLookCam.m_XAxis.Value);
            Debug.Log("Cam y axis: " + freeLookCam.m_YAxis.Value);
            freeLookCam.m_XAxis.Value = x_Cam_Value;
            freeLookCam.m_YAxis.Value = y_Cam_Value;
            Debug.Log("Setting X Y" + x_Cam_Value + "  " + y_Cam_Value);
            Debug.Log("Cam x axis: " + freeLookCam.m_XAxis.Value);
            Debug.Log("Cam y axis: " + freeLookCam.m_YAxis.Value);

        }
    }

    public void teleportBall()
    {
        GameObject next_Hole = GameObject.Find(goto_Hole_Name);
        Debug.Log(next_Hole.transform.Find("StartMat").position);
        // System.Threading.Thread.Sleep(2000);

    }


}
