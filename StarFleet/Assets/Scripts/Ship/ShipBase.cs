using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    public GameObject Bullet;
    [SerializeField] private UnitHpBar HpBar;
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
        sr = transform.Find("Hull").GetComponent<SpriteRenderer>();
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
        HpBar.GetComponent<UnitHpBar>().SetHPBar(HP, MaxHealth);
        if (isEnemy)
        {
            SpriteRenderer srr = GetComponent<SpriteRenderer>();
            if (srr != null)
            {
                Color c = srr.color;
                srr.color = new Color(c.r, c.g, c.b, 0f); // 半透明
            }
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, MaxHealth);

        if (HpBar != null)
            HpBar.SetHPBar(HP, MaxHealth);
        if (HP <= 0)
        {
            Debug.Log(name + " Destroyed!");
            Destroy(gameObject);
        }
    }
}