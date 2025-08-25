using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySenscor : MonoBehaviour
{
    //利用CircleCollider的覆蓋範圍當作雷達範圍 在利用contacs印出範圍內的物件
    public List<Transform> Radarimage = new List<Transform>();
    public float Radarsize;
    private CircleCollider2D Radar;
    void Start()
    {
        Radarsize = transform.parent.GetComponent<ShipBase>().RadarSize;
        Radar = gameObject.GetComponent<CircleCollider2D>();
        Radar.radius = Radarsize; //設置雷達範圍
    }
    void Update()
    {
        if (Radarimage.Any())
        {
            foreach (var item in Radarimage)
            {
                if (item.gameObject.GetComponent<ShipBase>().HP==0)
                {
                    Radarimage.Remove(item);
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<ShipBase>() != null)
        {
            if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
            {
                SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
                Radarimage.Add(collision.transform);  //進入碰撞圈，加入雷達範圍
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<ShipBase>() != null)
        {
            if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
            {
                SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
        {
            SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
            Radarimage.Remove(collision.transform);
        }

    }
}
