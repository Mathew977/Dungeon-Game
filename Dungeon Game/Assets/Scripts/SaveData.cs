using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

    public void SavDat()
    {
        PlayerPrefs.SetFloat("PlayX", transform.position.x);
        PlayerPrefs.SetFloat("PlayY", transform.position.y);
        PlayerPrefs.SetFloat("PlayZ", transform.position.z);
    }

    public void LoaDat()
    {
        float x = PlayerPrefs.GetFloat("PlayX");
        float y = PlayerPrefs.GetFloat("PlayY");
        float z = PlayerPrefs.GetFloat("PlayZ");
        transform.position = new Vector3(x, y, z);
    }
}
