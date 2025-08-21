using UnityEngine;

public class Indicator : MonoBehaviour
{
    private Transform WeaponRing;
    private Transform RadarRing;
    private Color tempWeaponColor;
    private Color tempRadarColor;
    void Start()
    {
        //取得環的顏色和物件
        WeaponRing = transform.Find("WeaponRange/Ring");
        RadarRing = transform.Find("RadarRange/Ring");
        tempWeaponColor = WeaponRing.GetComponent<SpriteRenderer>().color;
        tempRadarColor = RadarRing.GetComponent<SpriteRenderer>().color;

        //設定環的大小
        WeaponRing.localScale = new Vector3(transform.parent.GetComponent<ShipBase>().FireRange, transform.parent.GetComponent<ShipBase>().FireRange, 0);
        RadarRing.localScale = new Vector3(transform.parent.GetComponent<ShipBase>().RadarSize, transform.parent.GetComponent<ShipBase>().RadarSize, 0);

        //初始設定為透明
        tempWeaponColor.a = 0f;
        tempRadarColor.a = 0f;
        WeaponRing.GetComponent<SpriteRenderer>().color = tempWeaponColor;
        RadarRing.GetComponent<SpriteRenderer>().color = tempRadarColor;

    }
    public void ShowRange(bool show) //被選中時顯示環帶 沒有則不顯示
    {
        if (show)
        {
            tempWeaponColor.a = 1;
            tempRadarColor.a = 1;
        }
        else
        {
            tempWeaponColor.a = 0f;
            tempRadarColor.a = 0f;
        }
        WeaponRing.GetComponent<SpriteRenderer>().color = tempWeaponColor;
        RadarRing.GetComponent<SpriteRenderer>().color = tempRadarColor;
    }
}
