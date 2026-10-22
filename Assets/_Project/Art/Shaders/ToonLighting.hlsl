#ifndef TOON_LIGHTING_INCLUDED
#define TOON_LIGHTING_INCLUDED

// Called from a Custom Function node (File mode, name "ToonMainLight").
// Gives the graph the main directional light's direction and colour.
void ToonMainLight_float(out float3 Direction, out float3 Color)
{
#ifdef SHADERGRAPH_PREVIEW
    Direction = normalize(float3(0.5, 0.5, 0.0));
    Color = float3(1.0, 1.0, 1.0);
#else
    Light mainLight = GetMainLight();
    Direction = mainLight.direction;
    Color = mainLight.color;
#endif
}

#endif
