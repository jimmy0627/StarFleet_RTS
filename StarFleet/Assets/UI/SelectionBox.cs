using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SelectionBox : MonoBehaviour
{
    [SerializeField] RectTransform selectionBoxUI; // UI 選取框 (需要 Image)
    [SerializeField] Camera mainCamera;

    private bool isSelecting = false;
    private bool clickStartOnUI = false;
    private Vector2 startPos, endPos;
    private List<GameObject> selectedUnits = new List<GameObject>();

    void Update()
    {
        HandleUnitSelection();
    }

    void HandleUnitSelection()
    {
        // 開始框選
        if (Input.GetMouseButtonDown(0))
        {
            clickStartOnUI = EventSystem.current.IsPointerOverGameObject();
            if (clickStartOnUI) return;

            UnSelectUnits();
            startPos = Input.mousePosition;
            selectionBoxUI.gameObject.SetActive(true);
            isSelecting = true;
        }

        // 更新框選
        if (isSelecting)
        {
            endPos = Input.mousePosition;
            UpdateSelectionBox();
        }

        // 結束框選
        if (Input.GetMouseButtonUp(0))
        {
            if (clickStartOnUI)
            {
                clickStartOnUI = false;
                return;
            }

            isSelecting = false;
            selectionBoxUI.gameObject.SetActive(false);
            SelectUnits();
            clickStartOnUI = false;
        }
    }

    // 更新 UI 框
    void UpdateSelectionBox()
    {
        Vector2 boxStart = startPos;
        Vector2 boxEnd = endPos;

        float width = Mathf.Abs(boxEnd.x - boxStart.x);
        float height = Mathf.Abs(boxEnd.y - boxStart.y);
        Vector2 center = (boxStart + boxEnd) / 2;

        // 如果 Canvas 是 Overlay 模式可直接這樣設定
        selectionBoxUI.position = center;
        selectionBoxUI.sizeDelta = new Vector2(width, height);
    }

    // 框選單位
    void SelectUnits()
    {
        UnSelectUnits();

        Vector2 worldStart = mainCamera.ScreenToWorldPoint(startPos);
        Vector2 worldEnd = mainCamera.ScreenToWorldPoint(endPos);

        // 找到選取框內的所有物件
        Collider2D[] selectedObjects = Physics2D.OverlapAreaAll(worldStart, worldEnd);

        foreach (var obj in selectedObjects)
        {
            // 只選取標記為 "FriendlyUnit" 的物件
            if (!obj.CompareTag("FriendlyUnit"))
                continue;

            // 如果是 Trigger，確保物件完全在框選內
            if (obj.isTrigger && !IsObjectInSelectionBox(obj))
                continue;

            // 確認有 ShipBase 元件
            if (!obj.TryGetComponent(out ShipBase shipBase))
                continue;

            // 避免重複加入
            if (!selectedUnits.Contains(obj.gameObject))
            {
                selectedUnits.Add(obj.gameObject);
            }

            Debug.Log("選取到：" + obj.gameObject.name);

            // 讓 ShipBase 自己處理外觀變化（選取高亮）
            shipBase.Select();
        }
    }

    // 取消選取
    void UnSelectUnits()
    {
        foreach (var obj in selectedUnits)
        {
            if (obj && obj.TryGetComponent(out ShipBase shipBase))
            {
                shipBase.Deselect();
            }
        }
        selectedUnits.Clear();
    }
    bool IsObjectInSelectionBox(Collider2D obj)
    {
        if (obj == null) return false;

        Vector2 worldStart = mainCamera.ScreenToWorldPoint(startPos);
        Vector2 worldEnd = mainCamera.ScreenToWorldPoint(endPos);

        // 取得選取範圍的最小和最大座標
        float minX = Mathf.Min(worldStart.x, worldEnd.x);
        float maxX = Mathf.Max(worldStart.x, worldEnd.x);
        float minY = Mathf.Min(worldStart.y, worldEnd.y);
        float maxY = Mathf.Max(worldStart.y, worldEnd.y);

        // 檢查物體的邊界是否完全在選取框內
        return obj.bounds.min.x >= minX && obj.bounds.max.x <= maxX && obj.bounds.min.y >= minY && obj.bounds.max.y <= maxY;
    }
}