using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace gilbor.Customclassess
{
    public class EmailTemplateService
    {
        public async Task<string> NotificationTempBody()
        {
            var templatePath = HostingEnvironment.MapPath("~/EmailTemplates/NotificationTemp.cshtml");
            StreamReader reader = new StreamReader(templatePath);
            var body = await reader.ReadToEndAsync();
            reader.Close();
            return body;
        }
    }
}