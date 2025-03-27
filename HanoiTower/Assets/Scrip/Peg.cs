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
    }

    public bool CanPlaceDisk(GameObject disk)
    {
        if (disk == null) return false;  
        if (diskList.Count == 0) return true;

        GameObject topDisk = diskList[diskList.Count - 1];
        return disk.transform.localScale.x < topDisk.transform.localScale.x;
    }

    public void PlaceDisk(GameObject disk)
    {
        diskList.Add(disk);
        float newY = transform.position.y + (diskList.Count - 1);
        Debug.Log(newY);

        disk.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        //disk.transform.SetParent(transform);

        Debug.LogWarning(gameObject.name + " : "+ diskList.Count);
    }


    public void RemoveTopDisk()
    {
        if (diskList.Count > 0)
        {
            diskList.RemoveAt(diskList.Count - 1);
        }
    }
}
