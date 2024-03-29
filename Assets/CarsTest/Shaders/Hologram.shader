﻿Shader "Custom/FlatTransparent" {
     Properties {
         _Color("Main Color", Color) = (1,1,1,1)
         _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
     }
 
     SubShader {
         Tags{ "Queue" = "Transparent" "IgnoreProjector" = "False" "RenderType" = "Transparent" }
 
         Pass {
             ColorMask A
             Blend SrcAlpha OneMinusSrcAlpha
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
 
             fixed4 _Color;
 
             float4 vert(float4 vertex : POSITION) : SV_POSITION {
                 return UnityObjectToClipPos(vertex);
             }
 
             fixed4 frag() : SV_Target {
                 return _Color;
             }
 
             ENDCG
         }
 
         Pass {
             ColorMask RGB
             Blend SrcAlpha OneMinusSrcAlpha
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
 
             sampler2D _MainTex;
             fixed4 _Color;
 
             struct appdata {
                 float4 vertex : POSITION;
                 float2 uv : TEXCOORD0;
             };
 
             struct v2f {
                 float2 uv : TEXCOORD0;
                 float4 vertex : SV_POSITION;
             };
 
             v2f vert(appdata v) {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.uv = v.uv;
                 return o;
             }
 
             fixed4 frag(v2f i) : SV_Target{
                 fixed4 col = _Color * tex2D(_MainTex, i.uv);
                 col.a = 0.2f;
                 return col;
             }
             ENDCG
         }
     }
 
     Fallback "Diffuse"
 }
 