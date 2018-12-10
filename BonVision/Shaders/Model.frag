#version 400
uniform vec3 Ka;
uniform vec3 Kd;
uniform vec3 Ks;
uniform float Ns = 1.0;
uniform vec3 light;
in vec3 position;
in vec3 normal;
out vec4 fragColor;

void main()
{
  vec3 L = normalize(light - position);
  vec3 R = normalize(-reflect(L, normal));
  vec3 V = normalize(-position);

  vec3 Iamb = Ka;
  vec3 Idiff = Kd * max(dot(normal, L), 0.0);
  vec3 Ispec = Ks * pow(max(dot(R, V), 0.0), Ns);

  fragColor = vec4(Iamb + Idiff + Ispec, 1.0);
}
