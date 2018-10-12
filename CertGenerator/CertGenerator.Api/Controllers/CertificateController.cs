using CertGenerator.Api.Models.DTO;
using jsreport.Binary;
using jsreport.Local;
using jsreport.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CertGenerator.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class CertificateController : ApiController
    {
        [Route("Generate")]
        public async Task<IHttpActionResult> Post(CertificateGeneratorDTO payload)
        {
            var ex = new Exception();
            var msg = string.Empty;
            try
            {
                var rs = new LocalReporting().UseBinary(JsReportBinary.GetBinary())
                    .Configure(cfg => cfg.AllowedLocalFilesAccess().BaseUrlAsWorkingDirectory())
                    .AsUtility().Create();

                var report = await rs.RenderAsync(new RenderRequest()
                {
                    Template = new Template()
                    {
                        Recipe = Recipe.PhantomPdf,
                        Engine = Engine.Handlebars,
                        Content = "Hello from pdf"
                    }
                });

                return Ok(new { data = string.Empty, msg });
            }
            catch (Exception caughtEx)
            {
                return BadRequest(msg);
            }
        }

    }
}
