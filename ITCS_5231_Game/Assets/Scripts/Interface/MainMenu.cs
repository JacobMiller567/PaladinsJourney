using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
  {
    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Go to next scene from scene manager
    SceneManager.LoadScene("Level_1");
    
  }

  public void QuitGame()
  {
    Application.Quit();
    UnityEditor.EditorApplication.isPlaying = false; //TEST?
  }
  
  public void Menu()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
