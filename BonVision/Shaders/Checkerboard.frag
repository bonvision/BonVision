#version 400
uniform vec2 frequency = vec2(1,1);
uniform int phase;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  float bitH = uint(texCoord.x * frequency.x + phase) % 2;
  float bitV = uint(texCoord.y * frequency.y) % 2;
  bitH = bitV != 0 ? uint(bitH + 1) % 2 : bitH;
  float color = bitH != 0 ? 1 : 0;
  fragColor = vec4(color, color, color, 1);
}
