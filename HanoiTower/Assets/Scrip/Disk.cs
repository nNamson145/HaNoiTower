using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disk : MonoBehaviour
{
    public Vector3 originalPos;

    public Vector3 mouseOffset;

    public Peg originalPeg;

    public Peg newPeg;


    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseDown()
    {
        if(originalPeg != null)
        {
            originalPeg = transform.parent.GetComponent<Peg>();

            if (originalPeg != null && originalPeg.diskList.Count > 0 && originalPeg.diskList[originalPeg.diskList.Count - 1] == this.gameObject)
            {
                originalPos = transform.position;
                mouseOffset = transform.localPosition - GetMouseWorldPos();
            }
        }
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() - mouseOffset;
        transform.position = newPosition;
    }

    void OnMouseUp()
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
                    Debug.Log("mmmmmmmmmm");
                }

                newPeg.PlaceDisk(gameObject);
                transform.SetParent(newPeg.transform);
                return;
            }
        }
        transform.position = originalPos;
    }
}
