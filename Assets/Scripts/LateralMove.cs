using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMove : MonoBehaviour
{
    public float move_Distance;
    public float movement_Speed;
    public Vector3 initial_Position;
    public GameObject platform;
    public float accel_Rate;

    public float max_Speed;
    public float current_Speed;
    public float accel_Rate_Sec;

    private bool direction_Up = true;
    private bool accel;

    // Start is called before the first frame update
    void Start()
    {
        initial_Position = transform.position;
        current_Speed = 0.00f;
    }

    // Update is called once per frame
    void Update()
    {
        /// well start with just y but should expand with a dict for x y and z
        /// 
        /// Looks like our issue here is our world spoace vs local. 
        /// 
        if (movement_Speed >= max_Speed)
        {
            movement_Speed = max_Speed;
        }
        else if (accel)
        {
            movement_Speed += accel_Rate;
        }
        if (direction_Up)
        {
            transform.Translate(new Vector3(0, 0, 1) * movement_Speed * Time.deltaTime);
        }
        if (!direction_Up)
        {
            transform.Translate(new Vector3(0, 0, -1) * movement_Speed * Time.deltaTime);
        }

        if (Mathf.Abs(transform.position.y - initial_Position.y) >= move_Distance)
        {
            Debug.Log("current pos: " + transform.position.y);
            Debug.Log("initial pos: " + initial_Position.y);
            direction_Up = false;
        }
        //else if (Mathf.Abs(transform.position.y - initial_Position.y) <= 0)
        //{
        //    direction_Up = true;
        //}
        
    }

    public void LinearAcceleration(float start_Speed, float max_Speed, float time_To_Max)
    {
        Debug.Log("Linear Accel");

    }
}
