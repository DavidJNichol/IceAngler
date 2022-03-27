using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance { get { return sharedInstance; } }

    private static ObjectPool sharedInstance;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
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

    // Instant
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp = new GameObject();
        InstantiateRequiredObjects(tmp);
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < minimumAmountOfObjects; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        return null;
    }

    private void InstantiateRequiredObjects(GameObject gameObject)
    {
        for (int i = 0; i < minimumAmountOfObjects; i++)
        {
            gameObject = Instantiate(objectToPool);
            gameObject.SetActive(false);
            pooledObjects.Add(gameObject);
        }
    }

}
