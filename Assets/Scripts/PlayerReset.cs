using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{


    void OnTriggerExit(Collider col)
    {
        //Debug.Log("On Trigger Exit");
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "golfBall")
        {
            Debug.Log("Player has left the bounds");
            // Need to get parent name, then startMat position, and then move the collider (the ball)
            // to the start position hf
            GameObject parent = gameObject.transform.parent.gameObject;
            Debug.Log(parent);
            Vector3 start_Point = parent.transform.Find("StartMat").position;
            Debug.Log("start_Point: " + start_Point);
            // add +.1 to y so you arent in the ground
            start_Point.y = start_Point.y + 0.1f;
            col.transform.position = start_Point;
            col.attachedRigidbody.velocity = Vector3.zero;
            col.attachedRigidbody.angularVelocity = Vector3.zero;
        }
    }
}

