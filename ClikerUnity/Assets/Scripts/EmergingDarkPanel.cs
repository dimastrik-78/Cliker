using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmergingDarkPanel : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(endValue: 0, 1f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                gameObject.GetComponent<CanvasGroup>().DOKill();
            });
    }
}
