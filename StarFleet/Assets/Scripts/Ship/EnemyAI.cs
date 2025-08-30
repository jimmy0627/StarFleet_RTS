using System;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyAI : MonoBehaviour
{
    private List<Vector3> TargetDes=new List<Vector3>();
    public Transform Target;
    public NavMeshAgent agent;
    public float speed;
    public float distance;
    private float FireRange;
    void Start()
    {
        //開火範圍和速度的初始設定
        FireRange = transform.GetComponent<ShipBase>().FireRange;
        speed = transform.GetComponent<ShipBase>().speed;
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = FireRange - 1; //設定和目標的距離保持
        agent.speed = speed;
        TargetDes = GetComponent<ShipBase>().TargetDes;
    }
    void FixedUpdate()
    {
        if (gameObject.transform.Find("Radar").GetComponent<EnemySenscor>().Radarimage.Count >= 1) //若雷達範圍內存在目標
        {
            Target = gameObject.transform.Find("Radar").GetComponent<EnemySenscor>().Radarimage[0];
            TargetDes = null;
            agent.SetDestination(Target.transform.position);
        }
        RotateFaceMoveDIR();
    }
    private void RotateFaceMoveDIR()
    {
        Vector2 MoveDIR = GetComponent<Rigidbody2D>().linearVelocity;
        if (MoveDIR != Vector2.zero)
        {
            float angel = (float)(Math.Atan2(MoveDIR.x, MoveDIR.y) * Mathf.Rad2Deg);
            Quaternion targetRotation = Quaternion.AngleAxis(angel, Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
        }  
    } 
}
