using Newtonsoft.Json;
using ShareX.UploadersLib.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ShareX.UploadersLib.ImageUploaders
{
    public class Szurubooru : ImageUploaderService
    {
        public override ImageDestination EnumValue { get; } = ImageDestination.Szurubooru;

        public override Icon ServiceIcon => Resources.Vgyme;

        public override bool CheckConfig(UploadersConfig config) => true;

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new SzurubooruUploader(config);
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpSzurubooru;
    }

    public sealed class SzurubooruUploader : ImageUploader
    {
        public UploadersConfig Config;

        public SzurubooruUploader(UploadersConfig config)
        {
            Config = config;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            NameValueCollection headers = new NameValueCollection
            {
                ["Accept"] = "application/json",
                ["Authorization"] = "Token " + Config.SzurubooruServerToken
            };

            UploadResult result = SendRequestSzuru(Config.SzurubooruServerURL + "/posts/", stream, fileName, "content", null, headers);

            if (result.IsSuccess)
            {
                SzurubooruResponse response = JsonConvert.DeserializeObject<SzurubooruResponse>(result.Response);

                if (response != null)
                {
                    //result.URL = Config.SzurubooruClientURL + "/post/" + response.ID;
                    result.ThumbnailURL = Config.SzurubooruClientURL + "/" + response.ContentUrl;
                    result.URL = Config.SzurubooruClientURL + "/post/" + response.ID;
                }
            }

            return result;
        }

        private class SzurubooruResponse
        {
            public int ID { get; set; }
            public string ContentUrl { get; set; }
        }
    }
}
