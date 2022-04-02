using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public List<MarineObject> ObjectsInScene { get { return objectsInScene; } set { objectsInScene = value; } }
    private List<MarineObject> objectsInScene;
    public float SpawnOffsetY { get; set; }
    private float offsetY;

    private List<float> spawnYCoordinates;

    void Start()
    {
        spawnYCoordinates = new List<float>();
        objectsInScene = new List<MarineObject>();

        spawnYCoordinates.Add(112);
        spawnYCoordinates.Add(90);
        spawnYCoordinates.Add(60);
        spawnYCoordinates.Add(30);
        spawnYCoordinates.Add(0);
        spawnYCoordinates.Add(-30);
        spawnYCoordinates.Add(-60);
        spawnYCoordinates.Add(-90);
        spawnYCoordinates.Add(-110);

        SpawnRandomMarineObject();
        SpawnRandomMarineObject();
        SpawnRandomMarineObject();
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

            objectsInScene.Add(marineObject);

            marineObject.OnDeactivate += RespawnObjectsIfDeactivated;

            marineObject.gameObject.SetActive(true);

            AdjustSpawnOffset(spawnYCoordinates[Random.Range(0, spawnYCoordinates.Count)]);
        }
    }

    private void AdjustSpawnOffset(float amount)
    {
        offsetY += amount;

        if (offsetY < -110 || offsetY > 112)
            offsetY = 0;
    }
}