using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Health : MonoBehaviour
{

    private float maxValue;

    // A set of events that will execute when the health changes
    [SerializeField, Header("Register to know when my health changes")]
    private UnityEventFloat HealthChanged;

    [SerializeField, Header("Register to know when I run out of health")]
    private UnityEvent HealthExpired;


    [SerializeField]
    private float _value = 10f;

    public float Value
    {
        get { return _value; }
        set
        {
            _value = value;

            // Check that an event listener for the HealthChanged event exists
            // invoke the event (call on the listener method) and pass it the new health percentage
            if (HealthChanged != null) HealthChanged.Invoke(HealthPercent);

            // Check if the health value has now dropped to zero
            // Check that an event listener for the HealthExpired event exists
            // invoke the event (call on the listener method) - does not need any value passed to it
            if (_value <= 0f)
                if (HealthExpired != null) HealthExpired.Invoke();
        }
    }


    public float HealthPercent
    {
        get { return Value / maxValue * 100; }
    }

    // Use this for initialization
    void Start()
    {
        maxValue = Value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        // Remove all the event listeners when the game object is destroyed
        HealthExpired.RemoveAllListeners();
        HealthChanged.RemoveAllListeners();
    }

    public void AddHealthExpiredListener(UnityAction action)
    {
        if (HealthExpired == null)
            HealthExpired = new UnityEvent();

        HealthExpired.AddListener(action);
    }
}
