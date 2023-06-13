using System;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Models.Managers
{
    public class UIManager : MonoBehaviour
    {
        public CanvasGroup initialPanel;
        public CanvasGroup losePanel;
        public CanvasGroup winPanel;
        public CanvasGroup gamePanel;
        public Image mainMask;
        public TextMeshProUGUI currentLevelText;
        public TextMeshProUGUI nextLevelText;
        [SerializeField] private List<Image> stageImageList;
        private int currentLevelValue;
        private bool gameStarted;
        private Color completedColor = new Color(255/255f, 152/255f, 0/255f); // color code FF9800

        private void Start()
        {
            gameStarted = false;
        }

        void Update()
        {
            if (!gameStarted)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gameStarted = true;
                    initialPanel.gameObject.SetActive(false);
                    SetGamePanel();
                    GameManager.instance.ChangeGameState(GameState.Moving);
                }
            }

            currentLevelValue = LevelManager.instance.GetCurrentLevelIndex()+1;
            SetTextValues();
        }

        private void SetGamePanel()
        {
            gamePanel.gameObject.SetActive(true);
            SetTextValues();
            SetStagesOnUI();
        }

        private void SetStagesOnUI()
        {
            int stageIndex = LevelManager.instance.GetCurrentStagIndex();

            for (int i = 0; i <= stageIndex; i++)
            {
                if (stageImageList[i].color != completedColor)
                {
                    stageImageList[i].color = Color.Lerp(stageImageList[i].color, completedColor, 0);
                    stageImageList[i].DOFade(1f, 1f);
                }
            }
        }
        private void FadeInWinPanel()
        {
            winPanel.gameObject.SetActive(true);
        }

        private void FadeInLosePanel()
        {
            losePanel.gameObject.SetActive(true);
            losePanel.gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
        }

        
        public void ControlPanels(GameState gameState)
        {
            if (gameState.HasFlag(GameState.Lose))
            {
                gamePanel.gameObject.SetActive(false);
                mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
                SetMaskState(mainMask, true);
                Sequence sequence = DOTween.Sequence();
                sequence.PrependInterval(0.8f).OnComplete(() => FadeInLosePanel());
            }
            else if (gameState.HasFlag(GameState.Win))
            {
                gamePanel.gameObject.SetActive(false);
                mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
                SetMaskState(mainMask, true);
                Sequence sequence = DOTween.Sequence();
                sequence.PrependInterval(0.8f).OnComplete(() => FadeInWinPanel());
            }
        }

        private void SetTryAgain()
        {
            gamePanel.gameObject.SetActive(false);
            winPanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(false);
            mainMask.gameObject.SetActive(false);
            initialPanel.gameObject.SetActive(true);
            gameStarted = false;
        }
        
        private void SetNextLevel()
        {
            gamePanel.gameObject.SetActive(false);
            winPanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(false);
            mainMask.gameObject.SetActive(false);
            initialPanel.gameObject.SetActive(true);
            gameStarted = false;
        }
        
        private void SetMaskState(Image mask, bool isActive, Action onClickAction = null)
        {
            if (isActive)
            {
                mask.gameObject.SetActive(true);
            }
            else
            {
                mask.gameObject.SetActive(false);
            }
        }
        
        private void SetTextValues()
        {
            currentLevelText.text = currentLevelValue.ToString();
            nextLevelText.text = (currentLevelValue + 1).ToString();
        }
        

        private void OnEnable()
        {
            GameManager.instance.OnGameStateChanged += ControlPanels;
            GameManager.instance.OnTryAgain += SetTryAgain;
            GameManager.instance.OnNextLevel += SetNextLevel;
            GameManager.instance.OnStageEnd += SetStagesOnUI;
        }

        private void OnDisable()
        {
            GameManager.instance.OnGameStateChanged -= ControlPanels;
            GameManager.instance.OnTryAgain -= SetTryAgain;
            GameManager.instance.OnNextLevel -= SetNextLevel;
            GameManager.instance.OnStageEnd -= SetStagesOnUI;
        }
    }
    
}

