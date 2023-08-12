using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject restartPanel;
    [SerializeField] TMP_Text scoreText,sliderValText;
    [SerializeField] GameEvent restartEvent,numberOfCircleEvent;
    [SerializeField] Slider slider;

    void Start()
    {
        restartPanel.SetActive(false);
        sliderValText.text=slider.value.ToString();
    }

    #region  Restart Panel
    void ShowReStartPanel(int score)
    {
        restartPanel.transform.localScale=new Vector3(0,0,0);
        restartPanel.SetActive(true);
        restartPanel.transform.DOScale(1f, 1f);
       
        scoreText.text="Score: "+ score.ToString();

    }


    //exit Game
    public void OnClikExit()
    {
        Application.Quit();
    }
    //onclick show panel animation
    public void OnClickRestart()
    {
        //on complete animation restart game
        restartPanel.transform.DOScale(0f, 0.5f).OnComplete(() => restartGame());
       
    }

    void restartGame()
    {
        restartPanel.SetActive(false);
        numberOfCircleEvent.Raise(this,(int)slider.value);
        restartEvent.Raise(this,(int)slider.value);
    }

    //Listn to event
    //when game ended
    public void GameEnded(Component sender,object data)
    {
        if(sender is CircleManager )
        {
            //if game ended show restart panel
            ShowReStartPanel((int)data);
        }
    }
    #endregion

    #region Slider

    //when got slider input
    public void OnSliderValueChange()
    {
        int val=(int)slider.value;
        sliderValText.text=val.ToString();
    }


    #endregion
}
