using Particle.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace finaltest.Controllers
{
    
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var success = await ParticleCloud.SharedCloud.LoginAsync("stathisvotsis@gmail.com", "eystbots");
            ParticleDevice myDevice = null;
            List<ParticleDevice> devices = await ParticleCloud.SharedCloud.GetDevicesAsync();
            foreach (ParticleDevice device in devices)
            {
                //MessageBox.Show(device.Name.ToString());
                myDevice = device;
            }
            ViewBag.PhotonMessage = "You device is" + " " + myDevice.Name.ToString();
            foreach (string functionName in myDevice.Functions)
                ViewBag.Photon2Message = ($"Device has function: {functionName}");
            var functionResponse = await myDevice.RunFunctionAsync("relayOn", "2");
            var result = functionResponse.ReturnValue;
           
            //ViewBag.Photon3Message(result.ToString());
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}