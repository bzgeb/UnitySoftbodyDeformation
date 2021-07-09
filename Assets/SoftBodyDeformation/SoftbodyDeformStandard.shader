Shader "Custom/SoftbodyDeformStandard"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert fullforwardshadows addshadow

        #include "SoftbodyDeformationCommon.cginc"

        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void vert(inout appdata_full v, out Input data)
        {
            UNITY_INITIALIZE_OUTPUT(Input, data);
            
            float4 vertexPositionWS = mul(unity_ObjectToWorld, v.vertex);
            float3 manipulatedPositionWS = ApplyManipulator(vertexPositionWS, _TransformationMatrix, _AnchorPosition, _Radius, _Hardness);
            v.vertex = mul(unity_WorldToObject, float4(manipulatedPositionWS, 1));

            float3 tangentWS = UnityObjectToWorldDir(v.tangent);
            float3 manipulatedTangentWS = ApplyManipulator(vertexPositionWS + tangentWS * 0.01, _TransformationMatrix, _AnchorPosition, _Radius, _Hardness);
            float3 finalTangent = normalize(manipulatedTangentWS - manipulatedPositionWS);
            v.tangent = float4(UnityWorldToObjectDir(finalTangent), v.tangent.w);

            float3 binormal = cross(normalize(v.normal), normalize(v.tangent.xyz)) * v.tangent.w;
            float3 binormalWS = UnityObjectToWorldDir(binormal);
            float3 manipulatedBinormalWS = ApplyManipulator(vertexPositionWS + binormalWS * 0.01, _TransformationMatrix, _AnchorPosition, _Radius, _Hardness);
            float3 finalBinormal = normalize(manipulatedBinormalWS - manipulatedPositionWS);
            float3 finalNormal = normalize(cross(finalTangent, finalBinormal)) * v.tangent.w;
            v.normal = UnityWorldToObjectDir(finalNormal);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
