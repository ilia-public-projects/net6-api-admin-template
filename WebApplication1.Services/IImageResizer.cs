﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IImageResizer
    {
        byte[] ResizeImage(Stream sourceImageStream, int width, int height);
    }
}
