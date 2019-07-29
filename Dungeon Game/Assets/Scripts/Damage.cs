using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    [SerializeField]
    private float damage = 1.0f; //The amount of damage the rocket causes

    public float Damages
    {
        get { return damage; }
        set { damage = value; }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
