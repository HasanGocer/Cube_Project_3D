using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] int _timer;

    public IEnumerator TimerStart()
    {
        while (_timer == 0 && GameManager.Instance.isStart)
        {
            yield return new WaitForSeconds(1);
            _timer--;
        }
        if (_timer == 0)
        {
            Buttons.Instance.failPanel.SetActive(true);
            GameManager.Instance.isStart = false;
        }
        yield return null;
    }
}