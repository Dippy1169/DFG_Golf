using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtObject : MonoBehaviour
{
    public float degrees;
    public GameObject ObjectToLookAt;
    public CinemachineFreeLook freeLookCam;

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
        /// Attached to a trigger, while our ball is within the trigger, we will look at the attached object
        /// 
        /// Using arc tan here is a problem. Tan gives us an asymptote
        /// 
        if (col.gameObject.tag == "golfBall")
        {
            GameObject hole_Num = transform.parent.gameObject;

            Vector3 pos_To_Look = ObjectToLookAt.transform.position;
            float x_Delta = col.transform.position.x - pos_To_Look.x;
            float z_Delta = col.transform.position.z - pos_To_Look.z;
            if (z_Delta > 0)
            {
                degrees = Mathf.Atan(z_Delta / x_Delta) * (180 / Mathf.PI);
            }
            else
            {
                degrees = Mathf.Atan(x_Delta / z_Delta) * (180 / Mathf.PI);
            }

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

            freeLookCam.m_XAxis.Value = degrees;
        }
    }
}

