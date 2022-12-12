using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoSingleton<LevelSystem>
{
    [SerializeField] private int _levelMod;

    public void NewLevelCheckField()
    {
        if (GameManager.Instance.level >= _levelMod * ItemData.Instance.factor.objectCount)
        {
            ItemData.Instance.SetObjectTypeCount();
            ItemData.Instance.SetObjectCount();
            ItemData.Instance.SetObjectTypeCount();
        }
    }
}
