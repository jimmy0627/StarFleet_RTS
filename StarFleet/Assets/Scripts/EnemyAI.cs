using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAI : MonoBehaviour
{
    public Transform Target;
    public float speed;
    public float distance;
    private float FireRange;
    void Start()
    {
        //開火範圍和速度的初始設定
        FireRange = transform.GetComponent<ShipBase>().FireRange;
        speed = transform.GetComponent<ShipBase>().speed;
    }
    void FixedUpdate()
    {

        if (gameObject.transform.Find("Radar").GetComponent<Senscor>().Radarimage.Count >= 1) //若雷達範圍內存在目標
        {
            Target = gameObject.transform.Find("Radar").GetComponent<Senscor>().Radarimage[0];
            distance = Vector2.Distance(transform.position, Target.position);
            if (transform.GetComponent<ShipBase>().HP > transform.GetComponent<ShipBase>().MaxHealth / 2 ||true) //和目標的交互過程
            {
                if (distance >= FireRange)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, Target.position, speed * Time.deltaTime);
                }
                else if (distance >= FireRange*0.7f)
                {
                    transform.position = transform.position;
                }
                else
                {
                    transform.Translate((transform.position - Target.position) * Time.deltaTime*speed);
                }
            }
            //else //血量過低時 逃跑
            //{
            //    transform.Translate((transform.position - Target.position) * Time.deltaTime*speed*0.2f);
            //}

        }

    }
}
