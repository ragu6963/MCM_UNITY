using Cinemachine;
using System;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Camera MainCamera;
    public CinemachineBrain mainChine;
    public ExhibitCam ExhibitCam;
    public CinemachineVirtualCamera PlayerCamera;
    // for tetris
    public Camera TetrisCamera;
    public Camera XYCamera;

    public void InitPlayerVirtualCamera(Transform transform)
    {
        PlayerCamera.Follow = transform;
        PlayerCamera.LookAt = transform;
    }
    public void ActivateExhibitCam()
    {
        ExhibitCam.Activate();
    }

    public void DeactivateExhibitCam()
    {
        ExhibitCam.Deactivate();
    }
    public void SetExhibitFreeLockCam(Exhibit exhibit)
    {
        ExhibitCam.SetExhibitCam(exhibit);
    }
    public void ActivateXYCamera()
    {
        MainCamera.enabled = false;
        XYCamera.enabled = true;
    }
    public void DeactivateXYCamera()
    {
        MainCamera.enabled = true;
        XYCamera.enabled = false;
    }

    public void HidePlayerLayer()
    {
        MainCamera.cullingMask = MainCamera.cullingMask & ~(1 << LayerMask.NameToLayer("Player"));
    }
    public void HideGlassLayer()
    {
        MainCamera.cullingMask = MainCamera.cullingMask & ~(1 << LayerMask.NameToLayer("Glass"));
    }
    public void ShowPlayerLayer()
    {
        MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Player");
    }
    public void ShowGlassLayer()
    {
        MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Glass");
    }

    internal void ShowWallLayer()
    {
        MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Wall");

    }

    internal void HideWallLayer()
    {
        MainCamera.cullingMask = MainCamera.cullingMask & ~(1 << LayerMask.NameToLayer("Wall"));
    }
}
