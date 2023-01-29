using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Jason;
using Unity.VisualScripting;

public class DragSystem : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler,IDropHandler
{
    Image Room;
    RaycastHit hitLayerMask;
    Vector3 distance;
    void Start()
    {
        distance = Vector3.zero;
    }
    private void OnMouseUp()
    {
        distance = Vector3.zero;
    }
    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("Room"); /* 특정 layer 검출 */
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (distance == Vector3.zero) distance = transform.position - hitLayerMask.point;

            float y = transform.position.y; /* 높이 저장 */
            float x = hitLayerMask.point.x+distance.x;
            transform.position = new Vector3(x, y, transform.position.z);
            //this.transform.position = hitLayerMask.point + distance;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag======");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"OnDrag======");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"OnDrop======");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"OnEndDrag======");
    }
}
