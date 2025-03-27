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

    private void Start()
    {
        if (transform.parent != null)
        {
            originalPeg = transform.parent.GetComponent<Peg>();
        }

        if (originalPeg == null)
        {
            Debug.LogError($"[Disk {gameObject.name}] Không tìm thấy Peg cha!");
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseDown()
    {
        if (transform.parent != null)
        {
            originalPeg = transform.parent.GetComponent<Peg>();
        }

        if (originalPeg == null)
        {
            Debug.LogError($"[Disk {gameObject.name}] Không tìm thấy Peg cha khi nhấn!");
            return;
        }

        // Kiểm tra nếu đĩa này là đĩa trên cùng của cọc
        if (originalPeg.diskList.Count > 0 && originalPeg.diskList[originalPeg.diskList.Count - 1] == gameObject)
        {
            originalPos = transform.position;
            mouseOffset = transform.localPosition - GetMouseWorldPos();
        }
        else
        {
            Debug.LogError($"[Disk {gameObject.name}] Không phải đĩa trên cùng của {originalPeg.name}!");
            return ;
        }
    }

    private void OnMouseDrag()
    {
            Vector3 newPosition = GetMouseWorldPos() - mouseOffset;
            transform.position = newPosition;
    }

    void OnMouseUp()
    {
        if (originalPeg == null) return;


        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in colliders)
        {
            Peg newPeg = col.GetComponent<Peg>();

            if (newPeg != null)
            {
                Debug.Log($"[Disk {gameObject.name}] Chạm vào trụ: {newPeg.gameObject.name}");
            }

            if (newPeg != null && newPeg.CanPlaceDisk(gameObject))
            {
                Debug.Log($"[Disk {gameObject.name}] Di chuyển từ {originalPeg.name} đến {newPeg.name}");

                if (originalPeg.diskList.Count > 0)
                {
                    originalPeg.RemoveTopDisk();
                }
                newPeg.PlaceDisk(gameObject);
                transform.SetParent(newPeg.transform);
                return;
            }
        }

        // Nếu không thả vào trụ hợp lệ, trả về vị trí cũ
        transform.position = originalPos;
    }
}
