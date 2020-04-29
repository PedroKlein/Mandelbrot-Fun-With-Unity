Shader "Custom/Mandelbrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Area("Area", vector) = (0,0,4,4)
        _ZPos("C Position", vector) = (0,0,0,0)
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
            vector _ZPos;

            float2 ComplexPow2(float2 a) {
                return float2(a.x*a.x - a.y*a.y, 2*a.x*a.y);
            }

            float3 GenMandelbrot(float2 xy) {
                float2 z = float2(_ZPos.x, _ZPos.y);
                for (int i = 0; i < _Iteretions; i++) {
                    z = ComplexPow2(z) + xy;
                    float zdot = dot(z,z);
                    if (zdot > 100000.0){
                        float sl = float(i) - log2(log2(zdot));
			            return sl/float(_Iteretions);
                    }                
                }
                return 0.0; // in set
            }

            float4 mapColor(float mcol) {
                return float4(0.5 + 0.5 * cos(2.7 + mcol * 30.0 + float3(0.0, .6, 1.0)), 1.0);
            }

            /*
            float2 Zoom(float scrollValue, float2 uv) {
                float2 mouse = float2(_xMousePos, _yMousePos);
                mouse = mouse != 0 ? (mouse - 0.5) * 4: 0;
                //mouse /= scrollValue;

                uv /= scrollValue;
                uv += mouse;

                float2 delta = mouse !=0 ? (mouse - uv): 0;
                return uv - delta;
            }
            */
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
                fixed4 col = float4(GenMandelbrot(c), 0);
                return mapColor(col);
            }
            ENDCG
        }
    }
}
