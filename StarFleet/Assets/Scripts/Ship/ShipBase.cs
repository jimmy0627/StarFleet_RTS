using UnityEngine;

public class ShipBase : MonoBehaviour
{
    public float FireRange;
    public float RadarSize;
    public int CD;
    public float accurcy;
    public int damage;
    public float speed;
    public int HP;
    public bool isEnemy;
    public int Shiptype;
    public int MaxHealth;
    void Start()
    {
        MaxHealth = HP;
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
