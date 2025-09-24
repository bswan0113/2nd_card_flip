Shader "Hidden/Custom/Pixelate"
{
    Properties
    {
        _PixelSize ("Pixel Size (screen px)", Float) = 8
        _Blend     ("Blend", Range(0,1)) = 1
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma target 3.0
            #pragma vertex   VertDefault
            #pragma fragment Frag

            // PPv2 표준 라이브러리 (패키지 경로)
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

            // PPv2 표준 텍스처 매크로
            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
            float4 _MainTex_TexelSize;   // (1/width, 1/height, width, height)
            float  _PixelSize;
            float  _Blend;

            float4 Frag (VaryingsDefault i) : SV_Target
            {
                float2 uv  = i.texcoord;
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                // 픽셀 블록 크기(해상도 무관)
                float px = max(_PixelSize, 1.0);
                float2 stepUV = px * _MainTex_TexelSize.xy;

                // 블록 중앙으로 스냅
                float2 snapped = floor(uv / stepUV) * stepUV + stepUV * 0.5;
                snapped = saturate(snapped); // UV 안전

                float4 pix = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, snapped);

                return lerp(col, pix, saturate(_Blend));
            }
            ENDHLSL
        }
    }

    Fallback Off
}
