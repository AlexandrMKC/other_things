shader_type sky;

uniform vec3 sky_color: source_color = vec3(0.0, 0.0, 0.0);

void sky() {
	COLOR = sky_color;
}
