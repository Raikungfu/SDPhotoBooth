using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public static class QueueRoutingHelper
    {
        public static string GetQueueName(string photoStyle)
        {
            return photoStyle switch
            {
                //"anime" or "vintage" or "cartoon" or "fashion_style" => "image_processing_queue_openpose_canny",
                //"id_photo" => "image_processing_queue",
                //"nature" => "image_processing_queue_depth",
                //"modern" => "image_processing_queue_canny_depth",
                //"artistic_portrait" or "fantasy" => "image_processing_queue_openpose_depth",
                _ => "image_processing_queue"
            };
        }
    }
}
