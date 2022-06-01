using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MovingToTheShop : MonoBehaviour
{
    public GameObject DarkPanelObj;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        DarkPanelObj.SetActive(true);
        DarkPanelObj.GetComponent<CanvasGroup>().DOFade(endValue: 1, 2f)
            .OnComplete(() => 
            {
                SceneManager.LoadScene(3);
                DarkPanelObj.GetComponent<CanvasGroup>().DOKill();
            });
    }
}