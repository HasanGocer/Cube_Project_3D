using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSystem : MonoSingleton<RandomSystem>
{
    public List<GameObject> ObjectList = new List<GameObject>();
    [SerializeField] private GameObject _objectPosTemplate;
    [SerializeField] private int _OPObjectCount;
    [SerializeField] private int _xDÝstance, _zDÝstance;
    [SerializeField] private float _objectPlacementTime;

    public void StartRandomSystem()
    {
        StartCoroutine(ObjectPlacementIenumerator(ItemData.Instance.field.objectCount, _OPObjectCount, ItemData.Instance.field.ObjectTypeCount, _xDÝstance, _zDÝstance, _objectPlacementTime, _objectPosTemplate, ObjectList));
    }

    public IEnumerator ObjectPlacementIenumerator(int maxCount, int OPObjectCount, int maxObjectCount, int xDÝstance, int zDistance, float objectPlacementTime, GameObject objectPosTemplate, List<GameObject> objects)
    {
        while (true)
        {
            if (ObjectCountCheck(maxCount, objects))
            {
                GameObject obj = GetObject(OPObjectCount);
                if (objects.Count == 0)
                    ObjectIDPlacement(obj, maxObjectCount, objects, objects.Count);
                else
                    ObjectIDPlacement(obj, maxObjectCount, objects);
                AddList(obj, objects);
                ObjectPositionPlacement(obj, objectPosTemplate, xDÝstance, zDistance);
                yield return new WaitForSeconds(objectPlacementTime);
            }
            yield return null;
        }
    }

    public void ObjectPoolAdd(GameObject obj, List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == obj)
                objects.RemoveAt(i);
        }
        obj.transform.GetChild(obj.GetComponent<ObjectID>().objectID).gameObject.SetActive(false);
        ObjectPool.Instance.AddObject(_OPObjectCount, obj);
    }

    private bool ObjectCountCheck(int maxCount, List<GameObject> objects)
    {
        if (objects.Count <= maxCount)
            return true;
        else
            return false;
    }
    private GameObject GetObject(int OPObjectCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPObjectCount);
    }
    private void AddList(GameObject obj, List<GameObject> objects)
    {
        objects.Add(obj);
    }
    private void ObjectIDPlacement(GameObject obj, int maxObjectCount, List<GameObject> objects, int count = -1)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();

        if (count != -1)
            objectID.objectID = count;
        else
            objectID.objectID = Random.Range(1, maxObjectCount);
        obj.transform.GetChild(objectID.objectID).gameObject.SetActive(true);
        objectID.ListCount = objects.Count - 1;
    }
    private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, int xDÝstance, int zDistance)
    {
        int tempX = Random.Range(0, xDÝstance);
        int tempZ = Random.Range(0, zDistance);
        obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + tempX, objectPosTemplate.transform.position.y, objectPosTemplate.transform.position.z + tempZ);
    }
}
