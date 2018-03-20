using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace WatchWord.Authorization.Accounts.Dto
{
    public class ChangePasswordInput
    {
        [Required]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [Required]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}
