using System.Collections.Generic;
using System.Linq;
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
    public List<Vector3> TargetDes = new List<Vector3>();
    [SerializeField] private UnitHpBar HpBar;
    private SpriteRenderer sr;
    public NavMeshAgent agent;

    void Awake()
    {
        sr = transform.Find("Hull").GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        sr.color = new Color(0.3323692f, 0.7264151f, 0.7151858f); // 高亮顯示
    }

    public void Deselect()
    {
        sr.color = Color.white; // 恢復原狀
    }
    void Start()
    {
        HpBar.GetComponent<UnitHpBar>().SetHPBar(HP, MaxHealth);
        MaxHealth = HP;
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
    void Update()
    {
        if (TargetDes.Any())
        {
            agent.SetDestination(TargetDes[0]);
            if (Vector3.Distance(transform.position, TargetDes[0]) < 0.5)
            {
                TargetDes.RemoveAt(0);
            }
        }
        if (HP <= 0)
        {
            Destroy(transform.gameObject);
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
            Destroy(transform.gameObject);
            Debug.Log(name + " Destroyed!");
            Destroy(gameObject);
        }
    }
}