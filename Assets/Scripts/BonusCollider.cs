using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCollider : MonoBehaviour
{
    public Bonus bonus;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            bonus.OnTaken();
        }
    }
}
