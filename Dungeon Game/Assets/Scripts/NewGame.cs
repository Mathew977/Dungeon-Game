using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

    public void game()
    {
        SceneManager.LoadScene("Dungeon Game"); //Load the first level scene
    }

    public void finish()
    {
        Application.Quit(); //Quit the game
    }
}
