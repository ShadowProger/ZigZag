using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            Cube cube = other.gameObject.GetComponent<Cube>();

            cube.Fall();
        }

        if (other.CompareTag("Platform"))
        {
            Platform platform = other.gameObject.GetComponent<Platform>();

            platform.Fall();
        }
    }
}
