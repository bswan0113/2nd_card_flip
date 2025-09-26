using UnityEngine;
public class HiddenBG_OnRelay : MonoBehaviour
{
    public Animator pixelateAnimator;   // GlobalVolume의 Animator 드래그
    public string openTrigger = "Open";
    public bool playOnFirstEnable = true;
    bool first = true;

    void OnEnable()
    {
        if (!pixelateAnimator) return;
        if (first) { if (playOnFirstEnable) pixelateAnimator.SetTrigger(openTrigger); first = false; }
        else pixelateAnimator.SetTrigger(openTrigger);
    }
}
