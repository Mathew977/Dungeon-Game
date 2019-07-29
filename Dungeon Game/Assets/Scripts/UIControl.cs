using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControl : MonoBehaviour {
    [SerializeField]
    private Text pointCountText; //Text game object to display point count
    [SerializeField]
    private GameObject healthPointBar; //The health point bar game object

    private RectTransform hpRectTransform; //Will store a reference to the rectangle component attached to the health point bar game object
    private float hpBarFullWidth; //Will store the original width of the rectangle component

	// Use this for initialization
	void Start ()
    {
        pointCountText.text = PlayerManager.Instance.Score.ToString();

        if (healthPointBar != null)
        {
            hpRectTransform = healthPointBar.GetComponent<RectTransform>();
            hpBarFullWidth = hpRectTransform.rect.width;
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        pointCountText.text = PlayerManager.Instance.Score.ToString();
        UpdateHealthPointBar();
    }

    void UpdateHealthPointBar()
    {
        if (healthPointBar = null)
            return;

        float newWidth = PlayerManager.Instance.Health / 10f * hpBarFullWidth;

        //Resize the health point bar to give the illusion of decreasing health
        hpRectTransform.sizeDelta = new Vector2(newWidth, hpRectTransform.rect.height);
    }
}
