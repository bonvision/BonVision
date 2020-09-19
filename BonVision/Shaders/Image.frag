#version 400
uniform vec2 shift;
uniform vec2 scale = vec2(1, 1);
uniform sampler2D tex;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord * scale + shift);
  fragColor = texel;
}
