Shader "Unlit/BlackholeVolumeURP"
{
    Properties
    {
        _MainColorGradient ("MainColorGradient", 2D) = "white" {}
        _BrightnessGradient ("BrightnessGradient", 2D) = "white" {}
        _MainNoiseSource ("MainNoiseSource", 2D) = "white" {}
        _SkyboxCube ("SkyboxCube", Cube) = "black" {}

        [Space]

        _BrightnessMultiplier ("BrightnessMultiplier", Range(0, 5)) = 3.2
        _SkyboxBrightnessMultiplier ("SkyboxBrightnessMultiplier", Float) = 800
        _SkyboxVerticalRotation ("SkyboxVerticalRotation", Float) = 0

        [Space]

        _FastSpinSpeed ("FastSpinSpeed", Range(0, 1)) = 0.37
        _SlowSpinSpeed ("SlowSpinSpeed", Range(0, 1)) = 0.09

        [Space]

        _MaxSteps ("MaxSteps", Int) = 1000
        _StepSize ("StepSize", Range(0.03, 0.1)) = 0.06
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend One One

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            //#include "Packages/jp.keijiro.noiseshader/Shader/SimplexNoise3D.hlsl"
            #include "UnityCG.cginc"

            #define SCHWARZSCHILD 0.5
            #define DISK_INNER_RADIUS SCHWARZSCHILD * 3 //AKA the Innermost Stable Circular Orbit (ISCO) = schwarzschild radius * 3
			#define DISK_OUTER_RADIUS SCHWARZSCHILD * 13
			#define DISK_VOLUMETRIC_HEIGHT 0.08
			#define SKYBOX_MARCH_RADIUS DISK_OUTER_RADIUS * 1.3
            
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;

                float3 rayOrigin : TEXCOORD1;
				float3 hitPosition : TEXCOORD2;
            };

            sampler2D _MainColorGradient;
            sampler2D _BrightnessGradient;
            sampler2D _MainNoiseSource;
            samplerCUBE _SkyboxCube;

            float _BrightnessMultiplier;
            float _SkyboxBrightnessMultiplier;
            float _SkyboxVerticalRotation;

            float _FastSpinSpeed;
            float _SlowSpinSpeed;

            int _MaxSteps;
            float _StepSize;

            float _Slider;

			float4x4 _ObjectToWorldMatrix;
			float4x4 _WorldToObjectMatrix;
            
            //Shader assumes blackhole is at object origin

			//Referenced articles/websites:
			//https://medium.com/dotcrossdot/raymarching-simulating-a-black-hole-53624a3684d3
			//https://www.shadertoy.com/view/XdjXDy
			//https://www.shadertoy.com/view/tsBXW3

			//Skybox generator: https://wwwtyro.github.io/space-3d/#animationSpeed=2.951618060622956&fov=90&nebulae=true&pointStars=true&resolution=2048&seed=2bjl0mehpyf4&stars=true&sun=false
			//Good seeds:
			//2bjl0mehpyf4
			//7a39vixeyjr4 - used for main menu
			//2zxhaw5xokw0

            float inverselerp(float a, float b, float value)
			{
				return (value - a) / (b - a);
			}

			float sdSphere(float3 p, float radius)
			{
				return length(p) - radius;
			}

			float sdCircle(float2 p, float radius)
			{
				return length(p) - radius;
			}

			float sdTorus(float3 p, float width, float radius)
			{
				return length(float2(length(p.xz) - radius, p.y)) - width;
			}

			float sdPlaneBox(float3 p, float height)
			{
				return abs(p.y) - height;
			}

			float sdDiskBox(float3 p, float inner, float outer, float height)
			{
				float ring = max(-sdCircle(p.xz, inner), sdCircle(p.xz, outer));
				return max(ring, sdPlaneBox(p, height));
			}

			float sdAccretion(float3 p)
			{
				return sdDiskBox(p, DISK_INNER_RADIUS, DISK_OUTER_RADIUS, DISK_VOLUMETRIC_HEIGHT);
			}

			float to0to1(float value)
			{
				return (value + 1) / 2;
			}

			float to1to1(float value)
			{
				return value * 2 - 1;
			}

			float corner(float value, float power)
			{
				return saturate(1 / (1 - min(value, 1)) / (power * power));
			}

			float2 rotate(const float2 value, const float degree)
			{
				const float radian = radians(degree);

				const float sinValue = sin(radian);
				const float cosValue = cos(radian);

				return float2(cosValue * value.x - sinValue * value.y, sinValue * value.x + cosValue * value.y);
			}
            
            float3 RotateXZ(float3 direction, float angle)
            {
                float s = sin(angle);
                float c = cos(angle);
                float2 rotatedXZ = float2(direction.x * c - direction.z * s, direction.x * s + direction.z * c);
                return float3(rotatedXZ.x, direction.y, rotatedXZ.y);
            }
            
            float3 SampleSkybox(float3 direction)
            {
                const float intensity = _SkyboxBrightnessMultiplier;

                float3 worldDirection = UnityObjectToWorldDir(direction);
                worldDirection.xz = RotateXZ(worldDirection, _SkyboxVerticalRotation);

                return texCUBE(_SkyboxCube, worldDirection).rgb * intensity;
            }


            
            float3 GetGradientColor(float t)
            {
                return tex2D(_MainColorGradient, float2(t, 0.5)).rgb;
            }

            float GetGradientBrightness(float t)
            {
                return tex2D(_BrightnessGradient, float2(t, 0.5)).r * _BrightnessMultiplier;
            }

            float GetNoiseValue(float3 uvw)
            {
                return tex2D(_MainNoiseSource, uvw).r;
            }

            float3 GetColor(float3 position)
            {
                // Perform necessary calculations here

                // Example calculation:
                float t = position.x + position.y + position.z;
                float3 color = GetGradientColor(t);
                float brightness = GetGradientBrightness(t);

                return color * brightness;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 position = mul(unity_ObjectToWorld, i.vertex).xyz;
                float3 color = GetColor(position);
                return fixed4(color, 1.0);
            }

            
            ENDHLSL
        }
    }
}