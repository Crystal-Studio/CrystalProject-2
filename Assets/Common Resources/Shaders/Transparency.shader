 Shader "Custom/Custom Transparency" {
     Properties {
         _MainTex ("Base (RGB)", 2D) = "white" {}
         _Color("Color",Color) = (1,1,1,1)
     }
     SubShader {
         Tags { "Queue"="Transparent" }
         CGPROGRAM
         #pragma surface surf Lambert alpha
         struct Input {
             float2 uv_MainTex;
         };
         
         uniform sampler2D _MainTex;
         uniform float4 _Color;
         
         void surf (Input IN, inout SurfaceOutput o) {
             float4 tex = tex2D (_MainTex, IN.uv_MainTex);
             
             tex.rgb *= _Color.rgb;
          
              o.Albedo = tex.rgb;
              o.Alpha = tex.a;
         }
         ENDCG
	 }