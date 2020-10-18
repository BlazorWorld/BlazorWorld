using BlazorWorld.Data.Identity;
using BlazorWorld.Services;
using BlazorWorld.Services.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ISecurityService _securityService;
        private readonly BlazorWorld.Services.Content.IProfileService _profileService;
        private readonly IInvitationService _invitationService;

        public RegisterModel(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ISecurityService securityService,
            BlazorWorld.Services.Content.IProfileService profileService,
            IInvitationService invitationService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _securityService = securityService;
            _profileService = profileService;
            _invitationService = invitationService;
            if (_configuration["InvitationRequired"] == "true")
            {
                InvitationRequired = true;
            }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public bool InvitationRequired { get; set; } = false;
        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Invitation Code")]
            public string InvitationCode { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            // check email
            var existingUser = await _userManager.FindByEmailAsync(Input.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("email", "Email is already used by another user.");
            }
            // check username
            existingUser = await _userManager.FindByNameAsync(Input.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("username", "Username is already taken.");
            }

            var inviteRequirementMet = true;
            if (InvitationRequired && !_securityService.IsAdminInConfig(Input.Username))
            {
                if (string.IsNullOrEmpty(Input.InvitationCode))
                    ModelState.AddModelError("code", "Please enter the Invitation Code.");
                else
                {
                    var userId = await _invitationService.GetInvitationAsync(Input.Email, Input.InvitationCode);
                    inviteRequirementMet = !string.IsNullOrEmpty(userId);
                    if (!inviteRequirementMet)
                        ModelState.AddModelError("code", "Invitation Code is invalid.");
                }
            }

            if (existingUser == null && inviteRequirementMet && ModelState.IsValid)
            {
                string avatarHash = string.Empty;
                using (var md5 = MD5.Create())
                {
                    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Input.Email));
                    avatarHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
                }

                var user = new ApplicationUser() {
                    UserName = Input.Username,
                    Email = Input.Email,
                    AvatarHash = avatarHash
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _profileService.Add(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var siteUrl = _configuration["SiteUrl"];
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
