using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Models.Managers
{
    public class UIManager : MonoBehaviour
    {
        public Image mainMask;
        public TextMeshProUGUI currentLevelText;
        public TextMeshProUGUI nextLevelText;
        
        [SerializeField] private CanvasGroup initialPanel;
        [SerializeField] private CanvasGroup losePanel;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private CanvasGroup gamePanel;
        [SerializeField] private CanvasGroup completePanel;
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
            if (stageIndex >= stageImageList.Count)
            {
                stageIndex = stageImageList.Count;
            }

            for (int i = 0; i < stageIndex; i++)
            {
                if (stageImageList[i].color != completedColor)
                {
                    Debug.Log("Change color!");
                    stageImageList[i].color = completedColor;
                }
            }
        }

        private void ResetStagesOnUI()
        {
            for (int i = 0; i < stageImageList.Count; i++)
            {

                stageImageList[i].color = Color.white;
            }
        }
        
        private void FadeInWinPanel()
        {
            winPanel.gameObject.SetActive(true);
        }

        private void FadeCompletePanel()
        {
            completePanel.gameObject.SetActive(true);
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
            else if (gameState.HasFlag(GameState.Complete))
            {
                gamePanel.gameObject.SetActive(false);
                mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
                SetMaskState(mainMask, true);
                Sequence sequence = DOTween.Sequence();
                sequence.PrependInterval(0.8f).OnComplete(() => FadeCompletePanel());
            }
        }

        private void SetTryAgain()
        {
            gamePanel.gameObject.SetActive(false);
            winPanel.gameObject.SetActive(false);
            completePanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(false);
            mainMask.gameObject.SetActive(false);
            initialPanel.gameObject.SetActive(true);
            gameStarted = false;
            ResetStagesOnUI();
        }
        
        private void SetNextLevel()
        {
            gamePanel.gameObject.SetActive(false);
            winPanel.gameObject.SetActive(false);
            completePanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(false);
            mainMask.gameObject.SetActive(false);
            initialPanel.gameObject.SetActive(true);
            gameStarted = false;
            ResetStagesOnUI();
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

