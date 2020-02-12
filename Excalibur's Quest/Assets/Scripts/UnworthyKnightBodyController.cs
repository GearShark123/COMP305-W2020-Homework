﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UnworthyKnightBodyController : MonoBehaviour
{
    [SerializeField] private GameObject unworthyKnightPatrolArea;
    [SerializeField] private GameObject excaliburCapturePrefab;
    [SerializeField] private GameObject excaliburCaptureClone;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        unworthyKnightPatrolArea = this.transform.parent.gameObject.transform.parent.gameObject;
        camera = GameObject.Find("CM vcam1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HidePlayer()
    {
        player.SetActive(true);
        camera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        player.GetComponent<PlayerController>().CheckPointRespawn();
        unworthyKnightPatrolArea.GetComponent<UnworthyKnightController>().StartMoving();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            if (col is CapsuleCollider2D)
            {
                unworthyKnightPatrolArea.GetComponent<UnworthyKnightController>().StopMoving();
                player.SetActive(false);
                camera.GetComponent<CinemachineVirtualCamera>().Follow = null;
                excaliburCaptureClone = Instantiate(excaliburCapturePrefab, player.transform.position, player.transform.rotation);
                excaliburCaptureClone.transform.GetChild(0).gameObject.GetComponent<PlayerController>().Captured();
                Destroy(excaliburCaptureClone, 3.0f);
                Invoke("HidePlayer", 3.0f);              
            }
        }
    }
}
