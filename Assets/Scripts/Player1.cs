using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour
{
    //Used for debug in Unity Play mode
    [SerializeField]
    public bool isDeubg = true;
    [SerializeField]
    private bool isGaming = false;
    //The amount of force applied when jumping
    [SerializeField]
    private int forceZ;
    [SerializeField]
    private int forceY;
    [SerializeField]
    private int iniScore = 100;
    //Total score
    [SerializeField]
    public int score = 100;
    //When the game starts
    [SerializeField]
    private float iniTime;
    [SerializeField]
    private Rigidbody rigid = null;
    //Where the bullet was fired
    [SerializeField]
    private Vector3 launchPos = new Vector3(0, 0, 0.5f);
    //Bullet firing speed
    [SerializeField]
    private Vector3 iniV = new Vector3(0, 0, 1f);
    //Used for play time
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text scorerText;
    //Used for play score
    [SerializeField]
    private Text scoreText;
    //The bullet prefab
    [SerializeField]
    private GameObject projectilePrefab = null;
    [SerializeField]
    private GameObject WallPrefab = null;
    [SerializeField]
    private GameObject Wall = null;
    //Game Start UI
    [SerializeField]
    private GameObject StartPanel;
    //Game Over UI
    [SerializeField]
    private GameObject EndPanel;
    [SerializeField]
    private List<Camera> cameras = new List<Camera>();
    [SerializeField]
    private Slider FOVSlider;
    private Vector2 touchPoint;
    private bool isJumping = false;
    private float jumpTime = 0;
    [SerializeField]
    private float jumpTotalTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize UI
        StartPanel.SetActive(true);
        EndPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Play time
        if (isGaming)
        {
            timerText.text = $"Time:{(Time.time - iniTime).ToString("F2")}";
            scorerText.text = $"Score:{score - (int)(40 * (1 - Mathf.Exp(-(Time.time - iniTime) / 60)))}";
        }
        //Calculate jump time
        if (isJumping)
        {
            jumpTime -= Time.deltaTime;
            if (jumpTime <= 0)
            {
                isJumping = false;
                jumpTime = jumpTotalTime;
            }
        }

    }
    //Initialize Scene
    public void StartGame()
    {
        if (Wall != null)
            Destroy(Wall);
        if (WallPrefab != null)
            Wall = Instantiate(WallPrefab, Vector3.forward * 12.5f, Quaternion.identity);
        foreach (Camera camera in cameras)
            camera.fieldOfView = 60;
        FOVSlider.value = Camera.main.fieldOfView;
        score = 100;
        iniTime = Time.time;
        isGaming = true;
        StartPanel.SetActive(false);
        EndPanel.SetActive(false);
    }
    //Restart game
    public void RestartGame()
    {
        //Initialize player
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        StartGame();
    }
    //Add force on player to jump
    public void Jump()
    {
        rigid.AddForce(transform.rotation * Vector3.forward * forceZ + transform.up * forceY);
        //rigid.AddRelativeForce(Vector3.forward * forceZ + Vector3.up * forceY);
        //rigid.velocity = transform.rotation * Vector3.forward * forceZ + transform.up * forceY;
    }
    //Calculate the direction of the jump and jump when the button clicked
    public void GetHitPoint()
    {
        Debug.Log("GetHitPoint");
        if (isJumping)
            return;
        else
            isJumping = true;
        if (isDeubg)
            touchPoint = Input.mousePosition;
        else
            touchPoint = Input.GetTouch(0).position;
        Debug.Log("GetHitPointEnd");
        Ray ray = Camera.main.ScreenPointToRay(touchPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "floor")
            {
                transform.LookAt(hit.point);
                Debug.Log(hit.point);
                Jump();
            }
        }
        else
            Debug.Log("no hit");
    }
    //Fire bullets when the button is pressed
    public void Fire()
    {
        GameObject game = Instantiate(projectilePrefab, transform.position + transform.rotation * launchPos, transform.rotation);
        game.GetComponent<Rigidbody>().velocity = transform.rotation * iniV;
        Destroy(game, 10f);
    }
    //Change mian camera
    public void CameraSwitch()
    {
        if (Camera.main == cameras[0])
        {
            cameras[0].enabled = false;
            cameras[1].enabled = true;
            FOVSlider.value = cameras[1].fieldOfView;
        }
        else
        {
            cameras[1].enabled = false;
            cameras[0].enabled = true;
            FOVSlider.value = cameras[0].fieldOfView;
        }
    }
    //Change FOV
    public void OnFOVChanged(Slider slider)
    {
        Camera.main.fieldOfView = slider.value;
    }
    //Exit game
    public void ExitGame()
    {
        SceneManager.LoadScene("LaunchScene");
    }
    //Show the score you get when game over
    public void EndGame()
    {
        EndPanel.SetActive(true);
        score -= (int)(40*(1-Mathf.Exp(-(Time.time - iniTime)/60)));
        scoreText.text = $"Score:{score}";
    }
}
