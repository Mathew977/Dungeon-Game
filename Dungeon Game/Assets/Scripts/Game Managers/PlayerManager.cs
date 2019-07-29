using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    public int Score { get; set; }
    public float Health { get; set; }

    public bool IsDead
    {
        get { return Health <= 0; }
    }

    // Use this for initialization
	protected virtual void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
