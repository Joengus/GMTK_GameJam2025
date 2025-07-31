shader_type spatial;

uniform vec4 color1 : source_color = vec4(0.0, 0.0, 0.0, 1.0); // Black
    uniform vec4 color2 : source_color = vec4(1.0, 1.0, 1.0, 1.0); // White
    uniform float scale : hint_range(1.0, 100.0) = 50.0;

    void fragment() {
        vec2 uv_scaled = UV * scale;
        float checker_pattern = mod(floor(uv_scaled.x) + floor(uv_scaled.y), 2.0);

        if (checker_pattern < 0.5) {
            ALBEDO = color1.rgb;
        } else {
            ALBEDO = color2.rgb;
        }
}