using UnityEngine;
using UnityEngine.UI;

public class UnitHpBar : MonoBehaviour
{
    [SerializeField] private Color StartColor = Color.green;
    [SerializeField] private Color EndColor = Color.red;
    [SerializeField] private Image HPImage;
    public float HP;
    public float MaxHealth;
    void Start()
    {
        HP = transform.parent.GetComponent<ShipBase>().HP;
        MaxHealth = transform.parent.GetComponent<ShipBase>().MaxHealth;
    }

    public void SetHPBar(float HP, float MaxHealth)
    {
        if (HPImage == null) return;

        float ratio = Mathf.Clamp01(HP / MaxHealth);
        HPImage.fillAmount = ratio;
        HPImage.color = Color.Lerp(EndColor, StartColor, ratio);
    }
    void Update()
{
    if (transform.parent == null) return;
    ShipBase ship = transform.parent.GetComponent<ShipBase>();
    if (ship == null) return;

    SetHPBar(ship.HP, ship.MaxHealth);
}
}