using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] PlatformSpawner _spawner;
    bool _isGameOver = false;

    ScoreManager _scoreManager;

    public static event Action OnStartGame;
    public static event Action<bool> OnEndGame;

    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
        _player.OnFirstJump += StartGame;
    }

    private void OnEnable()
    {
#if USE_PLUGIN
        AdController.OnRewardedAdWatched += Revive;
#endif
    }
    private void OnDisable()
    {
#if USE_PLUGIN
        AdController.OnRewardedAdWatched -= Revive;
#endif
    }

    void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public void EndGame()
    {
        if (_isGameOver) return;

        _isGameOver = true;
        _player.OnGameOver();
        _spawner.OnGameOver();

        OnEndGame?.Invoke(CanRevive());

        SoundController.Instance.PlayAudio(AudioType.GAMEOVER);
    }

    private bool CanRevive()
    {
        return _scoreManager.Score > 10;
    }

    private void Revive()
    {
        _isGameOver = false;
        _player.Revive();
        _spawner.Revive();
    }
}