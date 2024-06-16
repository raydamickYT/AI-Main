using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RestartGame : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
    public void RestartGameVoid()
    {
        SceneManager.LoadScene("BehaviourSample");
    }
}
