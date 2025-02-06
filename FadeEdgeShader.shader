Shader "Unlit/FadeEdgeShader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)  // Black center color
        _FadeStart ("Fade Start", Range(0,1)) = 0.3  // Inner solid area
        _FadeEnd ("Fade End", Range(0,1)) = 0.9  // Where fading starts
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back  // Hide back faces for optimization

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 viewNormal : TEXCOORD0;
            };

            float4 _Color;
            float _FadeStart;
            float _FadeEnd;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                
                // Transform normal to view space
                o.viewNormal = mul((float3x3)UNITY_MATRIX_MV, v.normal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the fade effect using the dot product of the normal and view direction
                float fadeFactor = smoothstep(_FadeStart, _FadeEnd, abs(i.viewNormal.z));

                return float4(_Color.rgb, fadeFactor); // Center solid, edges fade
            }
            ENDCG
        }
    }
}
