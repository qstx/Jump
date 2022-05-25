using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float idealSpeed=30;
    [SerializeField]
    private float noise = 5;
    [SerializeField]
    private float realSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //Generate random speed
        realSpeed = Random.Range(idealSpeed - noise, idealSpeed + noise);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, realSpeed * Time.deltaTime, Space.Self);
    }
}
