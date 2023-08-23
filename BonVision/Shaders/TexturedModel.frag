#version 400
uniform vec2 shift;
uniform vec2 scale = vec2(1, 1);
uniform vec4 colorAmbient;
uniform vec4 colorDiffuse;
uniform vec4 colorSpecular;
uniform float shininess = 1.0;
uniform sampler2D textureDiffuse;
uniform vec3 light;
in vec3 position;
in vec2 texCoord;
in vec3 normal;
out vec4 fragColor;

void main()
{
  vec3 L = normalize(light - position);
  vec3 R = normalize(-reflect(L, normal));
  vec3 V = normalize(-position);
  vec4 texel = texture(textureDiffuse, texCoord * scale + shift);

  vec4 Iamb = colorAmbient * texel;
  vec4 Idiff = colorDiffuse * vec4(texel.rgb * max(dot(normal, L), 0.0), texel.a);
  vec4 Ispec = vec4(colorSpecular.rgb * pow(max(dot(R, V), 0.0), shininess), colorSpecular.a * texel.a);

  fragColor = Iamb + Idiff + Ispec;
}