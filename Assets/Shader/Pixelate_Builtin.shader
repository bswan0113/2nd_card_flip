Shader "Hidden/Custom/Pixelate_Builtin"
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
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;   // (1/width, 1/height, width, height)
            float  _PixelSize;
            float  _Blend;

            fixed4 frag (v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float px = max(_PixelSize, 1.0);
                float2 stepUV = px * _MainTex_TexelSize.xy;

                float2 snapped = floor(i.uv / stepUV) * stepUV + stepUV * 0.5;
                snapped = saturate(snapped);

                fixed4 pix = tex2D(_MainTex, snapped);
                return lerp(col, pix, saturate(_Blend));
            }
            ENDCG
        }
    }
    Fallback Off
}

