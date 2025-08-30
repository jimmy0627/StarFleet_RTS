using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShipBase : MonoBehaviour
{
    public List<Vector3> TargetDes = new List<Vector3>();
    private GameObject InfoPanle;
    [SerializeField] private UnitHpBar HpBar;
    private SpriteRenderer sr;
    public NavMeshAgent agent;
    public LineRenderer lr;
    public GameObject Bullet;
    public float FireRange;
    public int damage;
    public float accurcy;
    public int CD;
    public int ProjectileSpeed;
    public float RadarSize;
    public int HP;
    public int MaxHealth;
    public int ECM;
    public float speed;
    public float Rotaespeed;
    public int Shiptype;
    public bool isEnemy;
    public bool Showinfo=false;
    void Awake()
    {
        sr = transform.Find("Hull").GetComponent<SpriteRenderer>();
        if (isEnemy)
        {
            transform.Find("Hull").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            transform.Find("Canvas").gameObject.SetActive(false);
        }
    }

    public void Select()
    {
        sr.color = new Color(0.3323692f, 0.7264151f, 0.7151858f); // 高亮顯示
        agent.speed = speed;
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
            SpriteRenderer srr = GetComponent<SpriteRenderer>();
            if (srr != null)
            {
                Color c = srr.color;
                srr.color = new Color(c.r, c.g, c.b, 0f); // 半透明
            }
        }
        agent.updateRotation = false; // 禁用 NavMeshAgent 自動旋轉
        agent.angularSpeed = 0f;      // 確保不會干擾手動旋轉
    }
    void Update()
    {
        //判定何時被擊毀
        if (HP <= 0)
        {
            Destroy(gameObject);
            return;
        }
        if (TargetDes.Any())
        {
            int count = TargetDes.Count() + 1;
            Vector3 target = TargetDes[0];
            float distance = Vector3.Distance(transform.position, target);
            if (!isEnemy)
            {
                lr.positionCount = count;
                lr.SetPosition(0, transform.position);
                for (int i = 1; i < count; i++)
                {
                    lr.SetPosition(i, TargetDes[i - 1]);
                }
            }
            
            //設置轉向下一個路徑點  
            bool isFacingTarget = RotateToward(target);
            if (isFacingTarget && distance > 1f)
            {
                agent.SetDestination(target); // 只有轉向完成才移動
            }
            else if (distance <= 1f)
            {
                TargetDes.RemoveAt(0);
                if(!isEnemy) lr.positionCount = 0;
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
            Destroy(transform.gameObject);
            Debug.Log(name + " Destroyed!");
            Destroy(gameObject);
        }
    }
    //連續旋轉
    private bool RotateToward(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Rotaespeed * Time.deltaTime);

        // 判斷是否已接近目標角度（小於 1 度視為完成轉向）
        return Quaternion.Angle(transform.rotation, targetRotation) < 1f;
    }
}