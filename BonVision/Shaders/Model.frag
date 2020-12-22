#version 400
uniform vec4 colorAmbient;
uniform vec4 colorDiffuse;
uniform vec4 colorSpecular;
uniform float shininess = 1.0;
uniform vec3 light;
in vec3 position;
in vec3 normal;
out vec4 fragColor;

void main()
{
  vec3 L = normalize(light - position);
  vec3 R = normalize(-reflect(L, normal));
  vec3 V = normalize(-position);

  vec4 Iamb = colorAmbient;
  vec4 Idiff = vec4(colorDiffuse.rgb * max(dot(normal, L), 0.0), colorDiffuse.a);
  vec4 Ispec = colorSpecular * pow(max(dot(R, V), 0.0), shininess);

  fragColor = Iamb + Idiff + Ispec;
}
