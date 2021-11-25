using UnityEngine;

public class Video : Exhibit
{
    private void OnTriggerEnter(Collider other)
    {
        //CanvasManager.Instance.VideoCanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        //VideoManager.Instance.VideoStop();
        //CanvasManager.Instance.VideoCanvas.SetActive(false);
    }
}
