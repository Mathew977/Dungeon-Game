using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        //Check if the player has collided with the exit of level 1
        if (col.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene("Dungeon Game Level 2"); //Load the level 2 scene
        }
    }
}