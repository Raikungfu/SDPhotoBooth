using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.ImageProcessingDTO
{
    public class ImageProcessingDTO
    {
        public string ImageUrl { get; set; }
        public FormData FormData { get; set; }
    }

    public class FormData
    {
        public string PhotoStyle { get; set; } = "id_photo";
    }

    public class Option
    {
        public bool RemoveBackground { get; set; }
        public bool AlignFace { get; set; }
        public bool SmoothSkin { get; set; }
        public bool Resize { get; set; }
        public bool StableDiffusion { get; set; }
        public EditPath EditStrength { get; set; } = new();
        public EditPath GuidanceScale { get; set; } = new();
    }

    public class EditPath
    {
        public double Face { get; set; }
        public double Clothes { get; set; }
        public double Combine { get; set; }
    }

    public class PersonalInfo
    {
        public string Gender { get; set; } = "male";
        public int Seek { get; set; }
    }
}
