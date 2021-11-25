using UnityEngine;
using UnityEngine.Video;

public class VideoManager : Singleton<VideoManager>
{
    public VideoPlayer vp;
    private string FileName = "testVideo.mp4";
    private void Awake()
    {
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, FileName);
        //Debug.Log(vp.url);
    }
    void Update()
    {
        
    }
    public void VideoPlay()
    {
        CanvasManager.Instance.InVideoObjOn(true);
        vp.loopPointReached += EndPoint;
        vp.Play();
    }

    public void EndPoint(VideoPlayer vp)
    {
        VideoStop();
    }
    public void VideoStop()
    {
        vp.Stop();
        CanvasManager.Instance.InVideoObjOn(false);
        PlayerManager.Instance.IsActivePlayer = true;
        PlayerManager.Instance.BGM.volume = 0.15f;
        PlayerManager.Instance.M_ShowChat();
    }
}
