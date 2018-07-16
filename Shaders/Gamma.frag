#version 400
uniform sampler2D tex;
uniform sampler2D lut;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord);
  vec4 weight = texture(lut, texCoord);
  fragColor = texel * weight;
}
