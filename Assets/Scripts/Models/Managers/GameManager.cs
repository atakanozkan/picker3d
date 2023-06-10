using System;
using Helpers.Patterns;
using Helpers.Enums;
namespace Models.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState currentGameState;
        public Player mainPlayer;
        public Action<GameState> OnGameStateChanged;
        public Action<float> onPlayerMoveHorizontal;

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

    }
}
