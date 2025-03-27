using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Peg : MonoBehaviour
{
    public List<GameObject> diskList;

    private void Start()
    {
        diskList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Disk"))
            {
                diskList.Add(child.gameObject);
            }
        }

        Debug.LogWarning($"[Peg {gameObject.name}] NumStart : {diskList.Count}");
    }

    public bool CanPlaceDisk(GameObject disk)
    {
        if (diskList.Count == 0) return true;

        return disk.transform.localScale.x < diskList[diskList.Count - 1].transform.localScale.x;
    }

    public void PlaceDisk(GameObject disk)
    {
        diskList.Add(disk);
        float diskHeight = disk.transform.localScale.y;
        float newY = transform.position.y + (diskList.Count - 1) * diskHeight*2;

        disk.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        //disk.transform.SetParent(transform);

        Debug.Log($"[Peg {gameObject.name}] Đĩa đặt vào: {disk.name}. Tổng đĩa: {diskList.Count}");

    }

    public void RemoveTopDisk()
    {
        if (diskList.Count > 0)
        {
            diskList.RemoveAt(diskList.Count - 1);
        }
    }
}
