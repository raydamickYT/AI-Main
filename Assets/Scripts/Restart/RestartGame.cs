using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RestartGame : MonoBehaviour
{
    public void RestartGameVoid()
    {
        SceneManager.LoadScene("BehaviourSample");
    }
}
