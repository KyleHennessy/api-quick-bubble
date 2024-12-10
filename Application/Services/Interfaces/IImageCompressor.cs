﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IImageCompressor
    {
        string CompressImage(string base64Image, int quality);
    }
}
