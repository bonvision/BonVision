#version 400
layout(location = 0) in vec3 vp;
layout(location = 1) in vec3 vt;
out vec3 texCoord;

void main()
{
  gl_Position = vec4(vp, 1.0);
  texCoord = vt;
}
