using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    protected float reloadTime = 5.0f;

    [SerializeField]
    protected float dyingTime = 3.0f;

    protected Health health;

    [SerializeField]
    protected DeathTypes deathType; //Indicates character's death type

    protected bool isAttacking; //Indicates if character is currently attacking
    Animator animator; //Will store a reference to the animator component attached to the game object

	// Use this for initialization
	protected virtual void Start ()
    {
        health = GetComponentInChildren<Health>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	//protected virtual void Update ()
 //   {
 //       //if (health.Value < 1)
 //       //{
 //       //    //Destroy the character after the specified duration
 //       //    Die();
 //       //}
 //   }

    //protected virtual void Die()
    //{
    //    //Destroy(this.gameObject);
    //    animator.SetBool("Dead", true);
    //    animator.SetInteger("DeathType", (int)deathType);
    //}

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.IsGameOver != true)
        {
            if (collision.transform.CompareTag("Weapon"))
            {
                Damage weapon = collision.gameObject.GetComponent<Damage>();
                if (weapon != null)
                {
                    health.Value = Mathf.Max(health.Value - weapon.Damages, 0);
                    //if (health.Value < 1)
                    //{
                    //    //Destroy the character after the specified duration
                    //    Die();
                    //}
                }
            }
        }
    }

    private void OnAnimatorMove()
    {
        if (animator = null)
            return;
        animator.SetBool("Attacking", isAttacking);
    }
}
