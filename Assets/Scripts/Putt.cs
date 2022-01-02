using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Putt : MonoBehaviour
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
    private Vector3 force_Vector = Vector3.zero;
    public Vector3 ready_To_Hit_Velocity = new Vector3(0.2f, 0.2f, 0.2f);

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
        // Debug.Log("Current Velocity: " + rb.velocity);
        //if (rb.velocity == Vector3.zero)
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
            is_Pressed = Input.GetButtonDown("Fire1");
            if (is_Pressed)
            {
                is_Moving = true;
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
                // Needed ray cast here instead of just cast point
                force_Vector = (world_Pos - rb.position);
                Debug.Log("Force Vector Ratio: " + force_Vector);
                // Take ratio on X/Z to send Putt Strength
                // Debug.Log((Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z)));
                // Debug.Log((force_Vector.x / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z))));
                putt_Strength = puttForce(force_Vector) * putt_Strength_Multiplier;
                if (putt_Strength > max_Putt_Strength)
                {
                    putt_Strength = max_Putt_Strength;
                    Debug.Log("Max Putt Strength Used");
                }
                Debug.Log("Using Putt Strength of: " + putt_Strength + " From Multiplier: " + putt_Strength_Multiplier);
                x_Force = putt_Strength * (force_Vector.x / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z)));
                z_Force = putt_Strength * (force_Vector.z / (Mathf.Abs(force_Vector.x) + Mathf.Abs(force_Vector.z)));
                force_Vector = new Vector3(x_Force, 0, z_Force);
                Debug.Log("Force Vector: " + force_Vector);
                rb.AddForce(new Vector3(x_Force, 0, z_Force));
                // if (rb.velocity == Vector3.zero)
                System.Threading.Thread.Sleep(100);
                while (Mathf.Abs(rb.velocity.x) > ready_To_Hit_Velocity.x && Mathf.Abs(rb.velocity.y) > ready_To_Hit_Velocity.y && Mathf.Abs(rb.velocity.z) > ready_To_Hit_Velocity.z)
                {
                    Debug.Log("Waiting for Velocity Change");
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }

    private float puttForce(Vector3 force_Vect)
    {
        Debug.Log("Put Force Function");
        float total_Force = Mathf.Sqrt((force_Vect.x *force_Vect.x) + (force_Vect.z * force_Vect.z));
        return total_Force;
    }
}
