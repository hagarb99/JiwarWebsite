using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GEWAR.Models
{
    public partial class PropertyMedia : BaseModel
    {

        public int PropertyID { get; set; }
        public string MediaURL { get; set; }
        public string MediaType { get; set; }
        public DateTime UploadedDate { get; set; }

        public static bool IsValidMediaType(string mediaType)
        {
            if (mediaType == "360Tour")
                return true;

            return Enum.TryParse<MediaTypeEnum>(mediaType, out var result) &&
                   Enum.IsDefined(typeof(MediaTypeEnum), result);
        }
    }
}
