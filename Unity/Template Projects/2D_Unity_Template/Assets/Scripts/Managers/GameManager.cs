using System;
using UnityEngine;

namespace GameStateManager {
    public enum GameState {
        Start,
        End,
        Pause,
        Victory,
        Lose
    }

    public class GameManager : MonoBehaviour {
#if UNITY_EDITOR
        // Reload current scene on saves
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() {
            if (UnityEditor.EditorApplication.isPlaying) {
                SceneHelper.ReloadScene();
            }
        }
#endif

        /* Singleton Game Manager */
        private static GameManager s_instance;
        public GameManager Instance {
            get { return s_instance; }
        }

        /* State Managment Elements*/
        public GameState State { get; private set; }
        public static event Action<GameState> OnGameStateChange;

        private void Awake() {
            // Ensure duplicate Game Managers are not created
            if (s_instance == null) {
                s_instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void UpdateGameState(GameState newState) {
            State = newState;

            switch (newState) {
                case GameState.Start:
                    break;

                case GameState.End:
                    break;

                case GameState.Pause:
                    break;

                case GameState.Victory:
                    break;

                case GameState.Lose:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            // Public GameState update
            OnGameStateChange?.Invoke(newState);
        }
    }
}
