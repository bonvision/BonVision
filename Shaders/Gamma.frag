#version 400
uniform sampler2D tex;
uniform sampler2D lut;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord);
  texel.r = texture(lut, vec2(texel.r, 0.5)).r;
  texel.g = texture(lut, vec2(texel.g, 0.5)).g;
  texel.b = texture(lut, vec2(texel.b, 0.5)).b;
  fragColor = texel;
}
