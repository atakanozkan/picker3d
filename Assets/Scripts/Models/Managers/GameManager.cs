using System;
using Helpers.Patterns;
using Helpers.Enums;
using Objects;
using UnityEngine;
using Models.Builders;
namespace Models.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState currentGameState;
        public Player mainPlayer;
        public Action<GameState> OnGameStateChanged;
        public Action OnStageEnd;
        public Action OnCollectedBallEvent;

        [SerializeField] private PlatformBuilder platformBuilder;
        [SerializeField] private PlatformController platformController;
        [SerializeField] private PlayerController playerController;
        private void Start()
        {
            ChangeGameState(GameState.Moving);
        }

        public void ChangeGameState(GameState state)
        {
            if (currentGameState != state)
            {
                currentGameState = state;
                OnGameStateChanged?.Invoke(state);
            }
        }

        public void SetPlayer(Player player)
        {
            mainPlayer = player;
        }

        public Player GetPlayer()
        {
            return mainPlayer;
        }

        public PlatformBuilder GetPlatformBuilder()
        {
            return platformBuilder;
        }

        public void SetPlatformBuilder(PlatformBuilder builder)
        {
            platformBuilder = builder;
        }

        public PlatformController GetPlatformController()
        {
            return platformController;
        }

        public void SetPlatformController(PlatformController controller)
        {
            platformController = controller;
        }

        public PlayerController GetPlayerController()
        {
            return playerController;
        }

        public void SetPlayerController(PlayerController controller)
        {
            playerController = controller;
        }
    }
}
