Shader "Effect/Particles/Sword" 
{
	Properties 
	{
        _MainTex ("刀光贴图", 2D) = "white" {}
        _EmissionTex("自发光贴图",2D) = "white"{}
        _EmissionInt("自发光强度",float) = 1
        [HDR]_Color("Color",Color) = (1,1,1,1)
		_NoiseTex ("溶解噪音贴图", 2D) = "gray" {}
        _FlowSpeed("流动速度",float) = 1		
		//_Dissolution ("Dissolution",range(0,1)) = 1    //改为由粒子系统中的customdata控制
	}

    SubShader 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent+10" "PreviewType"="Plane" }

		Blend One OneMinusSrcAlpha
		Cull Off Lighting Off ZWrite Off
	
		LOD 200


        Pass 
		{       
            //Tags {"LightMode" = "ForwardBase"}
            Tags { "RenderType"="Opaque" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_particles           
            #include "UnityCG.cginc"
            
			sampler2D _MainTex;
			float4 _MainTex_ST;
            sampler2D _EmissionTex;
            float _EmissionInt;

			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
	
			float _Dissolution;
            float4 _Color;
            float _FlowSpeed;


            struct appdata
			{
                float4 vertex		: POSITION;
                fixed4 color		: COLOR;
                float4 texcoord		: TEXCOORD0;		//xy放uv,zw放lifetime和自定义数据
                float4 texcoord1	: TEXCOORD1;
            };

            struct v2f 
			{
                float4 vertex		: SV_POSITION;
                fixed4 color		: COLOR;   
				float4 texcoords	: TEXCOORD0;		//xy采样AlphaTex,zw采样NoiseTex
                float4 customData	: TEXCOORD1;        //，y控制溶解，z控制流动
				float4 grabPos		: TEXCOORD3;

            };
                        

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
				o.texcoords.xy = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
				o.texcoords.zw = TRANSFORM_TEX(v.texcoord.zw, _NoiseTex);
				o.grabPos = ComputeNonStereoScreenPos(o.vertex);
				o.customData = v.texcoord1;
                o.texcoords.x += o.customData.x * _FlowSpeed; //uv流动
                o.texcoords.z += o.customData.y;

                return o;
            }

                    
            fixed4 frag (v2f i) : SV_Target
            {               
                half4 color = i.color; //获取顶点色
                //纹理采样
				half4 mainTex = saturate(tex2D(_MainTex, i.texcoords.xy));   //+ i.customData.zz
                half4 emissionTex = tex2D(_EmissionTex,i.texcoords.xy) *2 -1; //采样贴图，并将值域映射到[-1，1]
                half noise = tex2D(_NoiseTex, i.texcoords.zw ).r;
                //消融
                clip(noise- i.customData.y);
                //自发光
                half3 emission = emissionTex.rgb * _EmissionInt; //自发光强度
                //集合
                half3 finalcolor = emission*mainTex.a*color.a + _Color.rgb * color*mainTex.a;

                return float4(finalcolor,mainTex.a);
            }
            ENDCG   
		}
	}   
}
