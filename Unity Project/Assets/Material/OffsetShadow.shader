
Shader "Godot/Blur"
{
   Properties
   {
      _MainTex ("Texture", 2D) = "white" {}
      _Radius("Radius", FLOAT) = 2
   }
   SubShader
   {
      Tags { "RenderType"="Transparent" }
      LOD 100
        Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency
//        Blend One OneMinusSrcAlpha // Premultiplied transparency
//       Blend One One // Additive
//        Blend OneMinusDstColor One // Soft Additive
//        Blend DstColor Zero // Multiplicative
 //      Blend DstColor SrcColor // 2x Multiplicative
      Pass
      {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         // make fog work
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
            UNITY_FOG_COORDS(1)
            float4 vertex : SV_POSITION;
         };

         sampler2D _MainTex;
         float4 _MainTex_TexelSize;
         float4 _MainTex_ST;
         float _Radius;
         v2f vert (appdata v)
         {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
         }
         
         fixed4 frag (v2f i) : SV_Target
         {
            // sample the texture
            fixed4 col = tex2D(_MainTex, i.uv);
            float2 ps = float2(_MainTex_TexelSize.x, _MainTex_TexelSize.y);
            col+=tex2D(_MainTex,i.uv+float2(0,-_Radius)*ps);
                col+=tex2D(_MainTex,i.uv+float2(0,_Radius)*ps);
                col+=tex2D(_MainTex,i.uv+float2(-_Radius,0)*ps);
                col+=tex2D(_MainTex,i.uv+float2(_Radius,0)*ps);
                col/=5.0;
            // apply fog
            UNITY_APPLY_FOG(i.fogCoord, col);
            return col;
         }
         ENDCG
      }
   }
}