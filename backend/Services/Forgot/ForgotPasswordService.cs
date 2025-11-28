using SAConstruction.DTO;
using SAConstruction.Repositories;
using SAConstruction.Helpers;

namespace SAConstruction.Services
{
    public class ForgotPasswordService
    {
        private readonly UserRepository _userRepo;
        private readonly MailjetService _mailjetService;

        private readonly ForgotPasswordRepository _forgotPasswordRepo;

        public ForgotPasswordService(IConfiguration config)
        {
            _userRepo = new UserRepository(config);
            _mailjetService = new MailjetService(config);
            _forgotPasswordRepo = new ForgotPasswordRepository(config);
        }

        public async Task<bool> SendResetLink(ResetPasswordRequest req)
        {

            // 1. Verify Request
            if (string.IsNullOrWhiteSpace(req.Email))
            {
                throw new Exception("Email is required.");
            }


            // 2. See if the user exists
            var existingUser = _userRepo.GetUserByEmail(req.Email);

            if (existingUser == null)
            {
                throw new Exception("No account exists with that email address.");
            }

            // 2.5 Rate limit — block if sent within the last 5 minutes
            if (existingUser.LastPasswordResetEmailSentAt.HasValue)
            {
                var lastSent = existingUser.LastPasswordResetEmailSentAt.Value;

                if (lastSent >= DateTime.UtcNow.AddMinutes(-5))
                {
                    // Don't send another email
                    throw new Exception("Recent Mail Sent");
                }
            }

            
            // 3. Create the document to reset the password.
            var createdDocument = _forgotPasswordRepo.SetPasswordResetDoc(existingUser.UserId);


            // 4. Send the email
            var sent = await _mailjetService.SendResetLinkEmail(existingUser, createdDocument.ReferenceId);

            // 5. upadte the LastPasswordResetEmailSentAt property
            _userRepo.UpdateLastResetTime(createdDocument.ReferenceId);

            // 6. Return success to controller
            return sent;
        }


        public bool isResetDocValid(string referenceId)
        {

            var resetDoc = _forgotPasswordRepo.GetPasswordResetDocByRefId(referenceId);

            return true;
        }


        public void ResetPassword(NewPasswordRequest req)
        {
            if (req.NewPassword != req.ConfirmPassword)
            {
                throw new Exception("Passwords do not match.");
            }


            if (!PasswordHelpers.IsValidPassword(req.NewPassword))
            {
                throw new Exception("Password is not valid.");
            }



            // make sure we have a valid reset doc.
            var resetDoc = _forgotPasswordRepo.GetPasswordResetDocByRefId(req.ReferenceId);


            // update the users account
            _userRepo.UpdateUserPassword(req.NewPassword, resetDoc.UserId);


            // delete the reset doc
            _forgotPasswordRepo.DeleteResetDocByRefId(req.ReferenceId);
            

            
        }

    }
}
