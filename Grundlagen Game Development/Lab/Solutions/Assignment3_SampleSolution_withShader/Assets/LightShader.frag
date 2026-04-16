#version 130
// pass value from script via uniforms
uniform vec2 PlayerScreenPos;
uniform float coneScale;

void main()
{
    // Calc Distance of Fragment
    // gl_TexCoord[0] -> tex coordiantes of texture with idx 0
    float dist = distance(vec2(gl_TexCoord[0] / coneScale), vec2(PlayerScreenPos / coneScale));

    // Pixel RGBA , A = distance 
    //vec4 pixel = vec4(gl_TexCoord[0].x, gl_TexCoord[0].y, 0, 0.5f); 	

    vec4 pixel = vec4(0, 0, 0, dist);

    // multiply it by the color
    gl_FragColor = gl_Color * pixel;
}