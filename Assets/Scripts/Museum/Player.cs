using Photon.Pun;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    [DllImport("__Internal")]
    private static extern void Mute(string guid);

    [DllImport("__Internal")]
    private static extern void Unmute(string guid);

    public PhotonView PV;
    public float speed;

    float hAxis;
    float vAxis;
    bool rDown;

    public TextMeshPro NickNameText;
    Vector3 moveVec;
    Animator anim;

    void Awake()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        anim = GetComponentInChildren<Animator>();
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        if (PV.IsMine)
        {
            PlayerManager.Instance.DeactivatePlayer();
            PlayerManager.Instance.Player = gameObject;
            PlayerManager.Instance.HidePlayer();
            Invoke("ActivePlayer", 2f);
        }
    }

    void Update()
    {
        if (!PV.IsMine) return;
        ControllMove();
    }
    public void ActivePlayer()
    {
        PlayerManager.Instance.ShowPlayer();
        PlayerManager.Instance.ActivatePlayer();
        CameraManager.Instance.InitPlayerVirtualCamera(this.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!PV.IsMine)
            return;

        GameObject exhibitGameObject = other.gameObject;
        string tag = exhibitGameObject.tag;
        string HelpText = null;
        Exhibit CollisionExhibit = exhibitGameObject.GetComponent<Exhibit>();
        switch (tag)
        {
            case "Exhibit":
                HelpText = "E : 전시물 보기\nQ : 전시물 정보";
                PlayerManager.Instance.EnterInExhibitArea(CollisionExhibit);
                CameraManager.Instance.SetExhibitFreeLockCam(CollisionExhibit);
                CanvasManager.Instance.SetExhibitInfo(CollisionExhibit);
                break;
            case "GuestBook":
                HelpText = "E : 방명록 읽기\nQ : 방명록 작성";
                PlayerManager.Instance.EnterInExhibitArea(CollisionExhibit);
                #if !UNITY_EDITOR && UNITY_WEBGL
                ApiManager.Instance.RequestComment();
                #endif
                break;
            case "Game":
                HelpText = $"{CollisionExhibit.name}\nQ: 게임 정보\nE : 게임하기\nR : 게임 순위";
                PlayerManager.Instance.EnterInExhibitArea(CollisionExhibit);
                CanvasManager.Instance.SetGameInfo(CollisionExhibit); 
#if !UNITY_EDITOR && UNITY_WEBGL
                CollisionExhibit.RequestGameRank();
#endif
                break;
            case "Video":
                HelpText = "E : 영상 시청하기";
                PlayerManager.Instance.EnterInExhibitArea(CollisionExhibit);
                break;
            case "Audio":
#if !UNITY_EDITOR && UNITY_WEBGL
            Unmute(PlayerManager.Instance.guid.ToString());
#endif
                break;

        }
        if (HelpText != null)
            CanvasManager.Instance.SetHelpText(HelpText);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!PV.IsMine)
            return;

        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Exhibit":
            case "Game":
            case "GuestBook":
            case "Video":
                PlayerManager.Instance.ExitInExhibitArea();
                string HelpText = "방향키 : 이동\nL쉬프트 : 달리기";
                CanvasManager.Instance.SetHelpText(HelpText);
                break;
            case "Audio":
#if !UNITY_EDITOR && UNITY_WEBGL
            Mute(PlayerManager.Instance.guid.ToString());
#endif
                break;

        }
    }

    private void ControllMove()
    {
        if (!PlayerManager.Instance.IsActivePlayer) return;

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        rDown = Input.GetButton("Run");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime * (rDown ? 1.5f : 1f);

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isRun", rDown);

        PV.RPC("Rotate", RpcTarget.All, moveVec);
    }

    [PunRPC]
    private void Rotate(Vector3 moveVec)
    {
        transform.LookAt(transform.position + moveVec);
        NickNameText.transform.rotation = CameraManager.Instance.PlayerCamera.transform.rotation;
    }
}

