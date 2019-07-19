Shader "IndentSurface/IndentStamp_CUSTOM" {
	
	Properties {
		_Opacity("Opacity", Range(0,1)) = 1
	}

		SubShader{
			Tags {
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
			}
			Lighting Off Cull Off ZTest Always ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "IndentStampInc.cginc"

				float _Opacity;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 stamp = tex2D(_MainTex, i.texcoord);
				fixed4 surface = tex2D(_SurfaceTex, i.texcoord1);
				return fixed4(surface.rgb + stamp.rgb* stamp.a, surface.a + stamp.a*_Opacity);
			}
			ENDCG
		}
	}
}
