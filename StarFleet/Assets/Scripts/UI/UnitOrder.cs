using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class UnitOrder : MonoBehaviour
{
    public List<GameObject> Unitinlist = new List<GameObject>();
    public List<Vector3> TargetDes = new List<Vector3>();

    void Update()
    {
        Unitinlist = transform.GetComponent<SelectionBox>().selectedUnits;
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;

        if (Unitinlist.Any() && Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            TargetDes.Add(mousepos);

            foreach (var item in Unitinlist)
            {
                // 建立副本
                List<Vector3> copiedPath = new List<Vector3>(TargetDes);
                item.transform.GetComponent<ShipBase>().TargetDes = copiedPath;
            }
        }
        else if (Unitinlist.Any() && Input.GetMouseButtonDown(1))
        {
            TargetDes.Add(mousepos);

            foreach (var item in Unitinlist)
            {
                List<Vector3> copiedPath = new List<Vector3>(TargetDes);
                item.transform.GetComponent<ShipBase>().TargetDes = copiedPath;
            }

            TargetDes = new List<Vector3>(); // 清空原始路徑
        }
    }
}
