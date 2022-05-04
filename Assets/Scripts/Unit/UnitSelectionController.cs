using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionController : MonoBehaviour
{
    public static UnitSelectionController Singleton;

    public List<UnitMover> selectedUnits = new List<UnitMover>();
    public List<UnitMover> myAllUnits = new List<UnitMover>();

    public RectTransform selectionArea;
    Vector2 mouseStartPos;
    Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask targetLayerMask;
    void Awake()
    {
        Singleton = this;
        mainCamera = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            StartSelectionArea();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ClearSelection();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdateSelectionArea();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GiveCommandToUnits();
        }
    }
    void StartSelectionArea()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            foreach (UnitMover item in selectedUnits)
            {
                item.Deselect();
            }
        selectedUnits.Clear();
        }
        
        selectionArea.gameObject.SetActive(true);
        mouseStartPos = Input.mousePosition;
        UpdateSelectionArea();
    }
    void UpdateSelectionArea()
    {
        Vector2 mousePos = Input.mousePosition;

        float w = mousePos.x - mouseStartPos.x;
        float h = mousePos.y - mouseStartPos.y;

        selectionArea.sizeDelta = new Vector2(Mathf.Abs(w),Mathf.Abs(h));
        selectionArea.anchoredPosition = mouseStartPos + new Vector2(w/2,h/2);


    }
    void ClearSelection()
    {
        selectionArea.gameObject.SetActive(false);
        if (selectionArea.sizeDelta.magnitude == 0)
        {
                 Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,layerMask)) return;
    
            if(!hit.collider.TryGetComponent<UnitMover>(out UnitMover unit)) return;
            
            selectedUnits.Add(unit);
            foreach (UnitMover item in selectedUnits)
            {
                item.Select();
            }
            return;
        }
        Vector2 min = selectionArea.anchoredPosition - (selectionArea.sizeDelta/2);
        Vector2 max = selectionArea.anchoredPosition + (selectionArea.sizeDelta/2);

        foreach (UnitMover item in myAllUnits)
        {
            if(selectedUnits.Contains(item)) continue;

            Vector3 screenPos = mainCamera.WorldToScreenPoint(item.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y < max.y && screenPos.y >min.y)
            {
                selectedUnits.Add(item);
                item.Select();
            }
        }

    }
    void GiveCommandToUnits()
    {
         Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,targetLayerMask)) return;
        if(selectedUnits.Count == 0) return;

        foreach (UnitMover item in selectedUnits)
        {
            item.target = hit.transform; // target daha sonra dusman ya da bina gibi objeler olacak
            item.MoveToPoint(hit.point);
        }
    }
}
