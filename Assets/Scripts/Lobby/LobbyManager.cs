using Photon.Pun;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public int MaxPlayers;
    public string NickName { get; set; }
    public string CharacterName { get; set; }
    public TMP_InputField NickNameInputField;
    public GameObject Notice;
    public GameObject EnterRoomButton;

    [DllImport("__Internal")]
    private static extern void CheckPlayer();
    private int m_joinPlayerCount;

    public static LobbyManager Instance;
    private void Awake()
    {
#if UNITY_EDITOR
        NickNameInputField.text = "d102";
#endif
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        NickNameInputField.onValueChanged.AddListener(OnValueChanged);
        MaxPlayers = 15;
    }
    public void onClickEnterRoom()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        CheckPlayer();
#endif
        Invoke("JoinGame", 1f);
    }
    private void JoinGame()
    {
        if (NickNameInputField.text == "큐레이터")
        {
            NickName = NickNameInputField.text;
            this.CharacterName = PlayerSelectManager.Instance.GetCharacterName();
            SceneManager.LoadScene("Museum");
            return;
        }

        if (m_joinPlayerCount >= MaxPlayers)
        {
            Notice.SetActive(true);
            EnterRoomButton.GetComponent<Button>().interactable = false;
            Invoke("DeactivateNotice", 5f);
            return;
        } 
        if (NickNameInputField.text.Length == 0)
        {
            NickNameInputField.ActivateInputField();
            return;
        }
        NickName = NickNameInputField.text;
        this.CharacterName = PlayerSelectManager.Instance.GetCharacterName();
        SceneManager.LoadScene("Museum");
    }

    private void DeactivateNotice()
    {
        Notice.SetActive(false);
        EnterRoomButton.GetComponent<Button>().interactable = true;
    }

    public void OnValueChanged(string value)
    {
        if (value.Length > 6)
        {
            NickNameInputField.text = value.Substring(0, 6);
        }
    }
    public void OnCheckPlayer(int JoinPlayerCount)
    {
        m_joinPlayerCount = JoinPlayerCount;
    }
}
