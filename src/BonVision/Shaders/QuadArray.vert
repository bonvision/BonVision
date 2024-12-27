#version 400
uniform mat4 transform = mat4(1);
uniform mat4 projection = mat4(1);
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
layout(location = 2) in vec2 vo;
out vec2 texCoord;

void main()
{
  vec4 position = transform * vec4(vp, 0.0, 1.0);
  gl_Position = projection * vec4(position.xy + vo, 0.0, 1.0);
  texCoord = vt;
}
