using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera virtualCamera;

    [HideInInspector]
    public CinemachineBasicMultiChannelPerlin channelPerlin;


    private void Awake()
    {
        channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public void ShakeCamera(float intesity, float time)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => channelPerlin.m_AmplitudeGain = intesity)
            .AppendInterval(time)
            .AppendCallback(() => DOVirtual.Float(intesity, 0, .1f, (value) => channelPerlin.m_AmplitudeGain = value));
    }
}
