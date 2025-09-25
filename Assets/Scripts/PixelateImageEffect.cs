using UnityEngine;

[RequireComponent(typeof(Camera))]
[ImageEffectAllowedInSceneView] // Scene 뷰에서도 보고 싶으면 유지
public class PixelateImageEffect : MonoBehaviour
{
    [Range(1, 128)] public float pixelSize = 8f; // 큰 값일수록 큼직한 모자이크
    [Range(0f, 1f)] public float blend = 1f;     // 1=완전 모자이크, 0=원본
    [Tooltip("화면 높이를 블록 N개로 고정(0=미사용)")]
    public int blocksPerScreenHeight = 0;

    Material _mat;
    static readonly int PID_PixelSize = Shader.PropertyToID("_PixelSize");
    static readonly int PID_Blend     = Shader.PropertyToID("_Blend");

    void OnEnable()
    {
        var shader = Shader.Find("Hidden/Custom/Pixelate_Builtin");
        if (shader == null) { enabled = false; return; }
        _mat = new Material(shader) { hideFlags = HideFlags.DontSave };
    }

    void OnDisable()
    {
        if (_mat) DestroyImmediate(_mat);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (_mat == null) { Graphics.Blit(src, dst); return; }

        float px = pixelSize;
        if (blocksPerScreenHeight > 0)
            px = Mathf.Max(1f, (float)src.height / blocksPerScreenHeight);

        _mat.SetFloat(PID_PixelSize, px);
        _mat.SetFloat(PID_Blend,     blend);

        Graphics.Blit(src, dst, _mat, 0);
    }
}
