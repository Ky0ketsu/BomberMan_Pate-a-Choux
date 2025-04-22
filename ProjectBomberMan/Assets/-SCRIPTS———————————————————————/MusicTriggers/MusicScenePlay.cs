using UnityEngine;

public class MusicScenePlay : MonoBehaviour
{
    [SerializeField]SO_Playlist scenePlaylist;

    void OnEnable()
    {
        EVENTS.OnGameStart += SetPlaylist;
    }

    void OnDisable()
    {
        EVENTS.OnGameStart -= SetPlaylist;
    }

    void SetPlaylist(int sceneLoaded)
    {
        SetPlaylist();
    }

    void SetPlaylist()
    {
        MUSIC.PLAYER.SetPlaylist(scenePlaylist);
    }
} // SCRIPT END
