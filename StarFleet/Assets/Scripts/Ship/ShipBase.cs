using UnityEngine;
using UnityEngine.AI;

public class ShipBase : MonoBehaviour
{
    public GameObject Bullet;
    public float FireRange;
    public float RadarSize;
    public int CD;
    public float accurcy;
    public int damage;
    public float speed;
    public int ProjectileSpeed;
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
