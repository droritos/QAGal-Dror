using NUnit.Framework;
using UnityEngine;

public class PoolingControllerEditModeTests
{
    [Test]
    public void GetPoolingObject_CreatesNewObject_WhenPoolIsEmpty()
    {
        // 1. Setup our "Warehouse"
        GameObject controllerObj = new GameObject("PoolController");
        PoolingController pool = controllerObj.AddComponent<PoolingController>();
        pool.poolingObjectsClass = new PoolingObjects[0]; 
        
        // Emulate Awake injection since we are in EditMode
        PoolingController.instance = pool;

        // Create a dummy prefab to request from the pool
        GameObject dummyPrefab = new GameObject("Dummy");

        // 2. Act
        // We request an object that is NOT in the pool's array list. 
        // A good pooling system should automatically instantiate it dynamically to prevent crashing.
        GameObject result = pool.GetPoolingObject(dummyPrefab);

        // 3. Assert
        Assert.IsNotNull(result, "PoolingController should dynamically instantiate an object if the pool ran out of supply.");
        Assert.AreEqual("Dummy(Clone)", result.name, "The new object should be instantiated correctly as a clone.");
        Assert.IsFalse(result.activeSelf, "The newly created object in the pool should initially be inactive.");

        // Cleanup to keep the editor clean
        GameObject.DestroyImmediate(controllerObj);
        GameObject.DestroyImmediate(dummyPrefab);
        GameObject.DestroyImmediate(result);
    }
}
