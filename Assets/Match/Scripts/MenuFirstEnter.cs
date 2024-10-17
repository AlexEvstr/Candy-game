using UnityEngine;
using UnityEngine.UI;

public class MenuFirstEnter : MonoBehaviour
{
    [SerializeField] private GameObject _ava_00;
    [SerializeField] private GameObject _ava_11;
    [SerializeField] private Button _okay;
    [SerializeField] private Button _ava_0;
    [SerializeField] private Button _ava_1;
    [SerializeField] private GameObject _firstEnterPanel;
    [SerializeField] private GameObject _menuPanel;


    private void Start()
    {
        int avatar = PlayerPrefs.GetInt("HasAvatar", 0);
        if (avatar == 0)
        {
            _menuPanel.SetActive(false);
            _firstEnterPanel.SetActive(true);
        }
        else
        {
            _firstEnterPanel.SetActive(false);
            _menuPanel.SetActive(true);
        }
        _okay.interactable = false;
        _okay.onClick.AddListener(OkayBtn);
        _ava_0.onClick.AddListener(FirstAvaBtn);
        _ava_1.onClick.AddListener(SecondAvaBtn);
    }

    private void OkayBtn()
    {
        PlayerPrefs.SetInt("HasAvatar", 1);
        _firstEnterPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    private void FirstAvaBtn()
    {
        _ava_11.SetActive(false);
        _ava_00.SetActive(true);
        _okay.interactable = true;
        PlayerPrefs.SetInt("AvatarGame", 0);
    }

    private void SecondAvaBtn()
    {
        _ava_00.SetActive(false);
        _ava_11.SetActive(true);
        _okay.interactable = true;
        PlayerPrefs.SetInt("AvatarGame", 1);
    }
}