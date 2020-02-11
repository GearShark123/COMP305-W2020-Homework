using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnworthyKnightController : MonoBehaviour
{
    [SerializeField] private float speed = -0.02f;
    [SerializeField] private GameObject unworthyKnight;
    [SerializeField] private Transform point;
    [SerializeField] private Transform point2;

    private Transform targetPosition;
    private bool isLeft = true;


    void Start()
    {
        unworthyKnight = this.transform.GetChild(0).gameObject;
        point = this.transform.GetChild(1).gameObject.transform;
        point2 = this.transform.GetChild(2).gameObject.transform;
    }

    void Update()
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
