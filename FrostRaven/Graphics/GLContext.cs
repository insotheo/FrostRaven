using Silk.NET.OpenGL;

namespace FrostRaven.Graphics
{
    internal static class GLContext
    {
        private static GL _gl;

        internal static void InitGLContext(GL gl) => _gl = gl;

        internal static void Clear(float R, float G, float B, float alpha) => _gl.ClearColor(R, G, B, alpha);
        internal static void Clear() => _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

    }
}
