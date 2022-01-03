using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector3 force_Vector = Vector3.zero;
    public Vector3 ready_To_Hit_Velocity = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 world_Pos = Vector3.zero;

    public Vector2 mouse_Init = Vector2.zero;
    public Vector2 mouse_Release = Vector2.zero;
    public Vector2 mouse_Force_Pythag = Vector2.zero;

    private bool click_Trigger = true;
    private bool initial_Mouse_Trigger = true;



    public Transform clicked_Point;

    private int iterator;

    public Rigidbody rb;

    void Start()
    {
        is_Moving = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
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
                    click_Trigger = true;
                    Vector3 mouse = Input.mousePosition;
                    Vector3 world_Pos;
                    Ray ray = Camera.main.ScreenPointToRay(mouse);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000f))
                    {
                        world_Pos = hit.point;
                    }
                    else
                    {
                        world_Pos = Camera.main.ScreenToWorldPoint(mouse);
                    }
                    // Instantiate(clicked_Point, world_Pos, Quaternion.identity);
                    Vector3 castPoint = Camera.main.ScreenToWorldPoint(mouse);
                    Debug.Log("transform position" + transform.position);
                    Debug.Log("Where we clicked in the world (castPoint): " + castPoint);
                    Debug.Log("Rigid body position: " + rb.position);
                    Debug.Log("World Pos (ray using cast): " + world_Pos);
                    initial_Mouse_Trigger = false;
                }
                mouse_Release = Input.mousePosition;
            }
            // If our click Trigger was hit, we should then use the 2 locations we got and setup the restulting force vector to apply to ball.
            // Translating the 2 xy coordinates to 1 vector. This is our putt strength
            if (click_Trigger)
            {
                if (!Input.GetMouseButton(0))
                {
                    mouse_Force_Pythag = new Vector2((mouse_Init.x - mouse_Release.x), (mouse_Init.y - mouse_Release.y));
                    putt_Strength = puttForce(mouse_Force_Pythag) * putt_Strength_Multiplier;
                    if (putt_Strength > max_Putt_Strength)
                    {
                        putt_Strength = max_Putt_Strength;
                        Debug.Log("Max Putt Strength Used");
                    }
                    Debug.Log("Using Putt Strength of: " + putt_Strength + " From Multiplier: " + putt_Strength_Multiplier);
                    force_Vector = (world_Pos - rb.position);
                    x_Force = putt_Strength * (force_Vector.x / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.y)));
                    z_Force = putt_Strength * (force_Vector.y / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.y)));
                    force_Vector = new Vector3(x_Force, 0, z_Force);
                    Debug.Log("Force Vector: " + force_Vector);
                    rb.AddForce(force_Vector);
                    // if (rb.velocity == Vector3.zero)
                    // System.Threading.Thread.Sleep(100);
                    //while (Mathf.Abs(rb.velocity.x) > ready_To_Hit_Velocity.x && Mathf.Abs(rb.velocity.y) > ready_To_Hit_Velocity.y && Mathf.Abs(rb.velocity.z) > ready_To_Hit_Velocity.z)
                    //{
                    //    Debug.Log("Waiting for Velocity Change");
                    //    System.Threading.Thread.Sleep(100);
                    //}
                    is_Moving = true;
                    click_Trigger = false;
                    initial_Mouse_Trigger = true;
                }
            }
            // It works, now we need to correctly orient our vectors so that our force vectors signs are the same we expect from our rigid body to our click
            // We are translating XY to XZ

        }


            //is_Pressed = Input.GetButtonDown("Fire1");
            
            //    // Instantiate(clicked_Point, world_Pos, Quaternion.identity);
            //    Vector3 castPoint = Camera.main.ScreenToWorldPoint(mouse);
            //    Debug.Log("transform position" + transform.position);
            //    Debug.Log("Where we clicked in the world (castPoint): " + castPoint);
            //    Debug.Log("Rigid body position: " + rb.position);
            //    Debug.Log("World Pos (ray using cast): " + world_Pos);
            //    // Needed ray cast here instead of just cast point
            //    force_Vector = (world_Pos - rb.position);
            //    Debug.Log("Force Vector Ratio: " + force_Vector);
            //    // Take ratio on X/Z to send Putt Strength
            //    // Debug.Log((Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z)));
            //    // Debug.Log((force_Vector.x / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z))));
            //    putt_Strength = puttForce(force_Vector) * putt_Strength_Multiplier;
            //    if (putt_Strength > max_Putt_Strength)
            //    {
            //        putt_Strength = max_Putt_Strength;
            //        Debug.Log("Max Putt Strength Used");
            //    }
            //    Debug.Log("Using Putt Strength of: " + putt_Strength + " From Multiplier: " + putt_Strength_Multiplier);
            //    x_Force = putt_Strength * (force_Vector.x / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z)));
            //    z_Force = putt_Strength * (force_Vector.z / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z)));
            //    force_Vector = new Vector3(x_Force, 0, z_Force);
            //    Debug.Log("Force Vector: " + force_Vector);
            //    rb.AddForce(new Vector3(x_Force, 0, z_Force));
            //    // if (rb.velocity == Vector3.zero)
            //    System.Threading.Thread.Sleep(100);
            //    while (Mathf.Abs(rb.velocity.x) > ready_To_Hit_Velocity.x && Mathf.Abs(rb.velocity.y) > ready_To_Hit_Velocity.y && Mathf.Abs(rb.velocity.z) > ready_To_Hit_Velocity.z)
            //    {
            //        Debug.Log("Waiting for Velocity Change");
            //        System.Threading.Thread.Sleep(100);
            //    }
            //}
    }

    private float puttForce(Vector2 force_Vect)
    {
        Debug.Log("Put Force Function");
        float total_Force = Mathf.Sqrt((force_Vect.x *force_Vect.x) + (force_Vect.y * force_Vect.y));
        return total_Force;
    }
}
