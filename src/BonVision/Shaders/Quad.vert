#version 400
uniform mat4 transform = mat4(1);
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec2 texCoord;

void main()
{
  gl_Position = transform * vec4(vp, 0.0, 1.0);
  texCoord = vt;
}
