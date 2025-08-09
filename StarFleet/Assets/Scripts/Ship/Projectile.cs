using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform Target;
    private Vector2 launchpos;
    private int ProjectileSpeed;
    private float FireRange;
    private float current = 0;
    private float maxtime;

    //設定參數
    void Awake() 
    {
        Target = transform.parent.GetComponent<Attack>().attacking.transform;
        ProjectileSpeed = transform.parent.GetComponent<Attack>().ProjectileSpeed;
        launchpos = transform.position;
        FireRange = transform.parent.GetComponent<Attack>().FireRange;
        maxtime = FireRange / ProjectileSpeed;
    }
    void FixedUpdate()
    {
        //子彈朝目標飛行
        if (Target != null)
        {
            transform.Translate((Target.position - transform.position) * Time.deltaTime * ProjectileSpeed);
            if (Vector2.Distance(Target.position, transform.position) <= 1 || Vector2.Distance(launchpos, transform.position) > FireRange)
            {
                Destroy(gameObject);
            }
        }
        //超過理論最大飛行時長後自毀
        current += Time.deltaTime;
        if (current > +maxtime)
        {
            Destroy(gameObject);
        }
    }
}