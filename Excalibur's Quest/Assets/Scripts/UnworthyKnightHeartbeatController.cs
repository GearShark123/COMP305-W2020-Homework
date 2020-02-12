using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnworthyKnightHeartbeatController : MonoBehaviour
{
    [SerializeField] private AudioClip heartbeat;        //human-heartbeat-daniel_simon
    [SerializeField] private AudioSource audioSource;    //Unworthy_Knight    

    private bool isMute = true;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource.PlayOneShot(heartbeat, 1.0F);
        audioSource.clip = heartbeat;
        audioSource.Play();
        MuteSound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MuteSound()
    {
        this.gameObject.GetComponent<AudioSource>().mute = true;
    }

    public void UnmuteSound()
    {
        this.gameObject.GetComponent<AudioSource>().mute = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col is CapsuleCollider2D)
            {
                UnmuteSound();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col is CapsuleCollider2D)
            {
                MuteSound();
            }
        }
    }
}
