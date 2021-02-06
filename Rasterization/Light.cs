using System;
using System.Collections.Generic;
using System.Text;

namespace Rasterization
{
    public interface ILight
    {
        vector3 Calculate(VertexProcessor vert, vector3 position, vector3 normal);
    }
}
