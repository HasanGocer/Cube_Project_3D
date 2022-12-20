using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetSystem : MonoSingleton<CabinetSystem>
{
    public class Cabinet
    {
        public bool[,] ObjectGridBool;
        public GameObject[,] ObjectGridGameObject;
        public float objectStartVerticalDistance;
    }
    public List<Cabinet> CabinetClass = new List<Cabinet>();

    [SerializeField] private GameObject _objectPosTemplate;
    [SerializeField] private int _OPObjectCount;

    public float objectStartHorizontalDistance, cabinetLineDistance, cabinetColumnDistance;
    //scale System ile atanacaklar
    public int cabinetLineCount, cabinetColumnCount;
    public float scaleHorizontal, scaleVertical;

    public void StartCabinetSystem()
    {
        //TaskObjectPlacement(ItemData.Instance.field.taskObjectTypeCount, _OPObjectCount)
    }

    public void AllObjectClose()
    {
        for (int i1 = 0; i1 < CabinetClass.Count; i1++)
        {
            for (int i2 = 0; i2 < cabinetLineCount; i2++)
            {
                for (int i3 = 0; i3 < cabinetColumnCount; i3++)
                {
                    CabinetClass[i1].ObjectGridGameObject[i2, i3].SetActive(false);
                }
            }
        }
    }

    public void ObjectPoolAdd(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        CabinetClass[objectID.cabinetCount].ObjectGridBool[objectID.lineCount, objectID.columnCount] = false;
        CabinetClass[objectID.cabinetCount].ObjectGridGameObject[objectID.lineCount, objectID.columnCount].SetActive(false);
    }

    private void TaskObjectPlacement(int maxCount, int OPObjectCount, int cabinetLineCount, int cabinetColumnCount, GameObject objectPosTemplate, float scaleHorizontal, float scaleVertical, float objectHorizontalDistance, float objectVerticalDistance, List<Cabinet> CabinetClass)
    {
        TaskSystem taskSystem = TaskSystem.Instance;
        for (int i = 0; i < maxCount; i++)
        {
            GameObject obj = GetObject(OPObjectCount);
            ObjectID objectID = obj.GetComponent<ObjectID>();


            ObjectTaskIDPlacement(obj, objectID, taskSystem.ObjectTypeList[i], taskSystem.ObjectMaterialList[i], CabinetClass.Count, cabinetLineCount, cabinetColumnCount, CabinetClass);
            ObjectPositionPlacement(obj, objectPosTemplate, scaleHorizontal, scaleVertical, objectID.cabinetCount, objectID.columnCount, objectHorizontalDistance, objectVerticalDistance, CabinetClass[objectID.cabinetCount].objectStartVerticalDistance, CabinetClass.Count);
        }
    }

    private void ObjectPlacement(int OPObjectCount, int cabinetLineCount, int cabinetColumnCount, int maxObjectCount, int maxObjectMaterialCount, GameObject objectPosTemplate, float scaleHorizontal, float scaleVertical, float objectHorizontalDistance, float objectVerticalDistance, List<Cabinet> CabinetClass)
    {
        for (int i1 = 0; i1 < CabinetClass.Count; i1++)
        {
            for (int i2 = 0; i2 < cabinetLineCount; i2++)
            {
                for (int i3 = 0; i3 < cabinetColumnCount; i3++)
                {
                    if (!CabinetClass[i1].ObjectGridBool[i2, i3])
                    {
                        GameObject obj = GetObject(OPObjectCount);
                        ObjectID objectID = obj.GetComponent<ObjectID>();

                        ObjectIDPlacement(obj, objectID, maxObjectCount, maxObjectMaterialCount, CabinetClass.Count, cabinetLineCount, cabinetColumnCount, CabinetClass);
                        ObjectPositionPlacement(obj, objectPosTemplate, scaleHorizontal, scaleVertical, objectID.cabinetCount, objectID.columnCount, objectHorizontalDistance, objectVerticalDistance, CabinetClass[objectID.cabinetCount].objectStartVerticalDistance, CabinetClass.Count);
                    }
                }
            }
        }
    }

    private GameObject GetObject(int OPObjectCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPObjectCount);
    }
    private void ObjectTaskIDPlacement(GameObject obj, ObjectID objectID, int ID, int MaterialCount, int maxCabinetCount, int maxLineCount, int maxColumnCount, List<Cabinet> cabinet)
    {
        objectID.objectID = ID;
        objectID.materialCount = MaterialCount;
        GameObject child = obj.transform.GetChild(objectID.objectID).gameObject;

        child.gameObject.SetActive(true);
        child.GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;

        objectID.cabinetCount = Random.Range(0, maxCabinetCount);
        objectID.lineCount = Random.Range(0, maxLineCount);
        objectID.columnCount = Random.Range(0, maxColumnCount);
        cabinet[objectID.cabinetCount].ObjectGridBool[objectID.lineCount, objectID.columnCount] = true;
        cabinet[objectID.cabinetCount].ObjectGridGameObject[objectID.lineCount, objectID.columnCount] = this.gameObject;

    }
    private void ObjectIDPlacement(GameObject obj, ObjectID objectID, int maxObjectCount, int maxObjectMaterialCount, int maxCabinetCount, int maxLineCount, int maxColumnCount, List<Cabinet> cabinet)
    {

        int ID, materialCount;
        do
        {
            ID = Random.Range(0, maxObjectCount);
            materialCount = Random.Range(0, maxObjectMaterialCount);
        }
        while (CheckObjectID(ID, materialCount));

        objectID.objectID = ID;
        objectID.materialCount = materialCount;

        GameObject child = obj.transform.GetChild(objectID.objectID).gameObject;

        child.gameObject.SetActive(true);
        child.GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;

        objectID.cabinetCount = Random.Range(0, maxCabinetCount);
        objectID.lineCount = Random.Range(0, maxLineCount);
        objectID.columnCount = Random.Range(0, maxColumnCount);
        cabinet[objectID.cabinetCount].ObjectGridBool[objectID.lineCount, objectID.columnCount] = true;
        cabinet[objectID.cabinetCount].ObjectGridGameObject[objectID.lineCount, objectID.columnCount] = this.gameObject;

    }
    private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, float scaleHorizontal, float scaleVertical, int cabinetLineCount, int cabinetColumnCount, float objectHorizontalDistance, float objectVerticalDistance, float objectStartVerticalDistance, int cabinetCount)
    {
        obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + cabinetLineCount - 1 * scaleHorizontal + cabinetLineCount * (scaleHorizontal / 5) * 2 + scaleHorizontal / 2 + cabinetCount * objectHorizontalDistance, objectPosTemplate.transform.position.y + objectVerticalDistance * cabinetColumnCount + objectStartVerticalDistance + objectVerticalDistance - scaleVertical / 2, objectPosTemplate.transform.position.z);
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

}
