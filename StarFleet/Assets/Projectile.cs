using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Target;
    private int ProjectileSpeed;
    private float FireRange;
    private float current = 0;
    private float maxtime;
    private Vector3 DIR;

    //設定參數
    void Awake()
    {
        Target = transform.parent.GetComponent<Attack>().attacking.transform;
        ProjectileSpeed = transform.parent.GetComponent<Attack>().ProjectileSpeed;
        FireRange = transform.parent.GetComponent<Attack>().FireRange;
        maxtime = FireRange / ProjectileSpeed;

        Vector3 TargetDIR = Target.position - transform.position;
        DIR = Vector3.RotateTowards(transform.up, TargetDIR, 2, 0.0f).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, DIR);
    }
    void FixedUpdate()
    {
        //子彈朝目標飛行
        if (Target != null)
        {
            transform.Translate(Vector3.up * Time.deltaTime * ProjectileSpeed);
            if (Vector3.Distance(transform.position, Target.transform.position) <= 0.5f)  //近炸引信
            {
                Destroy(gameObject);
            }
        }
        current += Time.deltaTime;
        if (current > +maxtime)
        {
            Destroy(gameObject);  //超過理論最大飛行時長後自毀
        }
        Debug.DrawRay(transform.position, DIR, Color.red);
    }
}