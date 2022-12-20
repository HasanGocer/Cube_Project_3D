using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSystem : MonoSingleton<RandomSystem>
{

    //iptal



    /* public List<GameObject> ObjectList = new List<GameObject>();
     [SerializeField] private GameObject _objectPosTemplate;
     [SerializeField] private int _OPObjectCount;
     [SerializeField] private int _xDÝstance, _zDÝstance;
     [SerializeField] private float _objectPlacementTime;



     public void StartRandomSystem()
     {
         TaskObjectPlacementIenumurator(ItemData.Instance.field.taskObjectTypeCount, _OPObjectCount, _xDÝstance, _zDÝstance, _objectPosTemplate, ObjectList);
         ObjectPlacementIenumerator(ItemData.Instance.field.objectCount, _OPObjectCount, ItemData.Instance.field.ObjectTypeCount, MateraiSystem.Instance.ObjectMateral.Count, _xDÝstance, _zDÝstance, _objectPlacementTime, _objectPosTemplate, ObjectList);
     }

     private void TaskObjectPlacementIenumurator(int maxCount, int OPObjectCount, int xDÝstance, int zDistance, GameObject objectPosTemplate, List<GameObject> objects)
     {
         for (int i = 0; i < maxCount; i++)
         {
             for (int i1 = 0; i1 < TaskSystem.Instance.ObjectCountList[i]; i1++)
             {
                 GameObject obj = GetObject(OPObjectCount);
                 AddList(obj, objects);
                 ObjectTaskIDPlacement(obj, objects, TaskSystem.Instance.ObjectTypeList[i], TaskSystem.Instance.ObjectMaterialList[i]);
                 ObjectPositionPlacement(obj, objectPosTemplate, xDÝstance, zDistance, 5, 2);
             }
         }
     }

     private void ObjectPlacementIenumerator(int maxCount, int OPObjectCount, int maxObjectCount, int maxObjectMaterialCount, int xDÝstance, int zDistance, float objectPlacementTime, GameObject objectPosTemplate, List<GameObject> objects)
     {
         for (int i = 0; i < maxCount; i++)
         {
             GameObject obj = GetObject(OPObjectCount);
             AddList(obj, objects);
             ObjectIDPlacement(obj, maxObjectCount, maxObjectMaterialCount, objects);
             ObjectPositionPlacement(obj, objectPosTemplate, xDÝstance, zDistance, 5, 2);
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

     public void AllObjectClose()
     {
         for (int i = 0; i < ObjectList.Count; i++)
         {
             ObjectList[i].SetActive(false);
         }
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
     private void ObjectTaskIDPlacement(GameObject obj, List<GameObject> objects, int ID, int MaterialCount)
     {
         ObjectID objectID = obj.GetComponent<ObjectID>();
         objectID.objectID = ID;
         objectID.materialCount = MaterialCount;

         obj.transform.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;
         obj.transform.GetChild(objectID.objectID).gameObject.SetActive(true);
         objectID.ListCount = objects.Count - 1;
     }
     private void ObjectIDPlacement(GameObject obj, int maxObjectCount, int maxObjectMaterialCount, List<GameObject> objects)
     {
         ObjectID objectID = obj.GetComponent<ObjectID>();

         int ID, materialCount;
         do
         {
             ID = Random.Range(0, maxObjectCount);
             materialCount = Random.Range(0, maxObjectMaterialCount);
         }
         while (CheckObjectID(ID, materialCount));
         objectID.objectID = ID;
         objectID.materialCount = materialCount;
         obj.transform.GetChild(objectID.objectID).GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;
         obj.transform.GetChild(objectID.objectID).gameObject.SetActive(true);
         objectID.ListCount = objects.Count - 1;
     }
     private bool CheckObjectID(int ID, int materialCount)
     {
         TaskSystem taskSystem = TaskSystem.Instance;
         bool isTrue = false;
         for (int i1 = 0; i1 < taskSystem.ObjectTypeList.Count; i1++)
         {
             for (int i2 = 0; i2 < taskSystem.ObjectMaterialList.Count; i2++)
             {
                 if (ID == taskSystem.ObjectTypeList[i1] && materialCount == taskSystem.ObjectMaterialList[i2])
                     isTrue = true;
             }
         }

         return isTrue;
     }
     private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, int xDÝstance, int zDistance, float factorX, float factorY)
     {
         obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + factorX, objectPosTemplate.transform.position.y + factorY / 2, objectPosTemplate.transform.position.z);
     }*/
}
