using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //private float radius;

    [SerializeField]
    private string iconImage;

    public bool ShowGizmos { get; set; }

    public float MarkerRadius { get; set; }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (MarkerRadius == 0f)
            MarkerRadius = 1f;
        if (ShowGizmos && !string.IsNullOrEmpty(iconImage))
        {
            Gizmos.DrawWireSphere(transform.position, MarkerRadius);
            Gizmos.DrawIcon(transform.position, iconImage);
        }
    }
}