using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainGameScene : MonoBehaviour
{
    public void CallMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
