// Standard defines
#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#endif

// Define the input structure
struct VertexShaderInput {
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
};

// Define the output structure
struct VertexShaderOutput {
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
};

// Define the parameters
float4x4 World : WORLD;
float4x4 View : VIEW;
float4x4 Projection : PROJECTION;
float4 AmbientColor : AMBIENT_COLOR;
float3 LightDirection : LIGHT_DIRECTION;
float4 DiffuseColor : DIFFUSE_COLOR;
float4 Color;

// Function to compute the inverse of a 3x3 matrix
float3x3 InverseTranspose(float3x3 m)
{
    float det = dot(cross(m[0], m[1]), m[2]);
    return float3x3(
        cross(m[1], m[2]) / det,
        cross(m[2], m[0]) / det,
        cross(m[0], m[1]) / det
    );
}

// Vertex shader function
VertexShaderOutput SimpleVertexShader(VertexShaderInput input)
{
    VertexShaderOutput output;

    

    // Transform vertex position
    output.Position = mul(input.Position, World);
    output.Position = mul(output.Position, View);
    output.Position = mul(output.Position, Projection);

    // Transform normal (with the transpose of the inverse of the world matrix)
    float3x3 worldInverseTranspose = InverseTranspose((float3x3)World);
    float3 transformedNormal = normalize(mul(input.Normal, worldInverseTranspose));

    // Calculate light intensity
    float3 lightDir = normalize(LightDirection);
    float lightIntensity = saturate(dot(transformedNormal, lightDir));
    // Set vertex color
    output.Color = AmbientColor + DiffuseColor * lightIntensity;

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    return input.Color;
}

// Technique definition
technique SimpleShaderTechnique
{
    pass Pass1
    {
        // Set the vertex shader
        VertexShader = compile VS_SHADERMODEL SimpleVertexShader();
    // No pixel shader needed, set to null
    PixelShader = compile PS_SHADERMODEL MainPS();
    }
};