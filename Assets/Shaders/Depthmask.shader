Shader "Prerender/Mask" {

	Subshader {
		
		//Render after regular shader
		
		Tags {"Queue" = "Geometry-2"}
		
		//DO NOT draw to the RGBA channels, only the z buffer
		
		ColorMask 0
		ZWrite On
		
		//DO NOTHING on pass
		
		Pass {}
		
	}

}