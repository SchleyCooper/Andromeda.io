using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public string       name;
    public int          initialAmount;
    public GameObject   objectToPool;
    public bool         shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler Instance;

    [SerializeField]
    private GameObject poolContainer;

    public List<ObjectPoolItem> itemsToPool;

    private List<List<GameObject>> pooledLists;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledLists = new List<List<GameObject>>();

        for (int i = 0; i < itemsToPool.Count; i++)
        {   // NEED PHOTON VIEW TO SPAWN WITH PhotonNetwork.Instantiate...?
            //GameObject container = PhotonNetwork.Instantiate(poolContainer.name, Vector3.zero, Quaternion.identity);
            GameObject container = Instantiate(poolContainer, Vector3.zero, Quaternion.identity, transform);
            container.name = itemsToPool[i].name;

            pooledLists.Add(new List<GameObject>());

            for (int j = 0; j < itemsToPool[i].initialAmount; j++)
            {
                GameObject newObj = PhotonNetwork.Instantiate(itemsToPool[i].objectToPool.name, Vector3.zero, Quaternion.identity);
                newObj.transform.SetParent(container.transform, false);
                newObj.SetActive(false);
                pooledLists[i].Add(newObj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
