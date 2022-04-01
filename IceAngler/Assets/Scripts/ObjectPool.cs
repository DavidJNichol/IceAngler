using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance { get { return sharedInstance; } }

    private static ObjectPool sharedInstance;

    public List<MarineObject> objectsToPool;

    private List<MarineObject> pooledObjects;

    [SerializeField] private int minimumAmountOfObjects;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        pooledObjects = new List<MarineObject>();

        InstantiateMarineObjectsForScene();

        if (minimumAmountOfObjects == 0)
            minimumAmountOfObjects = 1;
    }

    public MarineObject GetPooledObject()
    {
        for (int i = 0; i < minimumAmountOfObjects; i++)
        {
            if(!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    private void InstantiateMarineObjectsForScene()
    {
        MarineObject tmp;
        for (int i = 0; i < minimumAmountOfObjects; i++)
        {
            tmp = Instantiate(objectsToPool[i]);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
}
