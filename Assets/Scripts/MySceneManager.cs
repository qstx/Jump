using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Load the corresponding scene when a button clicked
    public void LoadScene(string sceneStr)
    {
        SceneManager.LoadScene(sceneStr);
    }
}
