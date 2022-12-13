using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int objectCount, ObjectTypeCount, taskObjectTypeCount, taskObjectTypeCountCount;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;

    public void AwakeID()
    {
        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        field.ObjectTypeCount = standart.ObjectTypeCount + (factor.ObjectTypeCount * constant.ObjectTypeCount);
        field.taskObjectTypeCount = standart.taskObjectTypeCount + (factor.taskObjectTypeCount * constant.taskObjectTypeCount);
        field.taskObjectTypeCountCount = standart.taskObjectTypeCountCount + (factor.taskObjectTypeCountCount * constant.taskObjectTypeCountCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetObjectCount()
    {
        factor.objectCount++;
        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetObjectTypeCount()
    {
        factor.ObjectTypeCount++;
        field.ObjectTypeCount = standart.ObjectTypeCount + (factor.ObjectTypeCount * constant.ObjectTypeCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetObjectTaskTypeCount()
    {
        factor.taskObjectTypeCount++;
        field.taskObjectTypeCount = standart.taskObjectTypeCount + (factor.taskObjectTypeCount * constant.taskObjectTypeCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetObjectTaskTypeCountCount()
    {
        factor.taskObjectTypeCountCount++;
        field.taskObjectTypeCountCount = standart.taskObjectTypeCountCount + (factor.taskObjectTypeCountCount * constant.taskObjectTypeCountCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
