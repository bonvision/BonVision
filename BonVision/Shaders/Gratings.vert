#version 140
#extension GL_ARB_explicit_attrib_location : require
uniform mat4 transform = mat4(1);
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec2 texCoord;

void main()
{
  gl_Position = transform * vec4(vp, 0.0, 1.0);
  texCoord = vt;
}
