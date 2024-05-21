Shader "Unlit/CatoonShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Main Tex Color", Color) = (1,1,1,1)
        _BumpMap("NormalMap", 2D) = "bump" {}

        _Outline_Bold("Outline Bold", Range(0, 1)) = 0.1

        _Band_Tex("Band LUT", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        cull front    //! 1Pass는 앞면을 그리지 않는다.
        Pass
        {
            CGPROGRAM
            #pragma vertex _VertexFuc
            #pragma fragment _FragmentFuc
            #include "UnityCG.cginc"

                struct ST_VertexInput    //! 버텍스 쉐이더 Input
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };

                struct ST_VertexOutput    //! 버텍스 쉐이더 Output
                {
                    float4 vertex : SV_POSITION;
                };

                float _Outline_Bold;

                ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
                {
                    ST_VertexOutput stOutput;

                    float3 fNormalized_Normal = normalize(stInput.normal);        //! 로컬 노말 벡터를 정규화 시킴
                    float3 fOutline_Position = stInput.vertex + fNormalized_Normal * (_Outline_Bold * 0.1f); //! 버텍스 좌표에 노말 방향으로 더한다.

                    stOutput.vertex = UnityObjectToClipPos(fOutline_Position);    //! 노말 방향으로 더해진 버텍스 좌표를 카메라 클립 공간으로 변환 
                    return stOutput;
                }


                float4 _FragmentFuc(ST_VertexOutput i) : SV_Target
                {
                    return 0.0f;
                }

            ENDCG
        }

        cull back    //! 2Pass는 뒷면을 그리지 않는다.
        CGPROGRAM

        #pragma surface surf _BandedLighting    //! 커스텀 라이트 사용

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Band_Tex;
            float2 uv_BumpMap;
        };

        struct SurfaceOutputCustom        //! Custom SurfaceOutput 구조체, BandLUT 텍스처를 넣기 위해 만듬
        {
            fixed3 Albedo;
            fixed3 Normal;
            fixed3 Emission;
            half Specular;
            fixed Gloss;
            fixed Alpha;

            float3 BandLUT;
        };

        sampler2D _MainTex;
        sampler2D _Band_Tex;
        sampler2D _BumpMap;

        float4 _Color;

        void surf(Input IN, inout SurfaceOutputCustom o)
        {
            float4 fMainTex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = fMainTex.rgb;
            o.Alpha = 1.0f;

            float4 fBandLUT = tex2D(_Band_Tex, IN.uv_Band_Tex);
            o.BandLUT = fBandLUT.rgb;

            float3 fNormalTex = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Normal = fNormalTex;
        }

        //! 커스텀 라이트 함수
        float4 Lighting_BandedLighting(SurfaceOutputCustom s, float3 lightDir, float3 viewDir, float atten)
        {
            //! BandedDiffuse 조명 처리 연산
            float3 fBandedDiffuse;
            float fNDotL = dot(s.Normal, lightDir) * 0.5f + 0.5f;    //! Half Lambert 공식

            //! 0~1로 이루어진 fNDotL값을 3개의 값으로 고정함 <- Banded Lighting 작업
            //float fBandNum = 3.0f;
            //fBandedDiffuse = ceil(fNDotL * fBandNum) / fBandNum;             

            //! BandLUT 텍스처의 UV 좌표에 0~1로 이루어진 NDotL값을 넣어서 음영 색을 가져온다.
            fBandedDiffuse = tex2D(_Band_Tex, float2(fNDotL, 0.5f)).rgb;



            float3 fSpecularColor;
            float3 fHalfVector = normalize(lightDir + viewDir);
            float fHDotN = saturate(dot(fHalfVector, s.Normal));
            float fPowedHDotN = pow(fHDotN, 500.0f);

            //! smoothstep
            float fSpecularSmooth = smoothstep(0.005, 0.01f, fPowedHDotN);
            fSpecularColor = fSpecularSmooth * 1.0f;



            //! 최종 컬러 출력
            float4 fFinalColor;
            fFinalColor.rgb = ((s.Albedo * _Color) + fSpecularColor) *
                                 fBandedDiffuse * _LightColor0.rgb * atten;
            fFinalColor.a = s.Alpha;

            return fFinalColor;
        }

        ENDCG
    }
}
