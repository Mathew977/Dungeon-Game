using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerWeapon : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if (GameManager.Instance.IsGameOver != true)
        {
            //Check if weapon collided with an enemy
            if (col.gameObject.tag.Equals("Enemy"))
            {
                Damage weapon = this.gameObject.GetComponent<Damage>(); //Set weapon to an instance of the weapons damage script
                Health enemyHealth = col.gameObject.GetComponent<Health>(); //Set enemyHealth to an instance of the enemies health script

                //Check if weapon exists and if the enemy is already dead/dying
                if (weapon != null && enemyHealth.Value > 0)
                {
                    enemyHealth.Value = Mathf.Max(enemyHealth.Value - 1, 0); //Subtract 1 from the enemies health
                    //Debug.Log(enemyHealth.Value);
                    //Check if enemy has run out of health
                    if (enemyHealth.Value < 1)
                    {
                        col.gameObject.GetComponent<AI>().DieEnemy(); //Call the enemies DieEnemy function

                        if (col.gameObject.name.Equals("Boss")) //Check if enemy being killed the boss enemy
                        {
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            SceneManager.LoadScene("EndGameScreen"); //Load the EndGameScreen scene
                        }
                    }
                }
            }
        }
    }
}
