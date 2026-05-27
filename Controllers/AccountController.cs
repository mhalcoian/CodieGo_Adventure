using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Models;
using CodieGo_Adventure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace CodieGo_Adventure.Controllers
{
    [ServiceFilter(typeof(RedirectIfAuthenticatedFilter))]
    public class AccountController : BaseController
    {
        private readonly EmailService _emailService;

        public AccountController(IGenericServices services, EmailService emailService) : base(services) { _emailService = emailService; }

        // Endpoint for login page
        public ActionResult Login() => View();

        // Function to login user
        public async Task<IActionResult> LoginUser(string username, string password)
        {
            ViewBag.Username = username;
            ViewBag.Password = password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Username or Password is empty."
                });

                return View(nameof(Login));
            }

            Users users = new Users();
            LoginRecords loginRecords = new LoginRecords();

            users.SetUsername(username);
            users.SetPassword(password);

            var data = await _services.GetDataByUsernameOrEmailAndPasswordAsync<Users>(users.Username, users.Password);

            if (data == null)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Account not found."
                });

                return View(nameof(Login));
            }

            HttpContext.Session.SetInt32("UserId", data.UserId);

            Guid sessionId = Guid.NewGuid();

            loginRecords.UserId = data.UserId;
            loginRecords.SetSessionId(sessionId);
            loginRecords.SetLoginDateTime();
            loginRecords.SetStatus("Online");

            await _services.AddDataAsync<LoginRecords>(loginRecords);

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(99),
                HttpOnly = true,
                Secure = HttpContext.Request.IsHttps,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("SessionId", sessionId.ToString(), cookieOptions);

            TempData["Popup"] = JsonSerializer.Serialize(new
            {
                type = "success",
                title = "SUCCESS",
                message = "Signed"
            });

            return RedirectToAction("Dashboard", "Home");
        }

        // Endpoint for register page
        public ActionResult Register() => View();

        // Function to create account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAccount(string username, string email, string password, string confirm_password)
        {
            try
            {
                ViewBag.Username = username;
                ViewBag.Email = email;
                ViewBag.Password = password;
                ViewBag.ConfirmPassword = confirm_password;

                var userDetails = await _services.GetAllDataAsync<Users>();

                if (string.IsNullOrWhiteSpace(username))
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Username is empty."
                    });

                    return View(nameof(Register));
                }

                if (userDetails.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "This username is already taken."
                    });

                    return View(nameof(Register));
                }

                if (userDetails.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "This email is already connected to an existing account."
                    });

                    return View(nameof(Register));
                }

                Users users = new Users();

                if (!users.IsEmailValid(email))
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Invalid email format."
                    });

                    return View(nameof(Register));
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Password is empty."
                    });

                    return View(nameof(Register));
                }

                if (!users.IsPasswordStrong(password))
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Password must be at least 8 characters, include an uppercase letter, and a number or symbol."
                    });

                    return View(nameof(Register));
                }

                if (password != confirm_password)
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Password not match."
                    });

                    return View(nameof(Register));
                }

                Random rnd = new Random();
                int verificationCode = rnd.Next(100000, 999999);

                HttpContext.Session.SetInt32("EmailVerificationCode", verificationCode);
                HttpContext.Session.SetString("EmailVerificationExpiry", DateTime.UtcNow.AddMinutes(15).ToString("o"));
                HttpContext.Session.SetString("PendingUserEmail", email);
                HttpContext.Session.SetString("PendingUsername", username);
                HttpContext.Session.SetString("PendingPassword", password);

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
        {verificationCode}
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

                return RedirectToAction("Verify", "Verification");
            }
            catch
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "warning",
                    title = "WARNING",
                    message = "Username, Email, Password must not be empty."
                });

                return View(nameof(Register));
            }
        }

        // Endpoint for forgot password page
        [AllowAnonymous]
        [HttpGet("/forgot-password")]
        public ActionResult ForgotPassword() => View();

        [AllowAnonymous]
        [HttpPost("/forgot-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                // Check if user exists
                var user = await _services.GetAllDataAsync<Users>();
                var matchedUser = user.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (matchedUser == null)
                {
                    TempData["Popup"] = JsonSerializer.Serialize(new
                    {
                        type = "error",
                        title = "ERROR",
                        message = "Email not found."
                    });
                    return View();
                }

                // Generate a secure token
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                                 .Replace("+", "")
                                 .Replace("/", "")
                                 .Replace("=", "");

                var resetToken = new PasswordResetTokens();
                resetToken.SetUserId(matchedUser.UserId);
                resetToken.SetToken(token);
                resetToken.SetExperationDate();
                await _services.AddDataAsync<PasswordResetTokens>(resetToken);

                string resetUrl = Url.Action("ResetPassword", "Account", new { token = token }, Request.Scheme);

                string body = $@"
<div style=""max-width:600px;margin:0 auto;background:#050f23;border-radius:10px;overflow:hidden;font-family:Arial,Helvetica,sans-serif;color:#dffaff;"">

  <!-- Header -->

  <div style=""padding:20px 24px;text-align:center;border-bottom:1px solid rgba(0,255,255,.2);"">
    <h1 style=""margin:0;color:#48eaff;letter-spacing:4px;font-size:22px;"">
      CODIE GO
    </h1>
    <p style=""margin:6px 0 0;font-size:12px;opacity:.8;"">
      Adventure
    </p>
  </div>

  <!-- Body -->

  <div style=""padding:28px 24px;"">
    <p style=""margin-top:0;font-size:14px;"">
      Hello <strong style=""color:#48eaff;"">{matchedUser.Username}</strong>,
    </p>

```
<p style=""font-size:14px;line-height:1.6;"">
  We received a request to reset your <strong>CODIE GO</strong> account password.
  Click the button below to continue:
</p>

<!-- Button -->
<div style=""text-align:center;margin:28px 0;"">
  <a href=""{resetUrl}""
     style=""display:inline-block;padding:14px 30px;
            background:linear-gradient(90deg,#00eaff,#00bcd4);
            color:#031318;font-weight:bold;
            letter-spacing:2px;text-decoration:none;
            border-radius:8px;
            box-shadow:0 0 18px rgba(0,255,255,.6);"">
    RESET PASSWORD
  </a>
</div>

<p style=""font-size:13px;line-height:1.6;"">
  This reset link will expire in <strong>1 hour</strong>.
  If it expires, you can always request a new one.
</p>

<p style=""font-size:13px;line-height:1.6;"">
  If you did <strong>not</strong> request a password reset, you can safely ignore this email.
  Your account will remain secure.
</p>

<p style=""margin-top:26px;font-size:13px;"">
  See you in the adventure,<br>
  <strong style=""color:#48eaff;"">The CODIE GO Adventures Team</strong>
</p>
```

  </div>

  <!-- Footer -->

  <div style=""padding:14px 24px;text-align:center;
              font-size:11px;opacity:.7;
              border-top:1px solid rgba(0,255,255,.15);"">
    This is an automated message. Please do not reply.
  </div>

</div>

";

                await _emailService.SendEmailAsync(email, "Reset your CODIE GO password", body);

                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "success",
                    title = "SUCCESS",
                    message = "We sent a password reset link to your email."
                });

                return View();
            }
            catch
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Please enter your email."
                });
                return View();
            }
        }

        // Endpoint for reset password page
        [AllowAnonymous]
        [HttpGet("/reset-password")]
        public async Task<IActionResult> ResetPassword(string token)
        {
            var resetTokens = await _services.GetDataByTokenAsync<PasswordResetTokens>(token);

            if (resetTokens.ExpirationDate < DateTime.Now && resetTokens.UsedDate == null)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "The request made had already expired."
                });
                return RedirectToAction(nameof(Login));
            }

            if (resetTokens.UsedDate != null)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "The request made had been used."
                });
                return RedirectToAction(nameof(Login));
            }

            return View(new ResetPasswordToken
            {
                Token = token
            });
        }

        [AllowAnonymous]
        [HttpPost("/reset-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string token, string password, string confirm_password)
        {
            var resetTokens = await _services.GetDataByTokenAsync<PasswordResetTokens>(token);
            var user = await _services.GetDataByIdAsync<Users>(x => x.UserId == resetTokens.UserId);

            if (resetTokens == null || user == null)
                return RedirectToAction("Login");

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirm_password))
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Passwords field are empty."
                });
                return View(new ResetPasswordToken
                {
                    Token = token
                });
            }

            if (password != confirm_password)
            {
                TempData["Popup"] = JsonSerializer.Serialize(new
                {
                    type = "error",
                    title = "ERROR",
                    message = "Password not match."
                });
                return View(new ResetPasswordToken
                {
                    Token = token
                });
            }

            user.SetPassword(password);
            await _services.UpdateDataAsync<Users>(user);
            resetTokens.SetUsedDate();
            await _services.UpdateDataAsync<PasswordResetTokens>(resetTokens);

            TempData["Popup"] = JsonSerializer.Serialize(new
            {
                type = "success",
                title = "SUCCESS",
                message = "Password reseted."
            });

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var users = await _services.GetAllDataAsync<Users>();

            bool exists = users.Any(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            return Json(new { available = !exists });
        }

        // Endpoint for error tracing page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => 
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
