using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int objectCount, ObjectTypeCount, taskObjectCount;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;

    public void AwakeID()
    {
        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        field.ObjectTypeCount = standart.ObjectTypeCount + (factor.ObjectTypeCount * constant.ObjectTypeCount);
        field.taskObjectCount = standart.taskObjectCount + (factor.taskObjectCount * constant.taskObjectCount);
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

    public void SetObjectTaskCount()
    {
        factor.taskObjectCount++;
        field.taskObjectCount = standart.taskObjectCount + (factor.taskObjectCount * constant.taskObjectCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
