precision mediump float;
uniform float time;
uniform float2  mouse;
uniform float2  resolution;

const int   oct = 8;
const float per = 0.5;
const float PI = 3.1415926;
const float cCorners = 1.0 / 16.0;
const float cSides = 1.0 / 8.0;
const float cCenter = 1.0 / 4.0;

// 補間関数
float interpolate(float a, float b, float x) {
    float f = (1.0 - cos(x * PI)) * 0.5;
    return a * (1.0 - f) + b * f;
}

// 乱数生成
float rnd(float2 p) {
    return frac(sin(dot(p, float2(12.9898, 78.233))) * 43758.5453);
}

// 補間乱数
float irnd(vec2 p) {
    float2 i = floor(p);
    float2 f = frac(p);
    float4 v = float4(rnd(float4(i.x, i.y)),
        rnd(float2(i.x + 1.0, i.y)),
        rnd(float2(i.x, i.y + 1.0)),
        rnd(float2(i.x + 1.0, i.y + 1.0)));
    return interpolate(interpolate(v.x, v.y, f.x), interpolate(v.z, v.w, f.x), f.y);
}

// ノイズ生成
float noise(float2 p) {
    float t = 0.0;
    for (int i = 0; i < oct; i++) {
        float freq = pow(2.0, float(i));
        float amp = pow(per, float(oct - i));
        t += irnd(float2(p.x / freq, p.y / freq)) * amp;
    }
    return t;
}

// シームレスノイズ生成
float snoise(float2 p, float2 q, float2 r) {
    return noise(float2(p.x, p.y)) * q.x * q.y +
        noise(float2(p.x, p.y + r.y)) * q.x * (1.0 - q.y) +
        noise(float2(p.x + r.x, p.y)) * (1.0 - q.x) * q.y +
        noise(float2(p.x + r.x, p.y + r.y)) * (1.0 - q.x) * (1.0 - q.y);
}

void main(void) {
    // noise
    float2 t = gl_FragCoord.xy + float2(time * 10.0);
    float n = noise(t);

    // seamless noise
//	const float map = 256.0;
//	vec2 t = mod(gl_FragCoord.xy + vec2(time * 10.0), map);
//	float n = snoise(t, t / map, vec2(map));

    gl_FragColor = float4(float3(n), 1.0);
}