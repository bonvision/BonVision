#version 400
uniform vec2 shift;
uniform sampler2D tex;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord + shift);
  fragColor = texel;
}
