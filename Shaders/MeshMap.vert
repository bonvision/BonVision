#version 400
uniform mat4 modelview = mat4(1);
uniform mat4 projection = mat4(1);
layout(location = 0) in vec3 vp;
layout(location = 1) in vec3 vt;
out vec3 texCoord;

void main()
{
  gl_Position = projection * modelview * vec4(vp, 1.0);
  texCoord = vt;
}
