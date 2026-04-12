Shader "Custom/Caustics_BuiltIn"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CausticColor ("Caustic Color", Color) = (1,1,1,1)
        _StretchDirection ("Stretch Direction", Vector) = (1,0,0,0)
        _DistortionStrength ("Distortion Strength", Float) = 0.05
        _CellDensityA ("Cell Density A", Float) = 4
        _CellDensityB ("Cell Density B", Float) = 5
        _Transparency ("Transparency", Range(0,1)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _CausticColor;
            float2 _StretchDirection;
            float _DistortionStrength;
            float _CellDensityA;
            float _CellDensityB;
            float _Transparency;

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

            float2 hash2(float2 p)
            {
                p = float2(dot(p, float2(127.1, 311.7)),
                           dot(p, float2(269.5, 183.3)));
                return frac(sin(p) * 43758.5453);
            }

            void voronoi(float2 UV, float angleOffset, float cellDensity, out float outDist, out float outCell)
            {
                float2 g = floor(UV * cellDensity);
                float2 f = frac(UV * cellDensity);

                float minDist = 8.0;
                float cellValue = 0;

                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        float2 lattice = float2(x, y);
                        float2 id = g + lattice;
                        float2 offset = hash2(id);

                        float2 r = lattice + offset - f;
                        float d = sqrt(dot(r, r));

                        if (d < minDist)
                        {
                            minDist = d;
                            cellValue = offset.x;
                        }
                    }
                }

                outDist = minDist;
                outCell = cellValue;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float t = _Time.y;

                float2 flow = _StretchDirection * t;

                float2 uv0 = i.uv;
                float2 uv = uv0 + flow;

                float d1, c1;
                float d2, c2;

                voronoi(uv, 2.0, _CellDensityA, d1, c1);
                voronoi(uv, 3.0, _CellDensityB, d2, c2);

                float2 v = float2(d1, d2);
                float2 offset = (v - 0.5) * _DistortionStrength;

                uv0 += offset;

                fixed4 col = tex2D(_MainTex, uv0);

                float caustic = saturate((d1 + d2) * 0.5);

                col.rgb *= _CausticColor.rgb * (0.5 + caustic);

                // -----------------------------
                // TRANSPARENCY CONTROL (NEW)
                col.a *= _Transparency;

                return col;
            }

            ENDCG
        }
    }
}