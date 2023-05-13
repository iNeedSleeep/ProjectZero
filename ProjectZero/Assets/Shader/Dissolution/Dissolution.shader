Shader "Dissolution"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("NoiseTex",2D) = "gray" {}
        _Dissolution ("Dissolution",range(0,1.1)) = 1.1         //设定为1.1是因为outline需要减0.1，如果max设置为1则会漏光
        [HDR]_OutlineColor ("OutlineColor" , Color) =(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex; float4 _NoiseTex_ST;
            uniform float _Dissolution;
            uniform float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv1 = TRANSFORM_TEX(v.uv,_NoiseTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                half4 mainTex = tex2D(_MainTex, i.uv);
                half noise = tex2D(_NoiseTex, i.uv1).r;
                half dissolution =step(noise,_Dissolution);     
                half4 outline = (dissolution-step(noise,_Dissolution - 0.1)) * _OutlineColor;  //消融边颜色

                return mainTex*dissolution + outline*mainTex.a;
            }
            ENDCG
        }
    }
}
