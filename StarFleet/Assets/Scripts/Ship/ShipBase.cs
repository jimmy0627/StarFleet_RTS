using UnityEngine;

public class ShipBase : MonoBehaviour
{
    public GameObject Bullet;
    public float FireRange;
    public float RadarSize;
    public float FarRadarSize;
    public int CD;
    public float accurcy;
    public int damage;
    public float speed;
    public int ProjectileSpeed;
    public int HP;
    public bool isEnemy;
    public int Shiptype;
    public int MaxHealth;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        sr.color = Color.green; // 高亮顯示
    }

    public void Deselect()
    {
        sr.color = Color.white; // 恢復原狀
    }
    void Start()
    {
        MaxHealth = HP;
        if (isEnemy)
        {
            SpriteRenderer srr =  GetComponent<SpriteRenderer>();
            if (srr != null)
            {
                Color c = srr.color;
                srr.color = new Color(c.r, c.g, c.b, 0f); // 半透明
            }
        }
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}