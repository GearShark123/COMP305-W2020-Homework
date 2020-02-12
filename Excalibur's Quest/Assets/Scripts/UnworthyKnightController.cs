using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnworthyKnightController : MonoBehaviour
{
    [SerializeField] private float speed = -0.02f;
    [SerializeField] private GameObject unworthyKnight;
    [SerializeField] private Transform point;
    [SerializeField] private Transform point2;
    [SerializeField] private AudioClip footsteps;             //Walking On Gravel-SoundBible.com-2023303198
    [SerializeField] private AudioSource audioSource;         //Unworthy_Knight_Patrol_Area

    private Transform targetPosition;
    private bool isLeft = true;
    private bool isStop = false;


    void Start()
    {
        unworthyKnight = this.transform.GetChild(0).gameObject;
        point = this.transform.GetChild(1).gameObject.transform;
        point2 = this.transform.GetChild(2).gameObject.transform;
        //audioSource.PlayOneShot(footsteps, 1.0F);  
        audioSource.clip = footsteps;
        audioSource.Play();
    }

    void Update()
    {
        if (isStop == false)
        {
            if (isLeft == true && unworthyKnight.transform.position.x <= point.position.x)
            {
                isLeft = false;
                unworthyKnight.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (isLeft == true)
            {
                unworthyKnight.transform.Translate(speed, 0, 0);
            }

            if (isLeft == false && unworthyKnight.transform.position.x >= point2.position.x)
            {
                isLeft = true;
                unworthyKnight.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (isLeft == false)
            {
                unworthyKnight.transform.Translate(-speed, 0, 0);
            }
        }
    }

    public void StopMoving()
    {
        MuteSound();
        isStop = true;
        this.GetComponent<Animator>().enabled = false;
    }

    public void StartMoving()
    {
        UnmuteSound();
        isStop = false;
        this.GetComponent<Animator>().enabled = true;
    }

    public void MuteSound()
    {
        this.gameObject.GetComponent<AudioSource>().mute = true;
    }

    public void UnmuteSound()
    {
        this.gameObject.GetComponent<AudioSource>().mute = false;
    }
}