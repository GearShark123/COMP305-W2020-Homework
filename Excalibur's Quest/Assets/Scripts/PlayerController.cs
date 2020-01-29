using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float chargeTimer = 0.0f;
    [SerializeField] private float chargeTime = 0.1f;
    [SerializeField] private float chargeTime2 = 0.2f;
    [SerializeField] private float torque = -3.0f;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject spawn;    

    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
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
            this.transform.rotation = spawn.transform.rotation;
            //camera.GetCinemachineComponent<CinemachineVirtualCamera>();
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
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        spawn = col.gameObject.transform.Find("Spawn").gameObject;
    }
}
