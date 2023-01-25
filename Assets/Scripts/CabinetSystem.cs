using System.Collections.Generic;
using UnityEngine;

public class CabinetSystem : MonoSingleton<CabinetSystem>
{
    [System.Serializable]
    public class Cabinet
    {
        public bool[,] ObjectGridBool;
        public GameObject[,] ObjectGridGameObject;
        public float objectStartVerticalDistance;
    }
    public List<Cabinet> CabinetClass = new List<Cabinet>();

    [SerializeField] private GameObject _objectPosTemplate;
    [SerializeField] private int _OPObjectCount;

    public float cabinetEmptyDistanceVertical, cabinetEmptyDistanceHorizantal;
    public float cabinetLineDistance, cabinetColumnDistance;
    public int cabinetColumnCount = 8;

    public void StartCabinetSystem()
    {
        ReSizeCabinetClassArray(cabinetColumnCount, CabinetClass);
        TaskObjectPlacement(ItemData.Instance.field.taskObjectTypeCount, _OPObjectCount, cabinetColumnCount, cabinetColumnCount, _objectPosTemplate, ScaleSystem.Instance.scaleHorizontalDisctance, ScaleSystem.Instance.scaleVerticalDistance, cabinetLineDistance, cabinetColumnDistance, cabinetEmptyDistanceHorizantal, cabinetEmptyDistanceVertical, CabinetClass);
        ObjectPlacement(_OPObjectCount, cabinetColumnCount, ItemData.Instance.field.taskObjectTypeCount, MateraiSystem.Instance.ObjectMateral.Count, _objectPosTemplate, ScaleSystem.Instance.scaleHorizontalDisctance, ScaleSystem.Instance.scaleVerticalDistance, cabinetLineDistance, cabinetColumnDistance, cabinetEmptyDistanceHorizantal, cabinetEmptyDistanceVertical, CabinetClass);
    }

    public void AllObjectClose()
    {
        for (int i1 = 0; i1 < CabinetClass.Count; i1++)
        {
            for (int i3 = 0; i3 < ItemData.Instance.field.cabineObjectCount; i3++)
            {
                CabinetClass[i1].ObjectGridGameObject[0, i3].SetActive(false);
            }
        }
    }
    public void ObjectPoolAdd(GameObject obj)
    {
        ObjectID objectID = obj.GetComponent<ObjectID>();
        CabinetClass[objectID.cabinetCount].ObjectGridBool[0, objectID.columnCount] = false;
        CabinetClass[objectID.cabinetCount].ObjectGridGameObject[0, objectID.columnCount].SetActive(false);
    }

    private void TaskObjectPlacement(int maxCount, int OPObjectCount, int cabinetLineCount, int cabinetColumnCount, GameObject objectPosTemplate, float scaleColumn, float scaleLine, float cabinetLineDistance, float cabinetColumnDistance, float cabineEmptyColumnDistance, float cabineEmptyLineDistance, List<Cabinet> CabinetClass)
    {
        TaskSystem taskSystem = TaskSystem.Instance;
        for (int i1 = 0; i1 < maxCount; i1++)
        {
            for (int i = 0; i < taskSystem.ObjectCountList[i1]; i++)
            {
                GameObject obj = GetObject(OPObjectCount);
                ObjectID objectID = obj.GetComponent<ObjectID>();
                ObjectScalePlacement(obj);
                ObjectTaskIDPlacement(obj, objectID, taskSystem.ObjectTypeList[i1], taskSystem.ObjectMaterialList[i1], CabinetClass.Count, cabinetLineCount, cabinetColumnCount, CabinetClass);
                ObjectPositionPlacement(obj, objectPosTemplate, objectID.cabinetCount, objectID.columnCount, cabinetColumnDistance, cabinetLineDistance, cabineEmptyColumnDistance, cabineEmptyLineDistance, scaleColumn, scaleLine, CabinetClass[objectID.cabinetCount].objectStartVerticalDistance);

            }
        }
    }
    private void ObjectPlacement(int OPObjectCount, int cabinetColumnCount, int maxObjectCount, int maxObjectMaterialCount, GameObject objectPosTemplate, float scaleColumn, float scaleLine, float cabinetLineDistance, float cabinetColumnDistance, float cabineEmptyColumnDistance, float cabineEmptyLineDistance, List<Cabinet> CabinetClass)
    {
        for (int i1 = 0; i1 < CabinetClass.Count; i1++)
        {
            for (int i3 = 0; i3 < cabinetColumnCount; i3++)
            {
                if (!CabinetClass[i1].ObjectGridBool[0, i3])
                {
                    GameObject obj = GetObject(OPObjectCount);
                    ObjectID objectID = obj.GetComponent<ObjectID>();

                    ObjectScalePlacement(obj);
                    ObjectIDPlacement(obj, objectID, maxObjectCount, maxObjectMaterialCount, i1, 0, i3, CabinetClass);
                    ObjectPositionPlacement(obj, objectPosTemplate, objectID.cabinetCount, objectID.columnCount, cabinetColumnDistance, cabinetLineDistance, cabineEmptyColumnDistance, cabineEmptyLineDistance, scaleColumn, scaleLine, CabinetClass[objectID.cabinetCount].objectStartVerticalDistance);
                }
            }
        }
    }
    private void ReSizeCabinetClassArray(int cabinetLineCount, List<Cabinet> CabinetClass)
    {
        for (int i = 0; i < CabinetClass.Count; i++)
        {
            CabinetClass[i].ObjectGridBool = new bool[1, cabinetLineCount];
            CabinetClass[i].ObjectGridGameObject = new GameObject[1, cabinetLineCount];
        }
    }

    private GameObject GetObject(int OPObjectCount)
    {
        return ObjectPool.Instance.GetPooledObject(OPObjectCount);
    }
    private void ObjectScalePlacement(GameObject obj)
    {
        obj.transform.localScale *= ScaleSystem.Instance.scale;
    }
    private void ObjectTaskIDPlacement(GameObject obj, ObjectID objectID, int ID, int MaterialCount, int maxCabinetCount, int maxLineCount, int maxColumnCount, List<Cabinet> cabinet)
    {
        do
        {
            print(maxCabinetCount);
            print(maxColumnCount);
            objectID.cabinetCount = Random.Range(0, maxCabinetCount);
            objectID.columnCount = Random.Range(0, maxColumnCount);
        }
        while (cabinet[objectID.cabinetCount].ObjectGridBool[0, objectID.columnCount]);

        objectID.objectID = ID;
        objectID.materialCount = MaterialCount;
        GameObject child = obj.transform.GetChild(objectID.objectID).gameObject;

        child.gameObject.SetActive(true);
        child.GetComponent<MeshRenderer>().material = MateraiSystem.Instance.emptyMaterial;

        cabinet[objectID.cabinetCount].ObjectGridBool[0, objectID.columnCount] = true;
        cabinet[objectID.cabinetCount].ObjectGridGameObject[0, objectID.columnCount] = this.gameObject;
    }
    private void ObjectIDPlacement(GameObject obj, ObjectID objectID, int maxObjectCount, int maxObjectMaterialCount, int cabinetCount, int lineCount, int columnCount, List<Cabinet> cabinet)
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

        objectID.cabinetCount = cabinetCount;
        objectID.columnCount = columnCount;
        cabinet[objectID.cabinetCount].ObjectGridBool[0, objectID.columnCount] = true;
        cabinet[objectID.cabinetCount].ObjectGridGameObject[0, objectID.columnCount] = this.gameObject;

    }
    private void ObjectPositionPlacement(GameObject obj, GameObject objectPosTemplate, int objectCabineCount, int objectColumnCount, float cabineColumnDistance, float cabineLineDistance, float cabineEmptyColumnDistance, float cabineEmptyLineDistance, float objectColumnDistance, float objectLineDistance, float cabineLineMisDistance)
    {
        obj.transform.position = new Vector3(objectPosTemplate.transform.position.x + (objectCabineCount - 1) * (cabineColumnDistance + cabineEmptyColumnDistance) /*+ (objectColumnCount - 1) * (objectColumnDistance / 5) * 7 + objectColumnDistance / 2*/, objectPosTemplate.transform.position.y + (objectColumnCount - 1) * (cabineEmptyLineDistance + cabineLineDistance) + cabineLineDistance / 2 + cabineLineMisDistance, objectPosTemplate.transform.position.z);
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
