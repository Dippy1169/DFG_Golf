using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Putt_pull : MonoBehaviour
{
    public float putt_Strength;
    public float putt_Strength_Multiplier = 100.0f;
    public float max_Putt_Strength = 200000.0f;
    private float x_Velocity;
    private Vector3 move_Velocity = Vector3.zero;
    private bool is_Pressed;
    private bool is_Moving;
    private float x_Force;
    private float z_Force;
    private float x_Force_Ratio;
    private float z_Force_Ratio;

    public Vector3 force_Vector = Vector3.zero;
    public Vector3 force_Vector_Ratio = Vector3.zero;
    public Vector2 position_Vector = Vector2.zero;
    public Vector3 ready_To_Hit_Velocity = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 world_Pos = Vector3.zero;
    public Vector3 world_Clicked_Pos = Vector3.zero;
    public Vector3 ball_Pos = Vector3.zero;

    public Vector2 mouse_Init = Vector2.zero;
    public Vector2 mouse_Release = Vector2.zero;
    public Vector2 mouse_Force_Pythag = Vector2.zero;
    public Vector2 mouse_Force_Delta = Vector2.zero;

    private bool click_Trigger = true;
    private bool initial_Mouse_Trigger = true;

    public TMP_Text power_Text;
    public TMP_Text mouse_Delta;
    public TMP_Text putt_Count;
    private int putt_Counter;

    private float start_Cam_Look;
    public CinemachineFreeLook freeLookCam;

    public Transform clicked_Point;

    private int iterator;

    public Rigidbody rb;

    void Start()
    {
        is_Moving = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        putt_Counter = 0;
        // Get Hole1's game object
        GameObject start_Hole = GameObject.Find("Hole1");
        // Get StartMat game object and position
        GameObject start_Mat_Child = start_Hole.transform.Find("StartMat").gameObject;
        //Transform next_Mat = start_Hole.transform.Find("StartMat");
        Vector3 move_Pos = start_Mat_Child.transform.position;
        this.transform.position = move_Pos;
        // Get StartLook game object and position
        GameObject start_Look = start_Hole.transform.Find("StartLook").gameObject;
        Quaternion start_Rot = start_Look.transform.rotation;
        Debug.Log("Start Quat: " + start_Look.transform.rotation);
        this.transform.rotation = start_Rot * Quaternion.Inverse(this.transform.rotation);
        start_Cam_Look = Vector3.Angle(this.transform.position, this.transform.position - start_Look.transform.position);
        Debug.Log("Shit SHit Ass " + Vector3.Angle(this.transform.position, this.transform.position - start_Look.transform.position));
        freeLookCam.m_XAxis.Value = start_Cam_Look;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) < ready_To_Hit_Velocity.x && Mathf.Abs(rb.velocity.y) < ready_To_Hit_Velocity.y && Mathf.Abs(rb.velocity.z) < ready_To_Hit_Velocity.z)
        {
            if (is_Moving)
            {
                Debug.Log("rb should be 0 = " + rb.velocity);
            }
            is_Moving = false;
        }
        if (!is_Moving)
        {
            // If the ball isnt moving, we want to record where the mouse initially clicked, then the location where they release the mouse button
            // is_Pressed = Input.GetMouseButtonDown(0);
            if (Input.GetMouseButton(0))
            {
                if (initial_Mouse_Trigger)
                {
                    mouse_Init = Input.mousePosition;
                    Debug.Log("Where we clicked on the actual screen (XY): " + mouse_Init);
                    click_Trigger = true;
                    Vector3 mouse = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(mouse);
                    RaycastHit hit;
                    // Where we clicked in our world
                    if (Physics.Raycast(ray, out hit, 1000f))
                    {
                        world_Clicked_Pos = hit.point;
                    }
                    else
                    {
                        world_Clicked_Pos = Camera.main.ScreenToWorldPoint(mouse);
                    }
                    // Where our ball is in our world
                    ball_Pos = rb.position;
                    Debug.Log("Where our ball is in our world: " + ball_Pos);
                    Debug.Log("Were we clicked in our world: " + world_Clicked_Pos);
                    // These 2 values will tell us what ratio of force should be applied to the ball in XZ
                    // This is the direction our ball will travel
                    force_Vector_Ratio = world_Clicked_Pos - ball_Pos;
                    x_Force_Ratio = force_Vector_Ratio.x / (Mathf.Abs(force_Vector_Ratio.x) + Mathf.Abs(force_Vector_Ratio.z));
                    z_Force_Ratio = force_Vector_Ratio.z / (Mathf.Abs(force_Vector_Ratio.x) + Mathf.Abs(force_Vector_Ratio.z));

                    initial_Mouse_Trigger = false;
                }
                mouse_Release = Input.mousePosition;
                mouse_Delta.text = (mouse_Init.y - mouse_Release.y).ToString();
            }
            // If our click Trigger was hit, we should then use the 2 locations we got and setup the restulting force vector to apply to ball.
            // Translating the 2 xy coordinates to 1 vector. This is our putt strength
            if (click_Trigger)
            {
                if (!Input.GetMouseButton(0))
                {
                    // Here we should have a force ratio, how much of total force should be applied in x and z. 
                    // On mouse release we need to make a force to apply. This would be the delta in Y from mouse click to mouse release on the screen
                    mouse_Force_Delta = new Vector2((mouse_Init.x - mouse_Release.x), (mouse_Init.y - mouse_Release.y));

                    // This is our putt strength, how much we put on the ball. Absolute value because we correct that with our force ratios
                    putt_Strength = Mathf.Abs(mouse_Force_Delta.y) * putt_Strength_Multiplier;
                    if (putt_Strength > max_Putt_Strength)
                    {
                        putt_Strength = max_Putt_Strength;
                        Debug.Log("Max Putt Strength Used");
                    }
                    if (putt_Strength > 0)
                    {
                        power_Text.text = putt_Strength.ToString();
                        putt_Counter++;
                        putt_Count.text = putt_Counter.ToString();
                    }
                    Debug.Log("Using Putt Strength of: " + putt_Strength + " From Multiplier: " + putt_Strength_Multiplier);

                    // Here we combine our putt force we just calculated. Basic calculation: Putt force * Putt force ratio.
                    x_Force = putt_Strength * x_Force_Ratio;
                    z_Force = putt_Strength * z_Force_Ratio;
                    force_Vector = new Vector3(x_Force, 0, z_Force);
                    Debug.Log("Final Force Vector: " + force_Vector);
                    rb.AddForce(force_Vector);

                    is_Moving = true;
                    click_Trigger = false;
                    initial_Mouse_Trigger = true;
                }
            }
            // It works, now we need to correctly orient our vectors so that our force vectors signs are the same we expect from our rigid body to our click
            // We are translating XY to XZ
            // Problem seems to be we arent hitting the correct click position and rigid body positon.

        }

    }

    private float puttForce(Vector2 force_Vect)
    {
        Debug.Log("Put Force Function");
        float total_Force = Mathf.Sqrt((force_Vect.x *force_Vect.x) + (force_Vect.y * force_Vect.y));
        return total_Force;
    }
}
