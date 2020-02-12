using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallingController : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("CM vcam1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DelayCamera()
    {
        cam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        player.GetComponent<PlayerController>().CheckPointRespawn();        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            if (col is CapsuleCollider2D)
            {
                cam.GetComponent<CinemachineVirtualCamera>().Follow = null;
                col.gameObject.GetComponent<PlayerController>().Falling();
                Invoke("DelayCamera", 2.0f);
            }
        }
    }
}
