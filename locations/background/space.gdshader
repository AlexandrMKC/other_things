shader_type canvas_item;

uniform vec3 space_color: source_color = vec3(0.0, 0.0, 0.0); //color of space
uniform float star_density = 10.; // density
uniform float star_size_paramter:hint_range(0.0, 1.0, 0.01) = 1.0;

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

float Star(vec2 uv_star, float brightness){
	float star = 0.01/(length(uv_star));
	float rays = max(0.0,(1.0 - abs(uv_star.x*uv_star.y*1000.)))*star;
	star += rays*brightness;
	return pow(star, 3);
}

void vertex() {
}

void fragment() {
	COLOR.xyz = space_color;

	vec2 coord = UV*star_density;
	vec2 cell_position = floor(coord);
	vec2 uv_cell = fract(coord)-0.5;

	float star_size = RandomFloat(cell_position)*star_size_paramter;

	for(float x = -1.0; x <= 1.0; x++){
		for(float y = -1.0; y <= 1.0; y++){
			vec2 offset = vec2(x,y);
			vec2 star_position_offset = RandomVec2(cell_position + offset);

			vec2 uv_star = uv_cell + star_position_offset - offset;
			//vec2 star_position = cell_position - offset;

			COLOR += Star(uv_star, 1.0)*star_size;

		}
	}
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
