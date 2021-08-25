#version 400
uniform samplerCube tex;
uniform sampler2D viewMap;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 vector = texture(viewMap, texCoord);
  vec4 texel = texture(tex, vector.xyz);
  fragColor = vec4(texel.rgb * vector.a, texel.a);
}
