#ifndef WATER_SHADER_HELPER
#define WATER_SHADER_HELPER

#include "UnityCG.cginc"

	/* Created: Flaregames 09.03.2015
	 * Last Update: Flaregames 30.03.2015
	 */
	 
// General	
	// http://stackoverflow.com/questions/4200224/random-noise-functions-for-glsl
	float rand(float2 co)
	{
		return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
	}
	
	float GetEdgeFade(float environmentHeight, float edgeFade)
	{
		float fade = saturate(environmentHeight-edgeFade);
		return( fade );
		//return( ((1-environmentHeight)*edgeFade)+(1-edgeFade) );
	}

// Height
	float GetRelativeHeight(float mapHeight, float shallow, float deep)
	{
		float height = mapHeight;
		
		height = max(deep, height);
		height = min(shallow, height);
		
		height = (height-deep) / (shallow-deep);
		
		return( height );
	}
	
	float2 GetShoreDirection(sampler2D environmentMap, float2 uv)
	{
		// calculate the shore direction
		// later replaced with flowmap
		
		float height;
		float center = tex2D(environmentMap, uv).x;
		float2 flow = float2(0,0);
		
		for(int x=-1;x<=1;++x)
		{
			for(int y=-1;y<=1;++y)
			{
				height = tex2D(environmentMap, uv+float2(x*0.005,y*0.005)).x;
				flow += float2(x,y) * (center-height);
			}
		}
		
		flow = normalize(flow);
		return(flow);
	}
	
	float4 GetDisplacedPosition(float4 vertex)
	{
		float4 pos = mul(UNITY_MATRIX_MVP, vertex);
		pos.y += sin(vertex.y-_Time.z)*0.8;
		return(pos);
	}


// Normal mapping
	float3 GetNormal(sampler2D normalMap, float2 uv, float normalStrength)
	{
		return saturate(UnpackNormal( tex2D(normalMap, uv ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );
	}

	float3 GetNormal(sampler2D normalMap, float4 uv, float normalStrength)
	{
		float3 normal1 = saturate(UnpackNormal( tex2D(normalMap, uv.xy ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );
		float3 normal2 = saturate(UnpackNormal( tex2D(normalMap, uv.zw ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );

		return normal1 - float3(normal2.x,0,normal2.z);
	}
	
	float2 GetUV(float2 inUV, float2 movement)
	{
		return( inUV + movement*_Time.x );
	}
	
	
// Light
	float3 DiffuseLightSimple(float3 normalDir, float3 lightDir, float ambientMultiplier)
	{
		return max(dot(normalDir,lightDir),UNITY_LIGHTMODEL_AMBIENT * ambientMultiplier);
	}

	float3 SpecularLightSimple(float3 normalDir, float3 lightDir, float3 viewDir, float shininess)
	{
		return ( pow( saturate(dot(reflect(-lightDir, normalize(normalDir)), viewDir)), shininess) );
	}
	
	
// Depth
	float2 GetDepthValues(float4 posScreen, sampler2D depthTexture, float shallowDeepFade, float shallowDepth, float edgeFade)
	{
		float depth = tex2D(depthTexture, posScreen);
		float depthLevel = (depth - posScreen.z);
		float waterLevel = (depth - posScreen.z)*shallowDeepFade - shallowDepth;
		depthLevel = edgeFade * depthLevel;
		return float2(depthLevel, saturate(waterLevel));
	}
	

// Waves
	float2 GetMovement(float4 movement)
	{
		return( movement.xy*_Time.x*movement.z );
	}

	float2 GetDirectionalWave(float2 dir, float2 pos, float waveLength, float waveSpeed)
	{
		float dist = sin( length(pos.x * waveLength) + (_Time.y * waveSpeed) );
		return( dir * dist );
	}
	
	float2 GetCurveWave(float2 pos, float waveLength, float waveSpeed, int loops)
	{
		float2 uvOffset = float2(0,0);
		float dist = 0;
		float2 dir = 0;
		float time = _Time.y*waveSpeed;
		
		for(int a=loops;a>=0;--a)
		{
			dir = pos+float2(rand(a+1),rand(a))*10000;
			dist = sin( length( dir )* waveLength + time );
			uvOffset += normalize(dir.xy)*dist/ loops;
		}
		
		return(uvOffset);
	}
	
	float GetBlending(float3 pos, float waveLength, float3 movement, float wavePower)
	{
		float blending = saturate(_SinTime.z);//sin(_Time.y);//saturate(_SinTime.y);
		
		
		
		/*float dist = 0;
		float2 dir = 0;
		float time = _Time.y*movement.z;
		
		dir = float2(movement.x*0.7+pos.x+3760, movement.y*0.3+pos.z-17000);
		dist = sin( length( dir )* waveLength + time );
		blending += dist;
		
		dir = float2(movement.x*0.7+pos.x-2800, movement.y*0.3+pos.z+1900);//;pos + float2(movement.x*3462, movement.y*9124);
		dist = sin( length( length(dir) )* waveLength + time );
		blending += dist;
		
		dir = float2(movement.x*0.7+pos.x-10560, movement.y*0.3+pos.z-2630);//pos + float2(movement.x*-7716, movement.y*4371);
		dist = sin( length( length(dir) )* waveLength + time );
		blending += dist;
		
		blending = blending * 0.5 + 0.5 ;
		blending = saturate(blending);*/
		
		return( blending );
	}
	
// Foam
	float4 GetWaveFoam(sampler2D tex, float2 uv, float2 waveOffset, float factor)
	{
		float val = tex2D(tex,uv).z;
		float4 foam = float4(val,val,val,0.5);
		foam *= pow((saturate(waveOffset.x)+saturate(waveOffset.y)),factor);
		return(foam);
	}
	
	float4 GetShoreFoam(sampler2D tex, float2 uv, float2 shoreDir, float waterLevel, float length, float edgeFade, float speed, float power)
	{
		shoreDir *= sin(_Time.y*speed)*power;
		
		float val = tex2D(tex,uv+shoreDir).z;
		float4 foamTex = float4(val,val,val,0.5);
		
		float shoreFoam = saturate( (waterLevel - (1 - length))/(length) );
		float value = saturate(sin(_Time.y*speed + uv.x*0.1));
		shoreFoam *= value*0.5+0.5;
		
		float4 foam = float4(foamTex.xyz * shoreFoam,foamTex.w);
		
		return(foam);
	}

	float3 GetFoam(sampler2D environmentMap, float2 uv, float groundLevel, float distance)
	{
		float3 foam = tex2D(environmentMap, uv);
		//float3 foam = float3(value, value, value);

		float height = max(1-distance, groundLevel);
		height = min(1.0, height);
		
		height = (height-(1-distance)) / distance;

		return foam * height;
	}
	
	float4 GetShoreWave(sampler2D tex, float2 uv, float waterLevel, float speed, float range, float offset)
	{
		float val = tex2D(tex,uv).z;
		float4 foamTex = float4(val,val,val,val);
		
		float curr = saturate(sin(_Time.y*speed + uv.x*0.1));
		float cosi = saturate(cos(_Time.y*speed + uv.x*0.1));
		float shoreLine =  0;
		
		shoreLine = curr*cosi;
		shoreLine *= saturate( (curr-(waterLevel-(range*0.2)))*(1/(range*0.2)) );
		shoreLine *= saturate( (curr-(offset))*(1/offset) );
		shoreLine *= saturate( ((waterLevel+range)-curr)*(1/range) );
		
		float4 foam = foamTex * shoreLine * waterLevel;
		
		return(foam);
	}
	
// Reflection
	float4 GetReflection(float4 posScreen, sampler2D reflectionTex)
	{
		return tex2Dproj( reflectionTex, UNITY_PROJ_COORD(posScreen) );
	}

	float4 GetReflection(float4 posScreen, sampler2D reflectionTex, float3 normalDir, float refraction)
	{
		return tex2Dproj( reflectionTex, UNITY_PROJ_COORD(posScreen)+normalDir.xzxz*refraction );
	}

	float4 GetReflectivity(float3 viewDir, float reflectivity)
	{
		return saturate((1-dot(float3(0,1,0), abs(viewDir))) + reflectivity);
	}
#endif