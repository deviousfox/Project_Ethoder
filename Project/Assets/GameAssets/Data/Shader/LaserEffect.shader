// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LaserEffect"
{
	Properties
	{
		_LaserGradient("LaserGradient", 2D) = "white" {}
		_WaveSpeed("WaveSpeed", Vector) = (0.1,0.1,0,0)
		_NoiseScale("NoiseScale", Vector) = (10,10,0,0)
		_LaserColor("LaserColor", Color) = (1,0,0,0.3803922)
		_EmissionIntensity("EmissionIntensity", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _LaserColor;
		uniform sampler2D _LaserGradient;
		uniform float4 _LaserGradient_ST;
		uniform float2 _NoiseScale;
		uniform float2 _WaveSpeed;
		uniform float _EmissionIntensity;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_LaserGradient = i.uv_texcoord * _LaserGradient_ST.xy + _LaserGradient_ST.zw;
			float2 uv_TexCoord4 = i.uv_texcoord * _NoiseScale + ( _WaveSpeed * _Time.y );
			float simplePerlin2D18 = snoise( uv_TexCoord4 );
			simplePerlin2D18 = simplePerlin2D18*0.5 + 0.5;
			float4 temp_output_5_0 = ( tex2D( _LaserGradient, uv_LaserGradient ) * simplePerlin2D18 * _LaserColor.a );
			o.Emission = ( _LaserColor * temp_output_5_0 * _EmissionIntensity ).rgb;
			o.Alpha = temp_output_5_0.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
389;431;1259;689;712.1389;532.0305;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;13;-657.139,-71.03052;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;10;-810.139,-315.0305;Inherit;False;Property;_WaveSpeed;WaveSpeed;1;0;Create;True;0;0;False;0;False;0.1,0.1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-567.139,-214.0305;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;15;-538.139,-344.0305;Inherit;False;Property;_NoiseScale;NoiseScale;2;0;Create;True;0;0;False;0;False;10,10;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-417,-266;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-162,-275;Inherit;True;Property;_LaserGradient;LaserGradient;0;0;Create;True;0;0;False;0;False;-1;c9274606b3eb5de48993cb0031cfcce6;c9274606b3eb5de48993cb0031cfcce6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;18;-100.139,-51.03049;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-154.139,-468.0305;Inherit;False;Property;_LaserColor;LaserColor;3;0;Create;True;0;0;False;0;False;1,0,0,0.3803922;1,0,0,0.3803922;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;235,-201;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-290.1389,-451.0305;Inherit;False;Property;_EmissionIntensity;EmissionIntensity;4;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;273.861,-358.0305;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;16;500,-155;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;LaserEffect;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;10;0
WireConnection;14;1;13;0
WireConnection;4;0;15;0
WireConnection;4;1;14;0
WireConnection;18;0;4;0
WireConnection;5;0;2;0
WireConnection;5;1;18;0
WireConnection;5;2;6;4
WireConnection;17;0;6;0
WireConnection;17;1;5;0
WireConnection;17;2;19;0
WireConnection;16;2;17;0
WireConnection;16;9;5;0
ASEEND*/
//CHKSM=4C439C49A84DB4A1B77B73E1E254C63C57911AB1