#version 400
uniform mat4 modelview;
uniform mat4 projection;
uniform mat4 normalMatrix;
layout(location = 0) in vec3 vp;
layout(location = 1) in vec3 vn;
out vec3 position;
out vec3 normal;

void main()
{
  vec4 v = modelview * vec4(vp, 1.0);
  gl_Position = projection * v;
  position = vec3(v);
  normal = normalize(vec3(normalMatrix * vec4(vn, 0.0)));
}
