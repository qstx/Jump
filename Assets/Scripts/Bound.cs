using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    [SerializeField]
    private Vector3 iniPos;
    // Start is called before the first frame update
    void Start()
    {
        //Get the player's initial position
        iniPos = FindObjectOfType<Player1>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        //Reset player's position
        if (other.tag == "Player")
        {
            other.transform.position = iniPos;
            other.transform.rotation = Quaternion.identity;
        }
        else
            Destroy(other.gameObject);
    }
}
