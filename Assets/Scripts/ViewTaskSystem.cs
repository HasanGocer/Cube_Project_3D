using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ViewTaskSystem : MonoSingleton<ViewTaskSystem>
{
    [SerializeField] Image viewPanel;
    [SerializeField] Sprite questionMark, aceptedMark, wrongMark;
    [SerializeField] List<Image> HideImage = new List<Image>();
    [SerializeField] List<Image> WrongImage = new List<Image>();
    [SerializeField] private Image tempImage;
    [SerializeField] private Material redMat;
    [SerializeField] Animator StartTaskAnim;

    public IEnumerator TrueCanvasMove(int taskCount, GameObject obj)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            tempImage.gameObject.SetActive(true);
            tempImage.gameObject.transform.position = touchPosition;
            tempImage = TaskSystem.Instance.CallImage(tempImage, taskCount);
            tempImage.gameObject.transform.DOMove(HideImage[taskCount].gameObject.transform.position, 1.5f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1.5f);
            tempImage.gameObject.SetActive(false);
            HideImage[taskCount].sprite = aceptedMark;
            obj.SetActive(false);
        }
    }

    public IEnumerator WrongCanvasMove(GameObject obj)
    {
        if (Input.touchCount > 0)
        {
            if (WrongSystem.Instance.wrongCount >= WrongSystem.Instance.maxWrongCount)
            {
                GameManager.Instance.isStart = false;
                Buttons.Instance.failPanel.SetActive(true);
                GameManager.Instance.isStart = false;
                CabinetSystem.Instance.AllObjectClose();
            }

            tempImage.gameObject.SetActive(true);
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            tempImage.gameObject.transform.position = touchPosition;
            tempImage = TaskSystem.Instance.CallWrong(tempImage, wrongMark, redMat);
            tempImage.gameObject.transform.DOMove(WrongImage[WrongSystem.Instance.wrongCount].gameObject.transform.position, 1.5f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1.5f);
            WrongSystem.Instance.wrongCount++;
            tempImage.gameObject.SetActive(false);
            WrongImage[WrongSystem.Instance.wrongCount - 1] = TaskSystem.Instance.CallWrong(WrongImage[WrongSystem.Instance.wrongCount - 1], wrongMark, redMat);
            obj.SetActive(false);
        }
    }
    public void ViewPanelOn()
    {
        viewPanel.gameObject.SetActive(true);
    }

    public IEnumerator WievTaskOff()
    {
        StartTaskAnim.enabled = true;

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

    public void CallCheckedTask(int taskCount)
    {
        HideImage[taskCount].sprite = aceptedMark;
    }

    public void OpenQuestionMark()
    {
        for (int i = 0; i < ItemData.Instance.field.taskObjectTypeCount; i++)
        {
            HideImage[i].gameObject.SetActive(true);
        }
    }
}
