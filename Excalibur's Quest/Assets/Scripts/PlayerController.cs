using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float chargeTimer = 0.0f;
    [SerializeField] private float chargeTime = 0.1f;
    [SerializeField] private float chargeTime2 = 0.2f;
    [SerializeField] private float torque = -3.0f;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject startSpawn;
    [SerializeField] private GameObject startCenter;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject center;

    private bool isCloseToDeath = false;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
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
            //Debug.Log("r");
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
            chargeTimer += Time.time;

        }
        else if (Input.GetButtonUp("Jump"))
        {
            chargeTimer = Time.time - chargeTimer;

            if (chargeTimer <= chargeTime)
            {
                Debug.Log("1");
                //Debug.Log(chargeTimer);
                //rBody.AddForce(new Vector2(jumpForce, jumpForce), ForceMode2D.Impulse);
                rBody.AddForce(new Vector2(jumpForce / 2.0f, jumpForce), ForceMode2D.Impulse);
                rBody.AddTorque(torque, ForceMode2D.Impulse);
            }
            else if (chargeTimer <= chargeTime2 || chargeTimer > chargeTime2)
            {
                Debug.Log("2");
                rBody.AddForce(new Vector2(jumpForce / 2.0f, jumpForce), ForceMode2D.Impulse);
                //rBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            }
            chargeTimer = 0;
        }

        if (isCloseToDeath == true && camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 5) 
        {
            camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.01f;
        }
        
        if (isCloseToDeath == false && camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 8)
        {
            camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.01f;
        }
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            spawn = col.gameObject.transform.Find("Checkpoint_Spawn").gameObject;
            center = col.gameObject.transform.Find("Checkpoint_Center").gameObject;
        }

        if (col.gameObject.tag == "Death")
        {
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
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            isCloseToDeath = false;
        }
    }
}
