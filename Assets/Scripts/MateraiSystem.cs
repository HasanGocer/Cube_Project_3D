using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateraiSystem : MonoSingleton<MateraiSystem>
{
    public Material emptyMaterial;
    public List<Material> ObjectMateral = new List<Material>();
}
