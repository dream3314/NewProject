Shader "Custom/Hologram2"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Pow ("Power", Range(1, 10)) = 1
        _Speed ("Speed", Range(1, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}

        CGPROGRAM
        #pragma surface surf _Lambert alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        float4 _Color;
        float _Pow;
        float _Speed;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Emission = _Color.rgb;
            float holo = pow(frac(IN.worldPos.g * 3 - _Time.y), 30);
            float ndotv = dot(o.Normal, IN.viewDir);
            float rim = pow(1 - ndotv, _Pow);
            o.Alpha = rim + holo;   
        }

        float4 Lighting_Lambert(SurfaceOutput s , float3 lightDir, float atten)
        {
            return float4(0, 0, 0, s.Alpha);
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
