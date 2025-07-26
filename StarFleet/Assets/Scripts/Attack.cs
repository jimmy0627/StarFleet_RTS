using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    //和Sensor.cs一樣概念，但是變成了攻擊
    //可調整變數如下:CD=冷卻時間，accurcy=準確度，damage=傷害
    public List<GameObject> Targetlist = new List<GameObject>();
    private GameObject attacking;
    private CircleCollider2D FireCircle;
    private Coroutine attackRoutine;
    private float FireRange;
    private int CD;
    private float accurcy;
    private float damage;

    //設置攻擊範圍
    void Start()
    {
        FireRange = transform.parent.GetComponent<ShipBase>().FireRange;
        CD = transform.parent.GetComponent<ShipBase>().CD;
        accurcy = transform.parent.GetComponent<ShipBase>().accurcy;
        damage = transform.parent.GetComponent<ShipBase>().damage;


        FireCircle = gameObject.GetComponent<CircleCollider2D>();
        FireCircle.radius = FireRange;
    }

    //進入攻擊範圍，加入目標名單
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")!=transform.parent.GetComponent<ShipBase>().isEnemy)
        {
            Targetlist.Add(collision.transform.gameObject);
        }
    }

    //取得優先級最高的進入攻擊循環
    void OnTriggerStay2D(Collider2D collision)
    {
        if (attacking == null)
        {
            attacking = MaxByShipType(Targetlist);
            attackRoutine = StartCoroutine(AttackTarget());
        }
    }

    //目標離開攻擊範圍，移出目標清單
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == attacking)
        {
            attacking = null;
            StopCoroutine(attackRoutine);
        }
        Targetlist.Remove(collision.gameObject);
    }

    //攻擊循環，判斷是否命中後扣血，並在每次攻擊循環中重新尋找優先級最高的目標
    private IEnumerator AttackTarget()
    {
        while (attacking != null)
        {
            var hull = attacking.GetComponent<Health>().HP;
            var aim = Random.Range(0, 100);
            if (aim <= accurcy)
            {
                attacking.GetComponent<Health>().HP -= damage;
            }
            Debug.Log("attacking:" + attacking.transform.parent.name + "  hull=" + hull);
            attacking = MaxByShipType(Targetlist);
            yield return new WaitForSeconds(CD);
        }
    }

    //尋找優先級最高的目標
    private GameObject MaxByShipType(List<GameObject> Targetlist)
    {
        int maxshiptype = int.MinValue;
        GameObject HVT = null;
        foreach (var target in Targetlist)
        {
            if (target.GetComponent<Health>().Shiptype >= maxshiptype)
            {
                maxshiptype = target.GetComponent<Health>().Shiptype;
                HVT = target;
            }
        }
        return HVT;
    }
    
    //尋找優先級最低的目標
    private GameObject MinByShipType(List<GameObject> Targetlist)
    {
        int minshiptype = int.MaxValue;
        GameObject HVT = null;
        foreach (var target in Targetlist)
        {
            if (target.GetComponent<Health>().Shiptype >= minshiptype)
            {
                minshiptype = target.GetComponent<Health>().Shiptype;
                HVT = target;
            }
        }
        return HVT;
    }
}