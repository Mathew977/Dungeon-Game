using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyWeapon : MonoBehaviour {

    
    private void OnTriggerEnter(Collider col)
    {
        if (GameManager.Instance.IsGameOver != true)
        {
            //Check if player character was hit
            if (col.gameObject.tag.Equals("Player"))
            {
                Damage weapon = this.gameObject.GetComponent<Damage>(); //Set weapon to an instance of the Damage script attached to this enemy 
                Health playerHealth = col.gameObject.GetComponent<Health>(); //Set playerHealth to an instance of the Health script attached to the player character
                //Check if weapon is null
                if (weapon != null)
                {
                    playerHealth.Value = Mathf.Max(playerHealth.Value - 1, 0); //Subtract the player character's health by one
                    //Debug.Log(playerHealth.Value);
                    //Check if player has died
                    if (playerHealth.Value < 1)
                    {
                        col.gameObject.GetComponent<Player>().Die(); //Run the function which handles the player's death
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene("GameOver"); //Load the GameOver scene
                    }
                }
            }
        }
    }
}
