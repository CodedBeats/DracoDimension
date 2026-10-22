#ifndef TOON_OUTLINE_INCLUDED
#define TOON_OUTLINE_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
#endif

// Called from a Custom Function node (File mode, name "ToonOutline").
// Compares scene depth at four diagonal neighbours (Roberts cross).
// Edge is 1 where depth jumps by more than Threshold, 0 elsewhere.
void ToonOutline_float(float2 UV, float Thickness, float Threshold, out float Edge)
{
#ifdef SHADERGRAPH_PREVIEW
    Edge = 0.0;
#else
    float2 texel = Thickness / _ScreenParams.xy;

    float d0 = LinearEyeDepth(SampleSceneDepth(UV + texel * float2(-1.0,  1.0)), _ZBufferParams);
    float d1 = LinearEyeDepth(SampleSceneDepth(UV + texel * float2( 1.0, -1.0)), _ZBufferParams);
    float d2 = LinearEyeDepth(SampleSceneDepth(UV + texel * float2(-1.0, -1.0)), _ZBufferParams);
    float d3 = LinearEyeDepth(SampleSceneDepth(UV + texel * float2( 1.0,  1.0)), _ZBufferParams);

    float diff = sqrt(pow(d0 - d1, 2.0) + pow(d2 - d3, 2.0));
    Edge = step(Threshold, diff);
#endif
}

#endif
