#version 400
const float pi = 3.1415926535897932384626433832795;
const float sqrtTwoPi = sqrt(2 * pi);
uniform int square = 0;
uniform float radius = 1;
uniform float edge = 0;
uniform float frequency = 1;
uniform float contrast = 1;
uniform float phase = 0;
uniform float opacity = 1;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  float value = sin((texCoord.x + phase) * 2 * pi * frequency);  // sinewave
  if (square != 0) value = value > 0 ? 1 : -1; // square modulation

  float envelope;
  float dist = length(texCoord * 2 - 1) / radius; // distance to the edge
  if (edge == 0) envelope = dist < 1 ? 1 : 0; // square envelope
  else
  {
    // gaussian envelope
    dist = dist / edge;
    envelope = 1. / (edge * sqrtTwoPi) * exp(-0.5 * dist * dist);
  }
  value = value * contrast * envelope * 0.5 + 0.5; // contrast modulation
  fragColor = vec4(value, value, value, opacity * envelope);
}
