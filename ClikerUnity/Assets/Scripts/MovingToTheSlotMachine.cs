using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MovingToTheSlotMachine : MonoBehaviour
{
    public GameObject DarkPanelObj;
    public GameTimer GameTimer;
    private void OnMouseDown()
    {
        GameTimer.SaveDataToJSON();

        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 1f)
            .OnComplete(() => 
            {
                SceneManager.LoadScene(2);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
