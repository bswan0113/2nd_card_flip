using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(
    typeof(PixelateRenderer),
    PostProcessEvent.AfterStack,     // 마지막에 픽셀화(배경 카메라라면 더 자연스러움)
    "Custom/Pixelate"
)]
public sealed class Pixelate : PostProcessEffectSettings
{
    // 화면 픽셀 단위 블록 크기
    [Range(1f, 128f)]
    public FloatParameter pixelSize = new FloatParameter { value = 6f };

    // 0~1 블렌드 (트랜지션용)
    [Range(0f, 1f)]
    public FloatParameter blend = new FloatParameter { value = 1f };

    // 해상도에 따라 일정한 블록 개수를 유지하고 싶을 때(선택)
    [Tooltip("화면 높이를 기준으로 블록 개수를 맞춥니다 (0=사용 안 함).")]
    public IntParameter blocksPerScreenHeight = new IntParameter { value = 0 };
}

public sealed class PixelateRenderer : PostProcessEffectRenderer<Pixelate>
{
    static readonly int PID_PixelSize = Shader.PropertyToID("_PixelSize");
    static readonly int PID_Blend     = Shader.PropertyToID("_Blend");

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Pixelate"));

        // 블록 개수 기반 옵션이 있으면 화면 높이로 픽셀 크기 환산
        float px = settings.pixelSize.value;
        if (settings.blocksPerScreenHeight.value > 0)
        {
            float blocks = Mathf.Max(1, settings.blocksPerScreenHeight.value);
            px = Mathf.Max(1f, context.screenHeight / blocks);
        }

        sheet.properties.SetFloat(PID_PixelSize, px);
        sheet.properties.SetFloat(PID_Blend, settings.blend.value);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
