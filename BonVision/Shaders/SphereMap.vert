#version 400
uniform mat4 transform = mat4(1);
uniform vec2 scale = vec2(1, 1);
uniform vec2 shift;
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec4 texCoord;

void main()
{
  vec2 v = 0.5 * vp * scale + shift;
  texCoord = transform * vec4(v, 0.0, 1.0);
  gl_Position = vec4(vp, 0.0, 1.0);
}
