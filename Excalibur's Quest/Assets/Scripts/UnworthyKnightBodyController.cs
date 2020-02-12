using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnworthyKnightBodyController : MonoBehaviour
{
    [SerializeField] private GameObject ExcaliburCapture;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col is CapsuleCollider2D)
            {
                Debug.Log(col.GetType());
                //Instantiate(ExcaliburCapture, this.transform.position, this.transform.rotation);
            }
        }
    }
}
