#version 400
uniform mat4 transform = mat4(1);
uniform mat4 rotation = mat4(1);
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec2 texCoord;

void main()
{
  mat2 texRotation = mat2(rotation[0][0], rotation[1][0], rotation[0][1], rotation[1][1]);
  gl_Position = transform * vec4(vp, -1.0, 1.0);
  texCoord = texRotation * (vt - 0.5) + 0.5;
}
