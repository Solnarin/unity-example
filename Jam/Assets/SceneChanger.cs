using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float changeTime;
    public string screenName;
    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            SceneManager.LoadScene(screenName);
        }
        
    }
}
