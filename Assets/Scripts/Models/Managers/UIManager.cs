using System;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Models.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject initialPanel;
        public GameObject gamePanel;
        public TextMeshProUGUI currentLevelText;
        public TextMeshProUGUI nextLevelText;
        [SerializeField] private List<Image> stageImageList;
        private bool gameStarted = false;
        private Color completedColor = new Color(255/255f, 152/255f, 0/255f); // color code FF9800

        void Update()
        {
            if (!gameStarted)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gameStarted = true;
                    initialPanel.SetActive(false);
                    SetGamePanel();
                    GameManager.instance.ChangeGameState(GameState.Moving);
                }
            }
        }

        private void SetGamePanel()
        {
            gamePanel.SetActive(true);
            int currentLevel = LevelManager.instance.GetCurrentLevelIndex() + 1;
            currentLevelText.text = currentLevel.ToString();
            nextLevelText.text = (currentLevel + 1).ToString();
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
    }
    
}

