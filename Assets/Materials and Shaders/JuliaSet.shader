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

            #define B 32.

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

            float2 ComplexPow2(float2 a) {
                return float2(a.x*a.x - a.y*a.y, 2*a.x*a.y);
            }

            float3 pal(in float t, in float3 a, in float3 b, in float3 c, in float3 d) {
                return a + b * cos(6.28318 * (c * t + d));
            }

            float GenJuliaSet(float2 z) {
                float2 c = float2(_CPos.x, _CPos.y);
                float zdot;
                int i;
                for (i = 0; i < _Iteretions; i++) {
                    z = ComplexPow2(z) + c;
                    zdot = dot(z, z);
                    if (zdot > B*B)
                        break;
                }
                float sl = float(i) - log(log(zdot) / log(B)) / log(2.);
                return sl / float(_Iteretions);
            }

            

            float4 mapColor(float mcol) {
                return float4(0.5 + 0.5 * cos(0.7 + mcol * 1.0 + float3(0.0, .6, .2)), 1.0);
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
                float res = GenJuliaSet(c);
                float3 col = pal(frac(2. * res + 0.5), float3(.5,.5,.5), float3(.5, .5, .5), float3(1, 1, 1), float3(.0, .10, .2));
                return float4(col, 1);
            }
            ENDCG
        }
    }
}
