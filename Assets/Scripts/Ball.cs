using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody body;
    private float speed = 5;


	void Awake () {
        body = GetComponent<Rigidbody>();
	}



    public void SetStartPos()
    {
        body.MovePosition(new Vector3(0f, 1.2f, 0f));
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }



    public void MoveForward()
    {
        Vector3 newVelocity = Vector3.forward * speed;
        newVelocity.y = body.velocity.y;
        body.velocity = newVelocity;
    }



    public void MoveRight()
    {
        Vector3 newVelocity = Vector3.right * speed;
        newVelocity.y = body.velocity.y;
        body.velocity = newVelocity;
    }



    public Vector3 GetPosition()
    {
        return body.position;
    }
}
