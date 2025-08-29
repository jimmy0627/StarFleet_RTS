using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        Target = transform.parent.GetComponent<EnemyAttack>().attacking.transform;
        ProjectileSpeed = transform.parent.GetComponent<EnemyAttack>().ProjectileSpeed;
        FireRange = transform.parent.GetComponent<EnemyAttack>().FireRange;
        maxtime = FireRange / ProjectileSpeed;
        transform.parent = null;

        Vector3 TargetDIR = (Target.position - transform.position).normalized;
        float angle = Mathf.Atan2(TargetDIR.y, TargetDIR.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    void FixedUpdate()
    {
        //子彈朝目標飛行
        if (Target != null)
        {
            transform.position += transform.up * Time.deltaTime * ProjectileSpeed;
            if (Vector3.Distance(transform.position, Target.transform.position) <= 1f)  //靠近目標後自動刪除
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
