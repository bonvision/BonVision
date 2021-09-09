#version 400
uniform mat4 modelview;
uniform mat4 projection;
uniform mat4 normalMatrix;
layout(location = 0) in vec3 vp;
layout(location = 1) in vec3 vn;
layout(location = 2) in vec3 mr0;
layout(location = 3) in vec3 mr1;
layout(location = 4) in vec3 mr2;
layout(location = 5) in vec3 mt;
out vec3 position;
out vec3 normal;

void main()
{
  mat3 mr = mat3(mr0, mr1, mr2);
  vec4 v = modelview * vec4(mr * vp + mt, 1.0);
  gl_Position = projection * v;
  position = vec3(v);
  normal = normalize(vec3(normalMatrix * vec4(mr * vn, 0.0)));
}
