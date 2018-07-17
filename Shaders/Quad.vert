#version 400
uniform float aspectRatio = 1;
uniform mat2 rotation = mat2(1);
uniform vec2 scale = vec2(1, 1);
uniform vec2 translation;
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec2 texCoord;

void main()
{
  vec2 ratioScale = vec2(scale.x / aspectRatio, scale.y);
  gl_Position = vec4(vp * rotation * ratioScale + translation, 0.0, 1.0);
  texCoord = vt;
}
