Shader "Unlit/Hologram 2"
{
    Properties
    {
        //Base
        _Color("Color", Color) = (0, 1, 1, 1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _AlphaTexture("Alpha Mask (R)", 2D) = "white" {}

        //Alpha Mask
        _Scale("Alpha Tiling", Float) = 3
        _ScrollSpeed("Alpha Scroll Speed", Range(0, 5.0)) = 1.0
        
        //Glow
        _GlowIntensity("Glow Intensity", Range(0.01, 1.0)) = 0.5

        //Glitch
        _GlitchSpeed("Glitch Speed", Range(0, 50)) = 50.0
        _GlitchIntensity("Glitch Intensity", Range(0.0, 0.1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType"="Transparent" }
        Lighting Off
        ZWrite On
        Blend SrcAlpha One
        Cull Back

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;        
                float3 grabPos : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float worldNormal : NORMAL;
            };

            fixed4 _Color;
            fixed4 _MainTex_ST;
            sampler2D _MainTex;
            sampler2D _AlphaTexture;
            half _Scale;
            half _ScrollSpeed;
            half _GlowIntensity;
            half _GlitchSpeed;
            half _GlitchIntensity;

            v2f vert (appdata v)
            {
                v2f o;

                v.vertex.z += sin(_Time.y * _GlitchSpeed * 5 * v.vertex.y) * _GlitchIntensity;

                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.grabPos = UnityObjectToViewPos(v.vertex);

                o.grabPos.y += _Time * _ScrollSpeed;

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(UnityWorldSpaceViewDir(o.grabPos.xyz));

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half dirVertex = (dot(i.grabPos, 1.0) + 1) / 2;
                
                fixed4 alphaColor = tex2D(_AlphaTexture, i.grabPos.xy * _Scale);
                fixed4 pixelColor = tex2D(_MainTex, i.uv);
                pixelColor.w = alphaColor.w;

                half rim = 1.0 - saturate(dot(i.viewDir, i.worldNormal));

                return pixelColor * _Color * (rim + _GlowIntensity);
            }
            ENDCG
        }
    }
}
