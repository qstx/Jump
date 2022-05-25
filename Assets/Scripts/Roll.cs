using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    public float speed=30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed*Time.deltaTime, Space.Self);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Reduce score when hit by players
        if (collision.transform.tag == "Player")
        {
            Debug.Log("OnCollisionEnter");
            --collision.gameObject.GetComponent<JammoPlayer>().score;
        }
    }
}
