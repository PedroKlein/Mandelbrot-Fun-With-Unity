Shader "Custom/JuliaSet"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Area("Area", vector) = (0,0,4,4)
        _CPos("C Position", vector) = (0,0,0,0)
        _Iteretions ("Iterations", int) = 50
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            int _Iteretions;
            vector _Area;
            vector _CPos;

            float2 complexPow2(float2 a) {
                return float2(a.x*a.x - a.y*a.y, 2*a.x*a.y);
            }

            float genJuliaSet(float2 z) {
                float2 c = float2(_CPos.x, _CPos.y);
                float zdot;
                int i;
                for (i = 0; i < _Iteretions; i++) {
                    z = complexPow2(z) + c;
                    zdot = dot(z, z);
                    if (zdot > (_Iteretions + 1) * (_Iteretions + 1)) {
                        float sl = i - log2(log2(dot(z, z))) + 4.0;
                        float al = smoothstep(-0.1, 0.0, sin(0.5 * 6.2831));
                        return lerp(i, sl, al);
                    }
                }
                return 0;
            }

            float3 mapColor(float l) {
                float3 res = 0.5 + 0.5 * cos(3.0 + l * 0.15 + float3(0.0, 0.6, 1.0));
                return res;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            fixed4 frag(v2f i) : SV_Target
            {
                float2 c = _Area.xy + ((i.uv - 0.5)) * _Area.zw;
                float res = genJuliaSet(c);
                if (res == 0) {
                    return float4(0,0,0, 1);
                }
                float3 col = mapColor(res);
                return float4(col, 1);
            }
            ENDCG
        }
    }
}
