using UnityEngine;

public class AnimationToSound : MonoBehaviour
{
    public void AnimationPlaySound(string name)
    {
        SoundManager.Instance.PlaySound(name);
    }
}
