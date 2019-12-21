using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPoolObject
{
    private Rigidbody body;
    private Crystal crystal;


    void Awake () {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
	}
	


    public void OnDespawn()
    {
        SetKinematic(true);
        if (crystal)
            GameManager.Instance.DeleteCrystal(crystal);
        crystal = null;
        StopCoroutine("DeleteCube");
    }



    public void OnSpawn()
    {
        
    }



    public void SetKinematic(bool isKinematic)
    {
        body.isKinematic = isKinematic;
        body.velocity = Vector3.zero;
    }



    public void AttachCrystal(Crystal crystal)
    {
        this.crystal = crystal;
        crystal.transform.SetParent(transform);
        crystal.transform.localPosition = Vector3.zero;
    }



    public void Fall()
    {
        SetKinematic(false);
        StartCoroutine("DeleteCube");
    }



    private IEnumerator DeleteCube()
    {
        yield return new WaitForSeconds(1.0f);

        GameManager.Instance.DeleteCube(this);
    }
}
