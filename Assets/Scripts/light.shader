Shader "Custom/ColorOnLight" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
    }
        SubShader{
        // Add pass tag here
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Add other shader pragmas and passes here
        // ...

        struct SurfaceOutputStandard {
            fixed4 Color;
            fixed3 Normal;
            fixed3 Emission;
            fixed Specular;
            fixed Shininess;
            fixed Alpha;
        };

        struct Light {
            fixed4 color;
            fixed3 direction;
            fixed intensity;
        };

        struct VertexInput {
            float3 vertex : POSITION;
            float3 normal : NORMAL;
        };

        struct FragmentInput {
            float3 worldPos : TEXCOORD0;
            float3 worldNormal : TEXCOORD1;
            fixed4 color : COLOR;
        };

        FragmentInput vert(VertexInput v) {
            FragmentInput o;
            o.worldPos = mul(unity_ObjectToWorld, v.vertex);
            o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
            o.color = _Color;
            return o;
        }

        SurfaceOutputStandard frag(FragmentInput i) {
            SurfaceOutputStandard o;
            o.Alpha = 1.0;
            o.Emission = 0;
            o.Normal = i.worldNormal;

            // Calculate lighting
            fixed3 diffuse = 0;
            fixed3 specular = 0;
            fixed3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
            for (int l = 0; l < UNITY_LIGHT_MAX_LIGHTS; l++) {
                Light light = GetLight(l);
                if (light.intensity > 0 && dot(light.direction, i.worldNormal) > 0) {
                    fixed3 lightDir = normalize(light.direction);
                    diffuse += light.color.rgb * light.intensity * max(0, dot(lightDir, i.worldNormal));
                    specular += light.color.rgb * light.intensity * pow(max(0, dot(reflect(-lightDir, i.worldNormal), viewDir)), _Shininess);
                }
            }

            o.Color = i.color * (diffuse + specular);
            return o;
        }
