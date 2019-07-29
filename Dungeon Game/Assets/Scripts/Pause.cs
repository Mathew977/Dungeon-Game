using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    
    private bool pause = false;

    public GameObject Pauses;
	// Use this for initialization
	void Start ()
    {
        Pauses.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("pauses"))
        {
            pause = !pause; //Change the boolean value of pause
        }

        if (pause)
        {
            Pauses.SetActive(true);
            Time.timeScale = 0;
        }

        if (!pause)
        {
            Pauses.SetActive(false);
            Time.timeScale = 1;
        }
	}
}
