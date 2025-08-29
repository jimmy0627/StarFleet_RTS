using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Indicator : MonoBehaviour
{
    private Transform WeaponRing;
    private Transform RadarRing;
    private Color tempWeaponColor;
    private Color tempRadarColor;
    public bool show = false;
    void Start()
    {
        //取得環的顏色和物件
        WeaponRing = transform.Find("WeaponRange");
        RadarRing = transform.Find("RadarRange");
        tempWeaponColor = new Color(1,0,0);
        tempRadarColor = new Color(0.537f,0.572f,0.678f);

        //設定環的大小
        WeaponRing.localScale = new Vector3(transform.parent.GetComponent<ShipBase>().FireRange, transform.parent.GetComponent<ShipBase>().FireRange, 0);
        RadarRing.localScale = new Vector3(transform.parent.GetComponent<ShipBase>().RadarSize, transform.parent.GetComponent<ShipBase>().RadarSize, 0);

        //初始設定為透明
        tempWeaponColor.a = 0f;
        tempRadarColor.a = 0f;
        WeaponRing.GetComponent<SpriteRenderer>().color = tempWeaponColor;
        RadarRing.GetComponent<SpriteRenderer>().color = tempRadarColor;

    }

    void Update()
    {
        if (show)
        {
            if (Input.GetKey(KeyCode.C))
            {
                tempWeaponColor.a = 0.5f;
                tempRadarColor.a = 0.5f;
            }
            else
            {
                tempWeaponColor.a = 0f;
                tempRadarColor.a = 0f;
            }
        }
        WeaponRing.GetComponent<SpriteRenderer>().color = tempWeaponColor;
        RadarRing.GetComponent<SpriteRenderer>().color = tempRadarColor;
    }
}
