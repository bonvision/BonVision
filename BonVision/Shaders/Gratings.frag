#version 400
const float pi = 3.1415926535897932384626433832795;
const float sqrtTwoPi = sqrt(2 * pi);
uniform vec2 frequency;
uniform float radius = 1;
uniform float aperture = 0;
uniform float contrast = 1;
uniform float phase = 0;
uniform float opacity = 1;
uniform float threshold = -1;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  float value = (texCoord.x - 0.5) * frequency.x + (texCoord.y - 0.5) * frequency.y + phase;
  if (threshold < 0) value = sin(value * 2 * pi); // sinewave
  else value = mod(value, 1) > threshold ? -1 : 1; // square modulation

  float envelope;
  float dist = length(texCoord * 2 - 1) / radius; // distance to the edge
  if (aperture == 0) envelope = dist < 1 ? 1 : 0; // square envelope
  else
  {
    // gaussian envelope
    dist = dist / aperture;
    envelope = exp(-0.5 * dist * dist);
  }
  value = value * contrast * envelope * 0.5 + 0.5; // contrast modulation
  fragColor = vec4(value, value, value, opacity * envelope);
}
