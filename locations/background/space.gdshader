shader_type canvas_item;

uniform vec3 space_color: source_color = vec3(0.0, 0.0, 0.0); //color of space
uniform float star_density = 10.; // density

float RandomFloat(vec2 uv){
	return fract(sin(dot(uv.xy, vec2(12.9898,78.233))) * 43758.5453123);
}

vec2 RandomVec2(vec2 uv){
	vec2 seed = fract(uv*244.048);
	seed += dot(seed, seed + vec2(488.34, 206.543));
	float x = fract(sin(dot(seed, vec2(647,78.233))) * 4858.5453123);
	float y = fract(sin(dot(seed, vec2(889,78.233))) * 4858.5453123);
	return vec2(x, y);
}

void vertex() {
}

void fragment() {
	COLOR.xyz = space_color;
	
	vec2 coord = UV*star_density;
	vec2 cell_position = floor(coord);
	vec2 uv_cell = abs(fract(coord)-0.5);
	
	float ray = max(0.0,(1.0 - uv_cell.x*uv_cell.y*1000.));
	float star = 0.01/(length(uv_cell));
	//star = star*ray;
	
	//float star = 1./abs(uv_plane.y*uv_plane.x*1000.);//0.0001/(length(plane_coord));
	//float test = 0.005/(length(uv_plane));
	//float rays = max(0, 1.0 - abs((plane_coord.x*plane_coord.y)*1000.0))*star;
	//star += rays*10.;
	//star = pow(star, 3);
	
	//COLOR.xyz += uv_cell.x*0.5;
	//COLOR.xyz += uv_cell.y*0.5;
	COLOR.xyz += star*ray;
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
