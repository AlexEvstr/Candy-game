using UnityEngine;
using UnityEngine.UI;

public class GameAvatar : MonoBehaviour
{
    [SerializeField] private Image _gameAvatar;
    [SerializeField] private Sprite[] _avatars;

    private void Start()
    {
        int avatarIndex = PlayerPrefs.GetInt("AvatarGame", 0);
        _gameAvatar.sprite = _avatars[avatarIndex];
    }
}