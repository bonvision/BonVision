#version 140
uniform vec2 frequency = vec2(1,1);
uniform int phase;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  uint bitH = uint(texCoord.x * frequency.x + float(phase)) % 2u;
  uint bitV = uint(texCoord.y * frequency.y) % 2u;
  bitH = bitV != 0u ? (bitH + 1u) % 2u : bitH;
  float color = bitH != 0u ? 1.0 : 0.0;
  fragColor = vec4(color, color, color, 1);
}
