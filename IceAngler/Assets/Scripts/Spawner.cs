using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public List<MarineObject> ObjectsInScene { get { return objectsInScene; } set { objectsInScene = value; } }
    private List<MarineObject> objectsInScene;
    public float SpawnOffsetY { get; set; }
    private float offsetY;

    void Start()
    {
        objectsInScene = new List<MarineObject>();

        SpawnRandomMarineObject();
        SpawnRandomMarineObject();
        SpawnRandomMarineObject();
    }

    private void Update()
    {
        RespawnObjectsIfDeactivated();
    }
    private void RespawnObjectsIfDeactivated()
    {
        for (int i = 0; i < objectsInScene.Count; i++)
        {
            if (!objectsInScene[i].gameObject.activeInHierarchy)
            {
                objectsInScene.RemoveAt(i); // object has been deactivated, remove from current pool
                SpawnRandomMarineObject();
            }
        }
    }

    private void SpawnRandomMarineObject()
    {
        // create temp marine obj and assign to OP singleton get pooled object
        MarineObject marineObject = ObjectPool.SharedInstance.GetPooledObject();

        if (marineObject != null)
        {
            // Set spawn position for object
            marineObject.transform.position = 
                new Vector3(this.transform.position.x, this.transform.position.y + offsetY, this.transform.position.z);

            marineObject.gameObject.SetActive(true);

            objectsInScene.Add(marineObject);
        }

        AdjustSpawnOffset(-100);
    }

    private void AdjustSpawnOffset(float amount)
    {
        offsetY += amount;

        if (offsetY < -200)
            offsetY = 0;
    }
}