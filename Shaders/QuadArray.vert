#version 400
uniform mat4 transform = mat4(1);
uniform mat2 rotation = mat2(1);
uniform vec2 scale = vec2(1, 1);
uniform vec2 translation;
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
layout(location = 2) in vec2 vo;
out vec2 texCoord;

void main()
{
  gl_Position = transform * vec4(vp * rotation * scale + translation + vo, -1.0, 1.0);
  texCoord = vt;
}
