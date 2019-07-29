using UnityEngine.Events;

// Custom unity event that accepts a single integer parameter (argument)
[System.Serializable]
public class UnityEventInt : UnityEvent<int> { }

// Custom unity event that accepts a single float parameter (argument)
[System.Serializable]
public class UnityEventFloat : UnityEvent<float> { }