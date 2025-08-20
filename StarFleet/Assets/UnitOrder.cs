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
        if (Unitinlist.Any() && Input.GetMouseButtonDown(1))
        {
            TargetDes.Add(mousepos);
        }
        foreach (var item in Unitinlist)
        {
            item.transform.GetComponent<ShipBase>().TargetDes = TargetDes;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            TargetDes = new List<Vector3>();
        }
    }
}
