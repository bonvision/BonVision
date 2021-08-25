#version 400
uniform sampler2D tex;
in vec3 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord.xy);
  fragColor = vec4(texel.rgb * texCoord.z, texel.a);
}
