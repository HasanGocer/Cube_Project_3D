using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewTaskSystem : MonoSingleton<ViewTaskSystem>
{
    [SerializeField] Image viewPanel;
    [SerializeField] Sprite questionMark, aceptedMark;
    [SerializeField] List<Image> HideImage = new List<Image>();

    public void viewPanelOn()
    {
        viewPanel.gameObject.SetActive(true);
    }

    public IEnumerator WievTaskOff()
    {
        float lerpCount = 0;
        while (true)
        {
            lerpCount += Time.deltaTime * 2;
            viewPanel.color = Color.Lerp(viewPanel.color, MateraiSystem.Instance.blur.color, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime * 2);
            if (viewPanel.color == MateraiSystem.Instance.blur.color)
            {
                viewPanel.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OpenQuestionMark()
    {
        for (int i = 0; i < ItemData.Instance.field.taskObjectTypeCount; i++)
        {

        }
    }
}
