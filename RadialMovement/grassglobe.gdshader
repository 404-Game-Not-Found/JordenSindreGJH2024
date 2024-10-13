shader_type canvas_item;

uniform sampler2D texture_grass;  // Grass texture
uniform sampler2D texture_dirt;   // Dirt texture
uniform float transition_smoothness = 0.1;
uniform float dirt_radius = 0.5;  // Controls how far the dirt extends inward
uniform vec2 center = vec2(0.5, 0.5);  // Center of the circular world (normalized UV coordinates)
uniform float earth_radius = 0.5;  // Radius of the circular world in normalized UV coordinates
uniform vec2 texture_scale = vec2(10.0, 10.0);  // Controls how many times the texture repeats

// Function to rotate UV coordinates using a 2D rotation matrix
vec2 rotate_uv(vec2 uv, float angle, vec2 pivot) {
    float cos_angle = cos(angle);
    float sin_angle = sin(angle);
    
    // Translate UV to pivot (center)
    uv -= pivot;
    
    // Apply 2D rotation matrix
    vec2 rotated_uv = vec2(
        cos_angle * uv.x - sin_angle * uv.y,
        sin_angle * uv.x + cos_angle * uv.y
    );
    
    // Translate back
    return rotated_uv + pivot;
}

void fragment() {
    // Get UV coordinates
    vec2 uv = UV;

    // Calculate the distance from the center of the circle
    float distance_from_center = distance(uv, center);

    // Check if the current pixel is outside the circle
    if (distance_from_center > earth_radius) {
        discard;  // Discard pixels outside the circular earth
    }

    // Calculate the direction vector from the center to the current pixel
    vec2 direction = uv - center;

    // Calculate the angle of the direction vector (radians)
    float angle = atan(direction.y, direction.x);

    // Rotate the UV coordinates based on the angle, using the center as the pivot
    vec2 rotated_uv = rotate_uv(uv, angle, center);

    // Scale UV coordinates to repeat the texture
    rotated_uv = fract(rotated_uv * texture_scale);

    // Calculate the blend factor for transition between grass and dirt
    float blend_factor = smoothstep(dirt_radius - transition_smoothness, dirt_radius + transition_smoothness, distance_from_center);

    // Sample both textures (grass and dirt) with rotated and scaled UV coordinates
    vec4 grass_color = texture(texture_grass, rotated_uv);
    vec4 dirt_color = texture(texture_dirt, rotated_uv);

    // Blend the two textures based on the blend factor
    vec4 final_color = mix(dirt_color, grass_color, blend_factor);

    // Output the final blended color
    COLOR = final_color;
}
