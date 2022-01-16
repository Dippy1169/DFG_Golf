using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotation_Speed;
    public float usable_Speed;
    public bool add_Random;
    public float rnd;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (add_Random)
        {
            usable_Speed = Random.Range(-10.0f, 10.0f) + rotation_Speed;
        }
        else
        {
            usable_Speed = rotation_Speed;
        }
        transform.Rotate(new Vector3(0, 0, usable_Speed) * Time.deltaTime);
    }
}
