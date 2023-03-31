Decals uses UV1.
Decal UV set by using standard shader property "UV Set for secondary textures".
Modifications that was done to standard shader in "Standard Car Decals" shader:
- Added "_DecalTex" property
- #include "UnityStandardCore.cginc" replaced with #include "Custom_UnityStandardCore.cginc"

Modifications that was done to Custom_UnityStandardInput.cginc:
- Added line "sampler2D   _DecalTex";
- Added line "float4      _DecalTex_ST"; 
- In "Albedo" functions added sampling of decal color and blending with main color using decal alpha channel:
  half4 decal = tex2D(_DecalTex, texcoords.zw);
  albedo = lerp(albedo, decal.rgb, decal.a);