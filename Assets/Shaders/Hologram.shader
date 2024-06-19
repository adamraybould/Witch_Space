// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "Black" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _RimIntensity ("Rim Intensity", Float) = 0
        _ScanSpeed("Scanning Speed", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend One One
        ZTest Off

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
                float2 uv : TEXCOORD0;            
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            float4 _Color;
            float _RimIntensity;
            sampler2D _MainTex;
            float _ScanSpeed;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);     
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 transit = tex2D(_MainTex, i.uv);

                float ndotv = 1 - dot(i.normal, i.viewDir) * _RimIntensity;
                return fixed4(ndotv, ndotv, ndotv, 1) * _Color;
            }
            ENDCG
        }
    }
}
