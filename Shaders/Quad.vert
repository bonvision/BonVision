#version 400
uniform mat2 rotation = mat2(1);
uniform vec2 scale = vec2(1, 1);
uniform vec2 shift;
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec2 texCoord;

void main()
{
  gl_Position = vec4(vp * scale * rotation + shift, 0.0, 1.0);
  texCoord = vt;
}
