using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;
    private float HP;

    [SerializeField] private UnitHpBar hpBar; // 拖進來

    void Start()
    {
        HP = transform.parent.GetComponent<ShipBase>().HP;
        hpBar.SetHPBar(HP, maxHP);
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, maxHP);

        // 更新血條
        hpBar.SetHPBar(HP, maxHP);

        if (HP <= 0)
        {
            Debug.Log(gameObject.name + " 死亡！");
            // 這裡可以加死亡處理
        }
    }
}