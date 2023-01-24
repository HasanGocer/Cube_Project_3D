using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MateraiSystem : MonoSingleton<MateraiSystem>
{
    public Material emptyMaterial;
    public List<Material> ObjectMateral = new List<Material>();
    public List<Sprite> objectTemp2D = new List<Sprite>();
    public Material Mat2D;
    public Material blur;
}
