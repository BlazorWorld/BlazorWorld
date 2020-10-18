using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Services.Security;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IAppEmailSender _emailSender;
        
        public InvitationService (
            IInvitationRepository invitationRepository,
            IAppEmailSender emailSender)
        {
            _invitationRepository = invitationRepository;
            _emailSender = emailSender;
        }

        public async Task<int> AddAsync(string url, string createdBy, string emails)
        {
            string[] emailArray = emails.Split(new string[] {"\n", "\r\n", ",", ";", " "}, StringSplitOptions.RemoveEmptyEntries);
            int result = 0;

            foreach (string email in emailArray)
            {
                string code = CreateRandomCode(6);

                Invitation invitation = new Invitation();
                invitation.CreatedBy = createdBy;
                invitation.CreatedDate = DateTime.UtcNow.ToString("s");
                invitation.Email = email;
                invitation.InvitationCode = code;

                result += await _invitationRepository.Add(invitation);
                await SendEmail(email, url, code);
            }

            return result;
        }

        public string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeCount);
                }

                temp = t;
                randomCode += allCharArray[t];
            }

            return randomCode;
        }

        public async Task<bool> SendEmail(string email, string url, string code)
        {
            url = url + "?email=" + email + "&code=" + code;
            await _emailSender.SendEmailAsync(email, "Invitation",
                $"You have been invited. Please register by clicking this link: <a href='{url}'>link</a>");

            return true;
        }

        public async Task<string> GetInvitationAsync(string email, string code)
        {
            return await _invitationRepository.GetInvitationAsync(email, code);
        }

        public async Task<Invitation[]> GetInvitationsAsync(string createdBy)
        {
            return await _invitationRepository.GetInvitationsAsync(createdBy);
        }
    }
}