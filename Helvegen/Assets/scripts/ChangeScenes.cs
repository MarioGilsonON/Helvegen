using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
  public void Instructions()
    {
        SceneManager.LoadScene("instructions");
    }

    public void ToBattle()
    {
        SceneManager.LoadScene("battle1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
