using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8.0f;      
    [SerializeField] private float torque = -3.0f;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject startSpawn;
    [SerializeField] private GameObject startCenter;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject center;
    [SerializeField] private AudioClip impact;           //Drop Sword-SoundBible.com-768774345
    [SerializeField] private AudioClip jump;             //Decapitation-SoundBible.com-800292304
    [SerializeField] private AudioClip jump2;            //Swoosh 3-SoundBible.com-1573211927
    [SerializeField] private AudioSource audioSource;


    private bool isCloseToDeath = false;
    private bool isShake = false;
    private float jumpNum = 2.0f;
    private float time = 0.0f;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camera = GameObject.Find("CM vcam1");
        startSpawn = GameObject.Find("Checkpoint/Checkpoint_Spawn");
        startCenter = GameObject.Find("Checkpoint/Checkpoint_Center");
        rBody = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            jumpNum = 2;
            //Debug.Log("r");
            rBody.velocity = new Vector2(0f, 0f);
            rBody.angularVelocity = 0f;
            this.transform.position = spawn.transform.position;
            this.transform.rotation = center.transform.rotation;
            camera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            camera.GetComponent<CinemachineVirtualCamera>().enabled = true;
            //Debug.Log();
        }

        if (Input.GetKeyDown("escape"))
        {
            jumpNum = 2;
            rBody.velocity = new Vector2(0f, 0f);
            rBody.angularVelocity = 0f;
            this.transform.position = startSpawn.transform.position;
            this.transform.rotation = startSpawn.transform.rotation;
            camera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            camera.GetComponent<CinemachineVirtualCamera>().enabled = true;
            //Debug.Log();
        }

        //if (Input.GetAxis("Jump") > 0)
        if (Input.GetButtonDown("Jump"))
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

        if (isCloseToDeath == true && camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 5)
        {
            camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.01f;
        }

        if (isCloseToDeath == false && camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 8)
        {
            camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.01f;
        }

        if (isShake == true)
        {
            time += Time.deltaTime;
            if (camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain == 0)
            {
                camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.2f;
                camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 3.0f;
            }
        }
        if (time >= 0.5)
        {
            //Debug.Log("stop " + time);
            camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
            camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
            isShake = false;
            time = 0;
        }
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        audioSource.PlayOneShot(impact, 1.0F);

        if (col.gameObject.tag == "Respawn")
        {
            camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 8;
            spawn = col.gameObject.transform.Find("Checkpoint_Spawn").gameObject;
            center = col.gameObject.transform.Find("Checkpoint_Center").gameObject;
        }

        if (col.gameObject.tag == "Death")
        {
            jumpNum = 2;
            camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 8;
            rBody.velocity = new Vector2(0f, 0f);
            rBody.angularVelocity = 0f;
            this.transform.position = spawn.transform.position;
            this.transform.rotation = center.transform.rotation;
            camera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            camera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        }

        if (col.gameObject.tag == "Enemy")
        {
            isCloseToDeath = true;
        }

        if (col.gameObject.tag == "Ground")
        {
            if (jumpNum == 0)
            {
                isShake = true;
            }
            jumpNum = 2;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            isCloseToDeath = false;
        }
    }
}
