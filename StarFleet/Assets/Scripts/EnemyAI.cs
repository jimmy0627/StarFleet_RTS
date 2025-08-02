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
        FireRange = transform.GetComponent<ShipBase>().FireRange;
        speed = transform.GetComponent<ShipBase>().speed;
    }
    void FixedUpdate()
    {
        if (gameObject.transform.Find("Radar").GetComponent<Senscor>().Radarimage.Count >= 1)
        {
            Target = gameObject.transform.Find("Radar").GetComponent<Senscor>().Radarimage[0];
            distance = Vector2.Distance(transform.position, Target.position);
            if (distance >= FireRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, Target.position, speed * Time.deltaTime);
            }
            else if (distance >= FireRange - 2)
            {
                transform.position = transform.position;
            }
            else
            {
                transform.Translate((transform.position - Target.position) *Time.deltaTime);
            }
            
        }

    }
}
