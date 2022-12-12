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

    public IEnumerator StartRandomSystem()
    {
        StartCoroutine(TaskObjectPlacementIenumurator(ItemData.Instance.field.taskObjectCount, _OPObjectCount, ItemData.Instance.field.ObjectTypeCount, MateraiSystem.Instance.ObjectMateral.Count, _xDÝstance, _zDÝstance, _objectPlacementTime, _objectPosTemplate, ObjectList));
        yield return new WaitForSeconds(ItemData.Instance.field.taskObjectCount * _objectPlacementTime);
        StartCoroutine(ObjectPlacementIenumerator(ItemData.Instance.field.objectCount, _OPObjectCount, ItemData.Instance.field.ObjectTypeCount, MateraiSystem.Instance.ObjectMateral.Count, _xDÝstance, _zDÝstance, _objectPlacementTime, _objectPosTemplate, ObjectList));
    }

    private IEnumerator TaskObjectPlacementIenumurator(int maxCount, int OPObjectCount, int maxObjectCount, int maxObjectMaterialCount, int xDÝstance, int zDistance, float objectPlacementTime, GameObject objectPosTemplate, List<GameObject> objects)
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject obj = GetObject(OPObjectCount);
            ObjectIDPlacement(obj, maxObjectCount, maxObjectMaterialCount, objects, TaskSystem.Instance.ObjectTypeList[i], TaskSystem.Instance.ObjectMaterialList[i]);
            AddList(obj, objects);
            ObjectPositionPlacement(obj, objectPosTemplate, xDÝstance, zDistance);
            yield return new WaitForSeconds(objectPlacementTime);

        }
        yield return null;
    }

    private IEnumerator ObjectPlacementIenumerator(int maxCount, int OPObjectCount, int maxObjectCount, int maxObjectMaterialCount, int xDÝstance, int zDistance, float objectPlacementTime, GameObject objectPosTemplate, List<GameObject> objects)
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject obj = GetObject(OPObjectCount);
            AddList(obj, objects);
            ObjectIDPlacement(obj, maxObjectCount, maxObjectMaterialCount, objects);
            ObjectPositionPlacement(obj, objectPosTemplate, xDÝstance, zDistance);
            yield return new WaitForSeconds(objectPlacementTime);
        }
        yield return null;
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
    //taskda arananla aynýsý çýkmasýn
    private void ObjectIDPlacement(GameObject obj, int maxObjectCount, int maxObjectMaterialCount, List<GameObject> objects, int ID = -1, int MaterialCount = -1)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        if (ID != -1)
            objectID.objectID = ID;
        else
            objectID.objectID = Random.Range(0, maxObjectCount);

        if (MaterialCount != -1)
            objectID.materialCount = MaterialCount;
        else
            objectID.materialCount = Random.Range(0, maxObjectMaterialCount);

        obj.transform.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;
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
