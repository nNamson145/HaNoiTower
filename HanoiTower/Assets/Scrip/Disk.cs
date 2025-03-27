using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disk : MonoBehaviour
{
    public Vector3 originalPos;
    public Vector3 mouseOffset;
    public Peg originalPeg;

    private void Start()
    {
        originalPos = transform.position;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseDown()
    {
        originalPeg = transform.parent.GetComponent<Peg>();

        if (originalPeg != null && originalPeg.diskList.Count > 0 && originalPeg.diskList[originalPeg.diskList.Count - 1] == this.gameObject)
        {
            originalPos = transform.position;
            mouseOffset = transform.localPosition - GetMouseWorldPos();
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        if(originalPeg.diskList[originalPeg.diskList.Count - 1] == this.gameObject)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (Collider col in colliders)
            {
                Peg newPeg = col.GetComponent<Peg>();

                if (newPeg != null && newPeg.CanPlaceDisk(gameObject))
                {
                    if (originalPeg != null)
                    {
                        originalPeg.RemoveTopDisk();
                    }
                    Debug.LogError("loi me roi");
                    newPeg.PlaceDisk(gameObject);
                    transform.SetParent(newPeg.transform);
                    return;
                }
            }
        }
        transform.position = originalPos;
    }
}
