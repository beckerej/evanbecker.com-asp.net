using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvanBecker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EvanBecker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Raymarch()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> ServerStatus()
        {
            MachineStatsViewModel vm;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://138.197.9.190/api/machinestats"))
                    {
                        var apiResponse = response.Content.ReadAsStringAsync();
                        var machineStats = JsonConvert.DeserializeObject<List<MachineStats>>(await apiResponse);
                        machineStats = machineStats.Where((x, i) => i % 6 == 0).TakeLast(5 * 60).ToList(); // take 1 every minute of last 5 hours
                        vm = new MachineStatsViewModel(machineStats) { IsOnline = true };
                    }
                }
            }
            catch (Exception)
            {
                vm = new MachineStatsViewModel(new List<MachineStats>()) { IsOnline = false };
            }
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult PostEmail([FromBody]ContactFormModel email)
        {
            SendEmail(email.FirstName, email.LastName, email.Email, email.Phone, email.Message);
            return Contact();
        }


        public string SendEmail(string firstName, string secondName, string email, string phone, string message)
        {
            try
            {
                var emailMessage = $"{message}\n\n-{firstName} {secondName},\nPhone number: {phone}\n{email}";
                // Credentials
                var credentials = new NetworkCredential("bcker08@gmail.com", Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
                // Mail message
                var mail = new MailMessage
                {
                    From = new MailAddress("noreply@evanbecker.com"),
                    Subject = $"Email from {email}",
                    Body = emailMessage,
                    IsBodyHtml = true
                };
                mail.To.Add(new MailAddress("ebecker.designs@gmail.com"));
                // Smtp client
                var client = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.Send(mail);
                return "Email Sent Successfully!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
