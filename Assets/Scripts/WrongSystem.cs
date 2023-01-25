using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongSystem : MonoSingleton<WrongSystem>
{
    public int wrongCount = 0, maxWrongCount = 2;

}
