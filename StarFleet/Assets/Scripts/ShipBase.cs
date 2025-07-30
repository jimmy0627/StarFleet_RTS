using UnityEngine;

public class ShipBase : MonoBehaviour
{
    public float FireRange;
    public int CD;
    public float accurcy;
    public float damage;
    public float speed;
    public float RadarSize;
    public float HP;
    public bool isEnemy;
    public int Shiptype;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
