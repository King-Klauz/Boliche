using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
   public void startGame()
   {
        SceneManager.LoadScene("Lane");
   }
   public void exitGame()
   {
        Application.Quit();
   }
}
