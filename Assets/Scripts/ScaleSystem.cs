using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSystem : MonoSingleton<ScaleSystem>
{
    public float scaleHorizontalDisctance, scaleVerticalDistance;
    public float scale;

    public void startScaleSystem()
    {
        CabinetSystem cabinetSystem = CabinetSystem.Instance;
        ItemData ýtemData = ItemData.Instance;
        //scale = ((cabinetSystem.cabinetLineDistance / (float)ýtemData.field.cabineObjectCount) * 5) / 12;
        scaleHorizontalDisctance *= scale;
        scaleVerticalDistance *= scale;
    }
}
