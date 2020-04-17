using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPositions : MonoBehaviour
{
    [SerializeField] private List<Transform> SpawnPositions;

    private void Awake()
    {
        SpawnPositions = new List<Transform>();
        PopulateList(SpawnPositions);
    }

    void PopulateList(List<Transform> childTrans)
    {
        Transform[] ch;
        ch = gameObject.GetComponentsInChildren<Transform>();

        for (int i = 1; i < ch.Length; i++) //i is = 1 because GetComponentsInChildren returns the compenent in the parent as index 0
        {
            childTrans.Add(ch[i]);
        }
    }

    public List<Transform> GetAllSpawnPositions()
    {
        return SpawnPositions;
    }
}
