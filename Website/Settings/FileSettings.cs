using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreServer.Settings
{
    public class FileSettings
    {

        private readonly string[] _allowed_ext = { "docx", "doc", "pdf", "jpg", "png", "gif", "jpeg", "xlsx", "odp", "odt", "txt" };
        private readonly string[] _allowed_image_ext = { "jpg", "png", "gif", "jpeg" };
        private readonly string _basePath = "../";

        public string BasePath
        {
            get
            {
                return this._basePath;
            }
        }
        public List<string> Allowed_ext
        {
            get
            {
                return this._allowed_ext.ToList();
            }
        }
        public List<string> Allowed_image_ext
        {
            get
            {
                return this._allowed_image_ext.ToList();
            }
        }

    }

}
