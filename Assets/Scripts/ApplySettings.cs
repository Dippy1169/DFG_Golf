using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ApplySettings : MonoBehaviour
{

    GameObject inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_Settings()
    {
        string text = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log(text);
    }
}
