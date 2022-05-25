using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public Vector3 iniScale;
    public float noise1;
    public float noise2;
    // Start is called before the first frame update
    void Start()
    {
        iniScale = transform.localScale;
        noise1 = Random.Range(0.3f, 0.5f);
        noise2 = Random.Range(0, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        //Scale regularly over time
        transform.localScale = (noise1 * Mathf.Sin(Time.time+noise2) + 1)*iniScale;
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
