using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace CodieGo_Adventure.Controllers
{
    [ServiceFilter(typeof(RedirectIfAuthenticatedFilter))]
    public class VerificationController : BaseController
    {
        private readonly EmailService _emailService;

        public VerificationController(IGenericServices services, EmailService emailService) : base(services) { _emailService = emailService; }

        public ActionResult Verify() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmailCode(string code)
        {
            try
            {
                int? savedCode = HttpContext.Session.GetInt32("EmailVerificationCode") ?? 0;
                string expiryString = HttpContext.Session.GetString("EmailVerificationExpiry");

                if (savedCode == null || expiryString == null)
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Verification session expired. Please register again."
                    });

                    return RedirectToAction("Register", "Account");
                }

                int verificationCode = savedCode.Value;

                DateTime expiryTime = DateTime.Parse(expiryString, null, DateTimeStyles.RoundtripKind);

                if (DateTime.UtcNow > expiryTime)
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Verification code has expired. Please request a new one."
                    });

                    return RedirectToAction(nameof(Verify));
                }

                if (code != verificationCode.ToString())
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Verification code is incorrect."
                    });

                    return RedirectToAction(nameof(Verify));
                }

                Users users = new Users();
                users.SetUsername(HttpContext.Session.GetString("PendingUsername"));
                users.SetEmail(HttpContext.Session.GetString("PendingUserEmail"));
                users.SetPassword(HttpContext.Session.GetString("PendingPassword"));
                users.SetRegisterationDate();
                users.SetIsNewUser(true);

                await _services.AddDataAsync<Users>(users);

                Profiles profiles = new Profiles();
                profiles.SetProfileId(users.UserId);
                profiles.SetDiplayName(users.Username);
                profiles.SetBio("This is my bio");

                await _services.AddDataAsync<Profiles>(profiles);

                HttpContext.Session.Remove("EmailVerificationCode");
                HttpContext.Session.Remove("EmailVerificationExpiry");
                HttpContext.Session.Remove("PendingUserEmail");
                HttpContext.Session.Remove("PendingUsername");
                HttpContext.Session.Remove("PendingPassword");

                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "success",
                    title = "SUCCESS",
                    message = "Account registered."
                });

                return RedirectToAction("Login", "Account");
            }
            catch
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Verification code cannot be empty."
                });

                return RedirectToAction(nameof(Verify));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendCode()
        {
            string email = HttpContext.Session.GetString("PendingUserEmail");
            if (email == null)
            {
                return Json(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Verification session expired. Please register again."
                });
            }

            string expiryStr = HttpContext.Session.GetString("EmailVerificationExpiry");
            bool isExpired = true;

            if (expiryStr != null)
            {
                DateTime expiry = DateTime.Parse(expiryStr, null, DateTimeStyles.RoundtripKind);

                isExpired = DateTime.UtcNow > expiry;
            }

            string lastSentStr = HttpContext.Session.GetString("LastVerificationSent");
            if (!isExpired && lastSentStr != null)
            {
                DateTime lastSent = DateTime.Parse(lastSentStr);
                if ((DateTime.UtcNow - lastSent).TotalSeconds < 60)
                {
                    return Json(new
                    {
                        type = "error",
                        title = "WAIT",
                        message = "Please wait before requesting a new code."
                    });
                }
            }

            Random rnd = new Random();
            int newCode = rnd.Next(100000, 999999);

            HttpContext.Session.SetInt32("EmailVerificationCode", newCode);

            HttpContext.Session.SetString(
                "EmailVerificationExpiry",
                DateTime.UtcNow.AddMinutes(15).ToString("o")
            );

            HttpContext.Session.SetString(
                "LastVerificationSent",
                DateTime.UtcNow.ToString("o")
            );

            string body = $@"
<div style=""max-width:600px;margin:0 auto;
            background:#050f23;
            border-radius:10px;
            overflow:hidden;
            font-family:Arial,Helvetica,sans-serif;
            color:#dffaff;"">

  <div style=""padding:20px 24px;
              text-align:center;
              border-bottom:1px solid rgba(0,255,255,.2);"">
    <h1 style=""margin:0;
               color:#48eaff;
               letter-spacing:4px;
               font-size:22px;"">
      CODIE GO
    </h1>
    <p style=""margin:6px 0 0;
              font-size:12px;
              opacity:.8;"">
      Adventure
    </p>
  </div>

  <div style=""padding:28px 24px;"">
    <p style=""margin-top:0;
              font-size:14px;"">
      Hello,
    </p>

    <p style=""font-size:14px;
              line-height:1.6;"">
      Use the verification code below to continue creating your
      <strong>CODIE GO</strong> account:
    </p>

    <div style=""text-align:center;
                margin:30px 0;"">
      <div style=""
           display:inline-block;
           padding:16px 32px;
           background:rgba(0,234,255,.1);
           border:1px solid rgba(0,255,255,.6);
           color:#48eaff;
           font-size:26px;
           font-weight:bold;
           letter-spacing:6px;
           border-radius:8px;"">
        {newCode}
      </div>
    </div>

    <p style=""font-size:13px;
              line-height:1.6;"">
      This verification code will expire in
      <strong>15 minutes</strong>.
    </p>

    <p style=""font-size:13px;
              line-height:1.6;"">
      If you did <strong>not</strong> request this code,
      you can safely ignore this email.
    </p>

    <p style=""margin:0;
               color:#48eaff;
               font-weight:600;
               letter-spacing:4px;
               font-size:13px;"">
      IMPORTANT:
    </p>

    <div style=""font-size:13px;
              opacity:.7;
              line-height:1.6;"">
      Never share this verification code with anyone.<br>
      CODIE GO staff will never ask for your code.
    </div>

    <p style=""margin-top:26px;
              font-size:13px;"">
      See you in the adventure,<br>
      <strong style=""color:#48eaff;"">
        The CODIE GO Adventures Team
      </strong>
    </p>
  </div>

  <div style=""padding:14px 24px;
              text-align:center;
              font-size:11px;
              opacity:.7;
              border-top:1px solid rgba(0,255,255,.15);"">
    This is an automated message. Please do not reply.
  </div>

</div>
";

            await _emailService.SendEmailAsync(email, "Your CODIE GO Verification Code", body);

            return Json(new
            {
                type = "success",
                title = "SUCCESS",
                message = "A new verification code has been sent."
            });
        }
    }
}
