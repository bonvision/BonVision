#version 400
uniform vec4 color;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  float scale = length(2 * texCoord - 1) > 1 ? 0 : 1;
  fragColor = scale * color;
}
