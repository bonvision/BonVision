#version 400
uniform vec4 color = vec4(1);
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  if (length(2 * texCoord - 1) > 1)
    discard;
  fragColor = color;
}
