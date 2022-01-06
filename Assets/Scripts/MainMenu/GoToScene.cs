using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{

    public void NextScene(string go_To_Scene)
    {
        Debug.Log("Button Pressed, going to scene: " + go_To_Scene);
        SceneManager.LoadScene(go_To_Scene);
        Debug.Log("Scene Change");
    }

    public void Exit()
    {
        Debug.Log("Exit Pressed");
        Application.Quit();
    }
}
