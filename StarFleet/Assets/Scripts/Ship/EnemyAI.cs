using System;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyAI : MonoBehaviour
{
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
        
        agent.stoppingDistance = FireRange; //設定和目標的距離保持
    }
    void FixedUpdate()
    {
        if (gameObject.transform.Find("Radar").GetComponent<Senscor>().Radarimage.Count >= 1) //若雷達範圍內存在目標
        {
            Target = gameObject.transform.Find("Radar").GetComponent<Senscor>().Radarimage[0];
            agent.SetDestination(Target.transform.position); //朝目標移動
        }
        RotateFaceMoveDIR();
    }
    private void RotateFaceMoveDIR()
    {
        Vector2 MoveDIR = GetComponent<Rigidbody2D>().linearVelocity;
        if (MoveDIR != Vector2.zero)
        {
            float angel = (float)(Math.Atan2(MoveDIR.x, MoveDIR.y) * Mathf.Rad2Deg);
            Quaternion targetRotation = Quaternion.AngleAxis(angel, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 80 * Time.deltaTime);
        }
       
    } 
}
