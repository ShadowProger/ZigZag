using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody body;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }



    public void SetStartPos()
    {
        StopCoroutine("DeletePlatform");
        gameObject.SetActive(true);
        body.isKinematic = true;
        body.MovePosition(Vector3.zero);
        body.velocity = Vector3.zero;
    }



    public void Fall()
    {
        body.velocity = Vector3.zero;
        body.isKinematic = false;
        StartCoroutine("DeletePlatform");
    }



    private IEnumerator DeletePlatform()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);
    }
}
