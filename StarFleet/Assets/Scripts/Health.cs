using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP;
    public int Shiptype;
    void Start()
    {
        HP = transform.parent.GetComponent<ShipBase>().HP;
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
