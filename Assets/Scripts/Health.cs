using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP;
    public int Shiptype;
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
