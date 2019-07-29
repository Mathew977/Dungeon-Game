using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBossLevel : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        //Check if the player has collided with exit of dungeon level 2
        if (col.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene("Dungeon Game Level 3"); //Load the boss level scen
        }
    }
}
