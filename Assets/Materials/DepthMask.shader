Shader "Custom/DepthMask"
{
  SubShader 
  {
    Tags {"RenderType"="Opaque" "Queue"="Geometry+10" }

    ColorMask 0
    ZWrite On

    Pass{}
  }
}