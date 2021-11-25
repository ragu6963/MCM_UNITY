using UnityEngine;

public class MuseumManager : Singleton<MuseumManager>
{
    public bool IsActive { get; set; }
    public bool IsFocusUnity { get; set; }
    public Light ligh1, ligh2;
    private void Awake()
    {
        ligh1.intensity = 1.5f;
        ligh2.intensity = 1f;
        IsActive = true;
    }
    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.captureAllKeyboardInput = false;
#endif
    }
    public void ActiveMuseum()
    {
        IsActive = true;
    }
    public void DeactiveMuseum()
    {
        IsActive = false;
    }
}
