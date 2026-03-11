using ShareX.UploadersLib.ImageUploaders;
using ShareX.UploadersLib.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareX.UploadersLib.FileUploaders
{
    public class Szurubooru : FileUploaderService
    {
        public override FileDestination EnumValue { get; } = FileDestination.Szurubooru;

        public override Icon ServiceIcon => Resources.puush;

        public override bool CheckConfig(UploadersConfig config) => true;

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new SzurubooruUploader(config);
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpSzurubooru;
    }
}
