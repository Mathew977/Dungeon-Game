using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    Animator animator; //Will store a reference to the animator component attached to the game object

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        animator = GetComponent<Animator>();
        UpdatePlayerHealth();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            animator.SetBool("Attacking", isAttacking);
            //rocketLauncher.Fire(reloadTime);
        }
        else
        {
            isAttacking = false;
        }

        UpdatePlayerHealth();
    }

    //protected override void OnCollisionEnter(Collision collision)
    //{
    //    base.OnCollisionEnter(collision);
    //    UpdatePlayerHealth();
    //}

    private void UpdatePlayerHealth()
    {
        if (health != null)
        {
            PlayerManager.Instance.Health = health.Value;
            if (health.Value == 0)
            {
                GameManager.Instance.DoGameOver();
            }
        }
    }
    public void Die()
    {
        //Destroy(this.gameObject);
        animator.SetBool("Dead", true);
        animator.SetInteger("DeathType", (int)deathType);
        Destroy(this.gameObject, dyingTime);
    }
}
