using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class FinishLineController : MonoBehaviour
{
    [SerializeField] private Transform cameraFocus;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject excaliburFinish;
    [SerializeField] private AudioSource knight;
    [SerializeField] private AudioSource knight2;
    [SerializeField] private AudioSource knight3;
    [SerializeField] private AudioClip impact;           //Drop Sword-SoundBible.com-768774345
    [SerializeField] private AudioClip jump;             //Decapitation-SoundBible.com-800292304
    [SerializeField] private AudioClip jump2;            //Swoosh 3-SoundBible.com-1573211927
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip angel;
    [SerializeField] private AudioClip done;
   
    void PlayDone()
    {
        audioSource.PlayOneShot(done, 1.0F);
    }

    void GameReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void Impact()
    {
        audioSource.PlayOneShot(impact, 1.0F);
    }

    void Jump()
    {
        audioSource.PlayOneShot(jump, 1.0F);
    }

    void Jump2()
    {
        audioSource.PlayOneShot(jump2, 1.0F);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.SetActive(false);
            knight.mute = true;
            knight2.mute = true;
            knight3.mute = true;
            Invoke("Jump", 0.10f);
            Invoke("Impact", 0.60f);
            Invoke("Jump", 0.70f);
            Invoke("Jump2", 1.25f);
            Invoke("Impact", 2.5f);
            Invoke("PlayDone", 2.4f);
            mainCam.GetComponent<AudioSource>().clip = angel;
            mainCam.GetComponent<AudioSource>().volume = 1.0f;
            mainCam.GetComponent<AudioSource>().Play();
            cam.GetComponent<CinemachineVirtualCamera>().Follow = cameraFocus.transform;
            excaliburFinish.SetActive(true);            
            Invoke("GameReset", 153.6f);
        }
    }
}
