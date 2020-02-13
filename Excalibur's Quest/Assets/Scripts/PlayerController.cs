using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class PlayerController : MonoBehaviour
{    
    [SerializeField] private float jumpForce = 8.0f;      
    [SerializeField] private float torque = -3.0f;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject center;
    [SerializeField] private AudioClip sign;             //Sigh-SoundBible.com-1679973774
    [SerializeField] private AudioClip cry;              //Crying-SoundBible.com-1419166957
    [SerializeField] private AudioClip falling;          //Scream And Die Fx-SoundBible.com-299479967
    [SerializeField] private AudioClip captured;         //Hl2_Rebel-Ragdoll485-573931361
    [SerializeField] private AudioClip impact;           //Drop Sword-SoundBible.com-768774345
    [SerializeField] private AudioClip jump;             //Decapitation-SoundBible.com-800292304
    [SerializeField] private AudioClip jump2;            //Swoosh 3-SoundBible.com-1573211927
    [SerializeField] private AudioSource audioSource;    //Excalibur



    private bool isCloseToDeath = false;
    private bool isShake = false;
    private float jumpNum = 2.0f;
    private float time = 0.0f;
    private int deaths = 0;
    private int randomNum;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {        
        //audioSource = this.gameObject.GetComponent<AudioSource>();
        cam = GameObject.Find("CM vcam1");
        //startSpawn = GameObject.Find("Checkpoint/Checkpoint_Spawn");
        //startCenter = GameObject.Find("Checkpoint/Checkpoint_Center");
        rBody = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            CheckPointRespawn();
        }

        if (Input.GetKeyDown("escape"))
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
            //Debug.Log();
        }

        //if (Input.GetAxis("Jump") > 0)
        if (this.gameObject.transform.parent == null && Input.GetButtonDown("Jump"))
        {
            if (jumpNum == 2)
            {
                audioSource.PlayOneShot(jump, 1.0F);
                rBody.AddForce(new Vector2(jumpForce / 2.0f, jumpForce), ForceMode2D.Impulse);
                rBody.AddTorque(torque, ForceMode2D.Impulse);
                jumpNum--;
            }
            else if (jumpNum == 1)
            {
                audioSource.PlayOneShot(jump2, 1.0F);
                rBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
                //rBody.AddTorque(torque, ForceMode2D.Impulse);
                jumpNum--;
            }
        }

        if (isCloseToDeath == true && cam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 5)
        {
            cam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.01f;
        }

        if (isCloseToDeath == false && cam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 8)
        {
            cam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.01f;
        }

        if (isShake == true)
        {
            time += Time.deltaTime;
            if (cam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain == 0)
            {
                cam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.2f;
                cam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 3.0f;
            }
        }
        if (time >= 0.5)
        {
            //Debug.Log("stop " + time);
            cam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
            cam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
            isShake = false;
            time = 0;
        }

        if (deaths == 3)
        {
            randomNum = (int)Random.Range(0.0f, 2.0f);
            if (randomNum == 1)
            {
                audioSource.PlayOneShot(sign, 1.0F);
            }
            else if (randomNum == 0)
            {
                audioSource.PlayOneShot(cry, 1.0F);
            }
            deaths = 0;
        }
    }

    void FixedUpdate()
    {
        
    }

    public void CheckPointRespawn()
    {
        deaths++;
        jumpNum = 2;
        rBody.velocity = new Vector2(0f, 0f);
        rBody.angularVelocity = 0f;
        this.transform.position = spawn.transform.position;
        this.transform.rotation = center.transform.rotation;
        cam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        cam.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    public void Captured()
    {
        audioSource.PlayOneShot(captured, 1.0F);
    }

    public void Falling()
    {
        audioSource.PlayOneShot(falling, 1.0F);
    }

    void OnTriggerEnter2D(Collider2D col)
    {       
        if (col.gameObject.tag == "Respawn")
        {
            cam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 8;
            spawn = col.gameObject.transform.Find("Checkpoint_Spawn").gameObject;
            center = col.gameObject.transform.Find("Checkpoint_Center").gameObject;
        }

        if (col.gameObject.tag == "Zoom")
        {
            isCloseToDeath = true;
        }

        if (col.gameObject.tag == "Ground")
        {
            audioSource.PlayOneShot(impact, 1.0F);
            if (jumpNum == 0)
            {
                isShake = true;
            }
            jumpNum = 2;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Zoom")
        {
            isCloseToDeath = false;
        }
    }
}
