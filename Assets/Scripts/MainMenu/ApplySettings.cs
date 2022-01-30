using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplySettings : MonoBehaviour
{

    public TMP_InputField mass_Input;
    public TMP_InputField drag_Input;
    private Rigidbody rb;

    public void ButtonClick()
    {
        Debug.Log("You clicked the button! good job!");
        Debug.Log("Input Field is: " + mass_Input.text);
    }

    public void ApplySettingsFunc()
    {
        //GameObject ball = GameObject.Find("GolfBall");
        GameObject ball = GameObject.FindGameObjectWithTag("golfBall");
        rb = ball.GetComponent<Rigidbody>();
        if (mass_Input.text != "")
        {
            rb.mass = float.Parse(mass_Input.text);
        }
        if (drag_Input.text != "")
        {
            rb.drag = float.Parse(drag_Input.text);
        }
                    
    }
}
