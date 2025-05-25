#version 300 es
precision highp float;

uniform sampler2D posMap;
uniform sampler2D densityMap;

in vec2 vUv;
out vec4 fragColor;

vec3 hash(vec3 v) {
  uvec3 x = floatBitsToUint(v + vec3(.1, .2, .3));
  x = (x >> 8 ^ x.yzx) * 0x456789ABu;
  x = (x >> 8 ^ x.yzx) * 0x6789AB45u;
  x = (x >> 8 ^ x.yzx) * 0x89AB4567u;
  return vec3(x) / vec3(-1u);
}

void main() {
  vec2 vel = texture(posMap, vUv).zw;

  float a = smoothstep(0.5, 0.45, distance(gl_PointCoord, vec2(0.5)));
  fragColor += a;

  if (hash(vec3(vUv, 0.1)).x < 0.02) {
    fragColor.gb *= 0.;
  }
}
