using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    //和Sensor.cs一樣概念，但是變成了攻擊
    //可調整變數如下:CD=冷卻時間，accurcy=準確度，damage=傷害
    public List<GameObject> Targetlist = new List<GameObject>();
    public GameObject attacking;
    private Coroutine attackRoutine;
    public float FireRange;
    private int CD;
    private float accurcy;
    private int damage;
    public int ProjectileSpeed;

    //設置攻擊範圍
    void Start()
    {
        FireRange = transform.parent.GetComponent<ShipBase>().FireRange;
        CD = transform.parent.GetComponent<ShipBase>().CD;
        accurcy = transform.parent.GetComponent<ShipBase>().accurcy;
        damage = transform.parent.GetComponent<ShipBase>().damage;
        ProjectileSpeed = transform.parent.GetComponent<ShipBase>().ProjectileSpeed;
    }

    void Update()
    {
        foreach (var target in transform.parent.Find("Radar").GetComponent<Senscor>().Radarimage)
        {
            if (target == null) continue;

            var DIS = Vector2.Distance(transform.position, target.position);
            if (DIS < FireRange && !Targetlist.Contains(target.gameObject))
            {
                Targetlist.Add(target.gameObject);

            }
            if (DIS > FireRange && Targetlist.Contains(target.gameObject))
            {
                Targetlist.Remove(target.gameObject);
            }
        }
        Targetlist.RemoveAll(t => t == null);
        if (attacking == null && attackRoutine == null)
        {
            attacking = MaxByShipType(Targetlist);
            attackRoutine = StartCoroutine(AttackTarget());
        }

    }

    //攻擊循環，判斷是否命中後扣血，並在每次攻擊循環中重新尋找優先級最高的目標
    private IEnumerator AttackTarget()
    {
        while (attacking != null)
        {
            Instantiate(transform.parent.GetComponent<ShipBase>().Bullet, transform.position, Quaternion.identity, transform); //生成子彈
            var aim = Random.Range(0, 100);
            if (aim <= accurcy)
            {
                attacking.GetComponent<ShipBase>().HP -= damage;
            }
            Debug.Log("attacking:" + attacking.name + "  hull=" + attacking.GetComponent<ShipBase>().HP + " attacked by:"+transform.parent.name);
            attacking = MaxByShipType(Targetlist);
            yield return new WaitForSeconds(CD);
        }
        attackRoutine = null;
    }

    //尋找優先級最高的目標
    private GameObject MaxByShipType(List<GameObject> Targetlist)
    {
        int maxshiptype = int.MinValue;
        GameObject HVT = null;
        foreach (var target in Targetlist)
        {
            if (target.GetComponent<ShipBase>().Shiptype>= maxshiptype)
            {
                maxshiptype = target.GetComponent<ShipBase>().Shiptype;
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
            if (target.GetComponent<ShipBase>().Shiptype >= minshiptype)
            {
                minshiptype = target.GetComponent<ShipBase>().Shiptype;
                HVT = target;
            }
        }
        return HVT;
    }
}

