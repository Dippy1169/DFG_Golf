using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{

    public ActiveHole hole_Script;
    //public Putt_pull putt_Script;

    private Vector3 shot_Location;

    void OnTriggerExit(Collider col)
    {
        //Debug.Log("On Trigger Exit");
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "golfBall")
        {
            //Debug.Log("Player has left the bounds");
            // Need to get parent name, then startMat position, and then move the collider (the ball)
            // to the start position hf
            GameObject parent = gameObject.transform.parent.gameObject;
            Debug.Log(parent);
            Vector3 last_Shot = col.gameObject.GetComponent<Putt_pull>().last_Shot_Location;
                //putt_Script.last_Shot_Location;
            Debug.Log("Last_Shot = " + last_Shot);
            Vector3 start_Point = parent.transform.Find("StartMat").position;
            //Debug.Log("start_Point: " + start_Point);
            // add +.1 to y so you arent in the ground
            if (last_Shot != Vector3.zero)
            {
                shot_Location = last_Shot;
                Debug.Log("Shot reset with last_Shot" + last_Shot);
            }
            else 
            {
                shot_Location = start_Point;
            }
            Debug.Log("Shot Location reset");
            shot_Location.y = shot_Location.y + 0.05f;
            col.transform.position = shot_Location;
            col.attachedRigidbody.velocity = Vector3.zero;
            col.attachedRigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        hole_Script.UpdateHoleInfo();
    }
}

