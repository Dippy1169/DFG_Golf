using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gotoMenu : MonoBehaviour
{
    public string scene_Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc key pressed");
            GameObject golfBall = GameObject.FindGameObjectsWithTag("golfBall")[0];
            //EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            //eventSystem.enabled = false;
            if (!SceneManager.GetSceneByName(scene_Name).isLoaded)
                {
                SceneManager.LoadScene(scene_Name, LoadSceneMode.Additive);

            }
        }
    }
}
