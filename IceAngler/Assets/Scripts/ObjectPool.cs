using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance { get { return sharedInstance; } }

    private static ObjectPool sharedInstance;

    public List<MarineObject> pooledObjects;

    public MarineObject objectToPool;

    public int minimumAmountOfObjects;

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
        MarineObject tmp = new MarineObject();
        InstantiateRequiredObjects(tmp);
    }

    public MarineObject GetPooledObject()
    {
        if(pooledObjects.Count > 0)
        {
            for (int i = 0; i < minimumAmountOfObjects; i++)
            {
                if (!pooledObjects[i].IsActivated)
                    return pooledObjects[i];
            }
            return new Shark();
        }
        return new Boot();
    }

    private void InstantiateRequiredObjects(MarineObject gameObject)
    {
        for (int i = 0; i < minimumAmountOfObjects; i++)
        {
            gameObject = Instantiate(objectToPool);
            pooledObjects.Add(gameObject);
        }
    }
}
