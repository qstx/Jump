using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Show random colors when the game starts
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Reduce score when hit by players
        if (collision.transform.tag == "Player")
        {
            --collision.gameObject.GetComponent<Player1>().score;
        }
    }
}
