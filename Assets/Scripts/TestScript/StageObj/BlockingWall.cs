using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingWall : MonoBehaviour
{
    public float speed = 5f;
    

    void Update()
    {
        AutoMove();
    }

    void AutoMove()
    {
        Vector3 moveDirection = new Vector3(1f, 0f, 0f);

        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.x > 15f)
        {
            transform.position = new Vector3(-15f, transform.position.y, transform.position.z);
        }
    }


}
