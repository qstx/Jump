using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JammoPlayer : MonoBehaviour
{
    [SerializeField]
    private bool isGaming = false;
    //The amount of speed when jumping
    [SerializeField]
    private float jumpSpeed;
    //Player movement speed
    [SerializeField]
    private float speed;
    [SerializeField]
    private int iniScore = 100;
    //Total score
    [SerializeField]
    public int score = 100;
    //When the game starts
    [SerializeField]
    private float iniTime;
    [SerializeField]
    private Animator animator = null;
    //Used for play time
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text scorerText;
    //Used for play score
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Joystick joystick;
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
            scorerText.text = $"Score:{score- (int)(40 * (1 - Mathf.Exp(-(Time.time - iniTime) / 60)))}";
        }
        //Calculate the direction and size of the movement and move
        if (joystick.vector.magnitude > 0.1f)
        {
            animator.SetBool("walking", true);
            float angle=Vector2.SignedAngle(joystick.vector, Vector2.right);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.Translate(0, 0, speed * Time.deltaTime* joystick.vector.magnitude, Space.Self);
        }
        else
            animator.SetBool("walking", false);
    }
    //Add velocity on player to jump
    public void Jump()
    {
        rigid.velocity = new Vector3(0, jumpSpeed, 0);
    }
    //Initialize Scene
    public void StartGame()
    {
        foreach (Camera camera in cameras)
            camera.fieldOfView = 60;
        FOVSlider.value = Camera.main.fieldOfView;
        score = iniScore;
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
        score -= (int)(40 * (1 - Mathf.Exp(-(Time.time - iniTime) / 60)));
        scoreText.text = $"Score:{score}";
    }
}
